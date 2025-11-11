using UnityEngine;
public class EnemyHealthSystem : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth = 100;
    private int _currentHealth;
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void GetDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            EnemySpawner.Instance.DecreaseEnemies();
            Destroy(gameObject);
        }
    }
}