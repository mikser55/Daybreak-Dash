using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100;

    public event Action<Health> ObjectDied;
    public event Action EnemyDied;
    public event Action<Enemy> EnemyComponentDied;
    public event Action Damaged;

    public float CurrentHealth { get; private set; }

    private void OnEnable()
    {
        CurrentHealth = _maxHealth;
    }

    public void IncreaseMaxHealth(float value)
    {
        if (value > 0)
        {
            _maxHealth += value;
            CurrentHealth += value;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        if (damage > 0)
            CurrentHealth -= damage;

        Damaged?.Invoke();
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);

        if (CurrentHealth == 0)
            Die();
    }

    private void Die()
    {
        ObjectDied?.Invoke(this);
        EnemyDied?.Invoke();

        if (TryGetComponent(out Enemy enemy))
            EnemyComponentDied(enemy);
    }
}