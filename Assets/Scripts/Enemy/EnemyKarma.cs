using UnityEngine;
using System.Collections.Generic;

public class EnemyKarma : MonoBehaviour
{
    [SerializeField]
    private GameObject expPrefub;
    [SerializeField]
    private int _karmaPoints;
    [SerializeField]
    private bool AllowEvolving;
    private int _level = 3;
    public void ChangeLevel(int level) => _level = level;
    private bool _isEvolving = false;
    private bool _scratch = false;
    private Dictionary<int, GameObject> _enemyUpgrade;
    
    void Start()
    {
        EventBus.Instance.Subscribe<KarmaChangedSignal>(OnKarmaChanged);
        EventBus.Instance.Subscribe<EndSignal>(OnEndSignal);
        _enemyUpgrade = EnemySpawner.Instance.GetEnemies;
    }

    void OnEndSignal(EndSignal _)
    {
        _isEvolving = false;
        _scratch = true;
        Destroy(gameObject);
    }

    void OnKarmaChanged(KarmaChangedSignal signal)
    {
        if(!AllowEvolving) return;
        if(signal.NewEnemyLevel == _level) return;
        int targetLevel = signal.NewEnemyLevel;
        if(targetLevel < 3)
            SchoolGirlSpawner.AllowSpawning = true;
        _isEvolving = true;
        EventBus.Instance.Unsubscribe<KarmaChangedSignal>(OnKarmaChanged);
        Destroy(gameObject);
        var newEnemy = Instantiate(_enemyUpgrade[targetLevel], transform.position, Quaternion.identity);
        newEnemy.GetComponent<EnemyKarma>().ChangeLevel(targetLevel);
    }

    void OnDestroy()
    {
        EventBus.Instance.Unsubscribe<KarmaChangedSignal>(OnKarmaChanged);
        EventBus.Instance.Unsubscribe<EndSignal>(OnEndSignal);
        if(_karmaPoints > 0)
        {
            SchoolGirlSpawner.GirlSpawned = false;
        }

        if (!_isEvolving && !_scratch)
        {
            EnemySpawner.Instance.Dead();
            KarmaSystem.Instance.GetKarmaChange(_karmaPoints);
            Instantiate(expPrefub, transform.position, Quaternion.identity);
        }
    }
}