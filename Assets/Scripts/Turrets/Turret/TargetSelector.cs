using System;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelector
{
    private readonly List<Enemy> _enemies = new();
    private readonly Transform _turretTransform;

    public TargetSelector(Transform turretTransform)
    {
        _turretTransform = turretTransform;
    }

    public event Action EnemyEntered;

    public void AddEnemy(Health enemyHealth)
    {
        enemyHealth.ObjectDied += RemoveEnemy;

        if (enemyHealth.TryGetComponent(out Enemy enemy))
        {
            if (!_enemies.Contains(enemy))
                _enemies.Add(enemy);

            EnemyEntered?.Invoke();
        }
    }

    public void RemoveEnemy(Health enemyHealth)
    {
        if (enemyHealth.TryGetComponent(out Enemy enemy))
        {
            if (_enemies.Contains(enemy))
                _enemies.Remove(enemy);

            enemyHealth.ObjectDied += RemoveEnemy;
        }
    }

    public Enemy GetNearestEnemy()
    {
        Enemy nearestEnemy = null;
        float nearestDistanceSqr = float.MaxValue;

        foreach (Enemy enemy in _enemies)
        {
            if (enemy == null)
                continue;

            float distanceSqr = (enemy.transform.position - _turretTransform.position).sqrMagnitude;

            if (distanceSqr < nearestDistanceSqr)
            {
                nearestDistanceSqr = distanceSqr;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
}