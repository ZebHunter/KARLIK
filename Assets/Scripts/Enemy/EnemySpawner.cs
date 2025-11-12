using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemyPrefabs;

    private int _currentIndex = 0;
    [SerializeField]
    private int _maxEnemies = 15;
    [SerializeField]
    private Transform _cameraTransform;

    private int _currentEnemies = 0;
	public void Dead()
	{
		_currentEnemies--;
		Update();
	}
    private float _cameraWidth;

    public void Start()
    {
		_currentIndex = _enemyPrefabs.Length - 1;
        _cameraWidth = _cameraTransform.GetComponent<Camera>().orthographicSize * _cameraTransform.GetComponent<Camera>().aspect;
    }
    public void Update()
    {
        if (_currentEnemies < _maxEnemies)
        {
            _currentEnemies++;
            Vector2 randomPoint = new Vector2(Random.Range(0f, _cameraWidth), Random.Range(0f, _cameraWidth));
            var enemyPrefab = _enemyPrefabs[_currentIndex];
            switch (Random.Range(0, 4))
            {
                case 0:
                    randomPoint.x += _cameraWidth; 
                    Instantiate(enemyPrefab, new Vector3(randomPoint.x, randomPoint.y, 0), Quaternion.identity);
                    break;
                case 1:
                    randomPoint.x -= _cameraWidth;
                    Instantiate(enemyPrefab, new Vector3(randomPoint.x, randomPoint.y, 0), Quaternion.identity);
                    break;
                case 2:
                    randomPoint.y += _cameraWidth;
                    Instantiate(enemyPrefab, new Vector3(randomPoint.x, randomPoint.y, 0), Quaternion.identity);
                    break;
                case 3:
                    randomPoint.y -= _cameraWidth;
                    Instantiate(enemyPrefab, new Vector3(randomPoint.x, randomPoint.y, 0), Quaternion.identity);
                    break;
            }
        }
    }

	public void SetIndex(int value){
       var newIndex = Mathf.Min(value / 25, 3);
       if (_currentIndex < newIndex)
           _maxEnemies = Mathf.Max(_maxEnemies - 5, 10);
       if (_currentIndex > newIndex)
           _maxEnemies = Mathf.Min(_maxEnemies + 5, 40);
		_currentIndex = newIndex;
        Update();
    }
}