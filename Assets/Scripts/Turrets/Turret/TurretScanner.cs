using System;
using UnityEngine;

public class TurretScanner : MonoBehaviour
{
    public event Action<Health> EnemyEntered;
    public event Action<Health> EnemyExited;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            EnemyEntered?.Invoke(enemy.Health);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
            EnemyExited?.Invoke(enemy.Health);
    }
}