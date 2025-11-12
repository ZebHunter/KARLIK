using UnityEngine;
public class EnemyHealthSystem : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth = 100;
    private int _currentHealth;
    
    [SerializeField]
    private GameObject expPrefub;
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void GetDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            this.GetComponentInParent<EnemyKarma>().ChangeKarma();
            Destroy(gameObject);
            Instantiate(expPrefub, transform.position, Quaternion.identity);
			GameObject.Find("KarmaSystem").GetComponent<KarmaSystem>().DeadBomsh();
        }
    }
}