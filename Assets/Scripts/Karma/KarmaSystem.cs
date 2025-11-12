using UnityEngine;
public class KarmaSystem : MonoBehaviour
{
    [SerializeField] private GameObject _enemySpawner;
    private int _currentCount;

    void Start()
    {
        _currentCount = 100;
		_enemySpawner.GetComponent<EnemySpawner>().Start();
    }

    public void GetKarmaChange(int change)
    {
        _currentCount += change;
		_enemySpawner.GetComponent<EnemySpawner>().SetIndex(_currentCount);
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