using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100;

    public event Action<Health> Died;
    public event Action Damaged;

    public float CurrentHealth { get; private set; }

    private void Start()
    {
        CurrentHealth = _maxHealth;
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
        Destroy(gameObject);

        if (TryGetComponent(out Enemy _))
            Died?.Invoke(this);
    }
}