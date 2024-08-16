using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100;

    private float _currentHealth;

    public event Action<Enemy> Died;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (damage > 0)
            _currentHealth -= damage;

        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        if (_currentHealth == 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);

        if (TryGetComponent(out Enemy enemy))
            Died?.Invoke(enemy);
    }
}