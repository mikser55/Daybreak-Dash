using System;
using UnityEngine;

public class TurretScanner : MonoBehaviour
{
    public event Action<Enemy> EnemyEntered;
    public event Action<Enemy> EnemyExited;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            EnemyEntered?.Invoke(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
            EnemyExited?.Invoke(enemy);
    }
}