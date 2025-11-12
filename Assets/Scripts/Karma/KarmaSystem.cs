using UnityEngine;
public class KarmaSystem : MonoBehaviour
{
    [SerializeField] private GameObject _enemySpawner;
    [SerializeField] private int _evolveBorder = 10;
    [SerializeField] private int _devolveBorder = -10;

    private int _currentCount;

    void Start()
    {
        _currentCount = 0;
		_enemySpawner.GetComponent<EnemySpawner>().Start();
    }

    public void GetKarmaChange(int change)
    {
        _currentCount += change;
        if (_currentCount >= _evolveBorder)
        {
            _currentCount = 0;
            _enemySpawner.GetComponent<EnemySpawner>().IncreaseIndex();
            return;
        }

        if (_currentCount <= _devolveBorder)
        {
            _currentCount = 0;
            _enemySpawner.GetComponent<EnemySpawner>().DecreaseIndex();
            return;
        }
    }
	public void DeadBomsh()
	{
		_enemySpawner.GetComponent<EnemySpawner>().Dead();
	}

	public void Update()
	{
		_enemySpawner.GetComponent<EnemySpawner>().Update();
	}

}