using UnityEngine;
public class CryptHealthSystem : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth = 100;
    private int _currentHealth;
    private int _previousEnemyLevel = 3;
    void Start()
    {
        _currentHealth = _maxHealth;
        EventBus.Instance.Subscribe<KarmaChangedSignal>(OnKarmaChanged);
    }

    public void OnKarmaChanged(KarmaChangedSignal signal)
    {
        if(signal.Karma == 0){
            Destroy(transform.parent.gameObject);
            return;
        }
        for(int i = 0; i < _previousEnemyLevel - signal.NewEnemyLevel; ++i){
            if(Random.Range(0,3) == 0){
                Destroy(transform.parent.gameObject);
            }
        }
        _previousEnemyLevel = signal.NewEnemyLevel;
    }

    public void GetDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    void OnDestroy()
    {
        EventBus.Instance.Unsubscribe<KarmaChangedSignal>(OnKarmaChanged);
    }

}
