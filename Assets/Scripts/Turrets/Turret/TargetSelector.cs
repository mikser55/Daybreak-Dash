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

    public event Action EnemySelected;

    public void AddEnemy(Enemy enemy)
    {
        if (!_enemies.Contains(enemy))
            _enemies.Add(enemy);

        if (enemy.TryGetComponent(out Health health))
            health.EnemyComponentDied += RemoveEnemy;

        EnemySelected?.Invoke();
    }

    public void RemoveEnemy(Enemy enemy)
    {
        if (_enemies.Contains(enemy))
            _enemies.Remove(enemy);

        if (enemy.TryGetComponent(out Health health))
            health.EnemyComponentDied -= RemoveEnemy;
    }

    public Enemy GetNearestEnemy()
    {
        Enemy nearestEnemy = null;
        float nearestDistanceSqr = float.MaxValue;

        foreach (var enemy in _enemies)
        {
            if (enemy == null) continue;

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