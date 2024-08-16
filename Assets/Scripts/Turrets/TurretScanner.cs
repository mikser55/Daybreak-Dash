using System;
using UnityEngine;

public class TurretScanner : MonoBehaviour
{
    public event Action StartingShoot;
    public event Action<Enemy> EnemyEntered;
    public event Action<Enemy> EnemyExited;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            EnemyEntered?.Invoke(enemy);
            StartingShoot?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
            EnemyExited?.Invoke(enemy);
    }
}
