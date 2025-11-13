using UnityEngine;
using System.Collections.Generic;

public class EnemyKarma : MonoBehaviour
{
    [SerializeField]
    private GameObject expPrefub;
    [SerializeField]
    private int _karmaPoints;
    private float lastKarma = 100;
    [SerializeField]
    private bool AllowEvolving;
    private bool _isEvolving = false;
    private int _upgradeIndex = 0;
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
        lastKarma = 1000;
        if(!AllowEvolving) 
        {
            lastKarma = signal.Karma;
            return;
        }
        if (signal.Karma < 75 && lastKarma >= 75)
        {
            _isEvolving = true;
                Destroy(gameObject);
            _upgradeIndex = 2;
            SchoolGirlSpawner.AllowSpawning = true;
        } else if (signal.Karma < 50 && lastKarma >= 50)
        {
            _isEvolving = true;
                Destroy(gameObject);
            _upgradeIndex = 1;
            SchoolGirlSpawner.AllowSpawning = true;
        } else if (signal.Karma < 25 && lastKarma >= 25)
        {
            _isEvolving = true;
                Destroy(gameObject);
            _upgradeIndex = 0;
        }
        lastKarma = signal.Karma;
    }

    void OnDestroy()
    {
        if(_karmaPoints > 0)
        {
            SchoolGirlSpawner.GirlSpawned = false;
        }
        EventBus.Instance.Unsubscribe<KarmaChangedSignal>(OnKarmaChanged);
        EventBus.Instance.Unsubscribe<EndSignal>(OnEndSignal);

        if (_isEvolving)
        {
            Instantiate(_enemyUpgrade[_upgradeIndex], transform.position, Quaternion.identity);
        } else {
            EnemySpawner.Instance.Dead();
            KarmaSystem.Instance.GetKarmaChange(_karmaPoints);
            Instantiate(expPrefub, transform.position, Quaternion.identity);
        }
    }
}