using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    [SerializeField] protected TurretScanner Scanner;

    [SerializeField] protected List<Enemy> Enemies = new();

    protected Enemy NearestTarget;
    protected bool CanShoot = true;

    public abstract void StartShoot();

    protected void AddNewEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);

        if (enemy.TryGetComponent(out Health health))
            health.Died += RemoveEnemy;
    }

    protected void RemoveEnemy(Enemy enemy)
    {
        if (enemy.TryGetComponent(out Health health))
            health.Died -= RemoveEnemy;

        Enemies.Remove(enemy);
    }

    protected void FindNearestEnemy()
    {
        if (Enemies.Count > 0)
        {
            NearestTarget = Enemies
                .OrderBy(enemy => (enemy.transform.position - transform.position).magnitude)
                .FirstOrDefault();
        }
        else
        {
            NearestTarget = null;
        }
    }
}