using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth = 100;
    [SerializeField]
    private Slider _healthBar;
    private int _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
        _healthBar.maxValue = _maxHealth;
        _healthBar.value = _currentHealth;
    }

    public void GetDamage(int damage)
    {
        _currentHealth -= damage;
        _healthBar.value = _currentHealth;
    }
}
