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
    private void ChangeLevel(int level) => _level = level;
    private bool _isEvolving = false;
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
        var newPidor = Instantiate(_enemyUpgrade[targetLevel], transform.position, Quaternion.identity);
        newPidor.GetComponent<EnemyKarma>().ChangeLevel(targetLevel);
    }

    void OnDestroy()
    {
        EventBus.Instance.Unsubscribe<KarmaChangedSignal>(OnKarmaChanged);
        EventBus.Instance.Unsubscribe<EndSignal>(OnEndSignal);
        
        if(_karmaPoints > 0)
        {
            SchoolGirlSpawner.GirlSpawned = false;
        }

        if (!_isEvolving)
        {
            EnemySpawner.Instance.Dead();
            KarmaSystem.Instance.GetKarmaChange(_karmaPoints);
            Instantiate(expPrefub, transform.position, Quaternion.identity);
        }
    }
}