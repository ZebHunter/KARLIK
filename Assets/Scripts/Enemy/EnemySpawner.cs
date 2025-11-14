using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct EnemyData
{
    public GameObject enemy;
    public int priotity;
}

public class EnemySpawner : MonoBehaviour
{
    public Dictionary<int, GameObject> _enemyPrefabs = new();
    public EnemyData[] _enemyUpgrades;
    public static EnemySpawner Instance { get; private set; }

    public EnemySpawner()
    {
        Instance = this;
    }

    private int _currentIndex = 0;
    private int _minBorder = 1;
    private int _maxBorder = 15;
    [SerializeField]
    public int _maxEnemies;
    [SerializeField]
    private Transform _cameraTransform;

    private int _currentEnemies = 0;
    private bool _gameEnded = false;

    public Dictionary<int, GameObject> GetEnemies => _enemyPrefabs;

    public void Dead()
    {
        _currentEnemies--;
    }
    private float _cameraWidth;
    public float CameraWidth => _cameraWidth;

    public void Start()
    {
        _gameEnded = false;
        _currentEnemies = 0;
        _maxBorder = 15;
        EventBus.Instance.Subscribe<KarmaChangedSignal>(OnKarmaChanged);
        EventBus.Instance.Subscribe<EndSignal>(OnEndSignal);
        _maxEnemies = _maxBorder;
        _currentIndex = _enemyUpgrades.Length - 1;
        _cameraWidth = _cameraTransform.GetComponent<Camera>().orthographicSize * _cameraTransform.GetComponent<Camera>().aspect;
        foreach (var enemyData in _enemyUpgrades)
        {
            _enemyPrefabs[enemyData.priotity] = enemyData.enemy;
        }
    }

    void OnEndSignal(EndSignal _)
    {
        _gameEnded = true;
    }

    void OnDestroy()
    {
        EventBus.Instance.Unsubscribe<KarmaChangedSignal>(OnKarmaChanged);
        EventBus.Instance.Unsubscribe<EndSignal>(OnEndSignal);
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void Update()
    {
        if (_gameEnded) return;
        if (_currentEnemies < _minBorder || _currentEnemies < _maxEnemies)
        {
            _currentEnemies++;
            Vector2 randomPoint = new Vector2(Random.Range(_cameraWidth, _cameraWidth +0.05f ), Random.Range(_cameraWidth , _cameraWidth + 0.05f)) + new Vector2(_cameraTransform.position.x, _cameraTransform.position.y);
            var enemyPrefab = _enemyPrefabs[_currentIndex];
            switch (Random.Range(0, 4))
            {
                case 0:
                    randomPoint.x += _cameraWidth;
                    var enemy0 = Instantiate(enemyPrefab, new Vector3(randomPoint.x, randomPoint.y, 0), Quaternion.identity);
                    var karma0 = enemy0.GetComponent<EnemyKarma>();
                    if (karma0 != null) karma0.ChangeLevel(_currentIndex);
                    break;
                case 1:
                    randomPoint.x -= _cameraWidth;
                    var enemy1 = Instantiate(enemyPrefab, new Vector3(randomPoint.x, randomPoint.y, 0), Quaternion.identity);
                    var karma1 = enemy1.GetComponent<EnemyKarma>();
                    if (karma1 != null) karma1.ChangeLevel(_currentIndex);
                    break;
                case 2:
                    randomPoint.y += _cameraWidth;
                    var enemy2 = Instantiate(enemyPrefab, new Vector3(randomPoint.x, randomPoint.y, 0), Quaternion.identity);
                    var karma2 = enemy2.GetComponent<EnemyKarma>();
                    if (karma2 != null) karma2.ChangeLevel(_currentIndex);
                    break;
                case 3:
                    randomPoint.y -= _cameraWidth;
                    var enemy3 = Instantiate(enemyPrefab, new Vector3(randomPoint.x, randomPoint.y, 0), Quaternion.identity);
                    var karma3 = enemy3.GetComponent<EnemyKarma>();
                    if (karma3 != null) karma3.ChangeLevel(_currentIndex);
                    break;
            }
        }
    }
    
    

    public void OnKarmaChanged(KarmaChangedSignal signal)
    {
        _currentIndex = signal.NewEnemyLevel;
    }

    public void AddMaxEnemies(int value)
    {
        _maxEnemies += value;
        _maxBorder += value;
    }
}