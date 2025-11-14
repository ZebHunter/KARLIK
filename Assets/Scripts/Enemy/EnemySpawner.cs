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

    private bool IsPositionInBorderZone(Vector3 position)
    {
        const int worldSize = 10;
        const int chunkSize = 16;
        int totalSize = worldSize * chunkSize;
        int offset = totalSize / 2;
        
        float x = position.x;
        float y = position.y;
        
        if (x <= -offset - 1 || x >= offset || y <= -offset - 1 || y >= offset)
        {
            return true;
        }
        
        return false;
    }

    public void Update()
    {
        if (_gameEnded) return;
        if (_currentEnemies < _minBorder || _currentEnemies < _maxEnemies)
        {
            _currentEnemies++;
            Vector2 randomPoint = new Vector2(Random.Range(_cameraWidth, _cameraWidth +0.05f ), Random.Range(_cameraWidth , _cameraWidth + 0.05f)) + new Vector2(_cameraTransform.position.x, _cameraTransform.position.y);
            var enemyPrefab = _enemyPrefabs[_currentIndex];
            Vector3 spawnPosition = Vector3.zero;
            switch (Random.Range(0, 4))
            {
                case 0:
                    randomPoint.x += _cameraWidth;
                    spawnPosition = new Vector3(randomPoint.x, randomPoint.y, 0);
                    break;
                case 1:
                    randomPoint.x -= _cameraWidth;
                    spawnPosition = new Vector3(randomPoint.x, randomPoint.y, 0);
                    break;
                case 2:
                    randomPoint.y += _cameraWidth;
                    spawnPosition = new Vector3(randomPoint.x, randomPoint.y, 0);
                    break;
                case 3:
                    randomPoint.y -= _cameraWidth;
                    spawnPosition = new Vector3(randomPoint.x, randomPoint.y, 0);
                    break;
            }
            
            if (!IsPositionInBorderZone(spawnPosition))
            {
                var enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                var karma = enemy.GetComponent<EnemyKarma>();
                karma.ChangeLevel(_currentIndex);
            }
            else
            {
                _currentEnemies--;
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