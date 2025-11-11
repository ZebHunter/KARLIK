using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private int _maxEnemies = 10;
    [SerializeField]
    private Transform _cameraTransform;

    private int _currentEnemies = 0;
    private float _cameraWidth;

    public void DecreaseEnemies()
    {
        --_currentEnemies;
    }
    void Start()
    {
        _cameraWidth = _cameraTransform.GetComponent<Camera>().orthographicSize * _cameraTransform.GetComponent<Camera>().aspect;
        Instance = this;
    }
    void Update()
    {
        if (_currentEnemies < _maxEnemies)
        {
            _currentEnemies++;
            Vector2 randomPoint = new Vector2(Random.Range(0f, _cameraWidth), Random.Range(0f, _cameraWidth));

            switch (Random.Range(0, 4))
            {
                case 0:
                    randomPoint.x += _cameraWidth; 
                    Instantiate(_enemyPrefab, new Vector3(randomPoint.x, randomPoint.y, 0), Quaternion.identity);
                    break;
                case 1:
                    randomPoint.x -= _cameraWidth;
                    Instantiate(_enemyPrefab, new Vector3(randomPoint.x, randomPoint.y, 0), Quaternion.identity);
                    break;
                case 2:
                    randomPoint.y += _cameraWidth;
                    Instantiate(_enemyPrefab, new Vector3(randomPoint.x, randomPoint.y, 0), Quaternion.identity);
                    break;
                case 3:
                    randomPoint.y -= _cameraWidth;
                    Instantiate(_enemyPrefab, new Vector3(randomPoint.x, randomPoint.y, 0), Quaternion.identity);
                    break;
            }
        }
    }
}