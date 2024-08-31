using System.Collections;
using UnityEngine;

public class BulletTurret : Turret
{
    [SerializeField] private float _damage = 1f;

    private Coroutine _coroutine;

    public override void Shoot()
    {
        _coroutine ??= StartCoroutine(ShootCoroutine(TargetSelector.GetNearestEnemy()));
    }

    private IEnumerator ShootCoroutine(Enemy target)
    {
        if (target != null)
        {

            while (target.Health.CurrentHealth > 0)
            {
                if (CurrentAmmo > 0)
                {
                    target.Health.TakeDamage(_damage);
                    CurrentAmmo--;
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    yield return StartCoroutine(ReloadCoroutine());
                }
            }

            _coroutine = StartCoroutine(ShootCoroutine(TargetSelector.GetNearestEnemy()));
        }
    }
}