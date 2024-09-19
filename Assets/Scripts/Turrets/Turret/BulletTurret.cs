using System.Collections;
using UnityEngine;

public class BulletTurret : Turret
{
    [SerializeField] private Transform _turretGunTransform;
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _fireRate = 0.1f;

    private Enemy _target;
    private Coroutine _coroutine;

    public override void Shoot()
    {
        _coroutine ??= StartCoroutine(ShootCoroutine(TargetSelector.GetNearestEnemy()));
    }

    private void Update()
    {
        if (_target != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_target.transform.position - _turretGunTransform.position);
            //_turretGunTransform.rotation = targetRotation;
            _turretGunTransform.rotation = Quaternion.Slerp(_turretGunTransform.rotation, targetRotation, 50 * Time.deltaTime);
        }
    }

    private IEnumerator ShootCoroutine(Enemy target)
    {
        _target = target;

        if (target != null)
        {
            while (target.Health.CurrentHealth > 0)
            {
                if (CurrentAmmo > 0)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - _turretGunTransform.position);
                    //_turretGunTransform.rotation = targetRotation;
                    _turretGunTransform.rotation = Quaternion.Slerp(_turretGunTransform.rotation, targetRotation, 50 * Time.deltaTime);

                    target.Health.TakeDamage(_damage);
                    CurrentAmmo--;
                    yield return new WaitForSeconds(_fireRate);
                }
                else
                {
                    yield return StartCoroutine(ReloadCoroutine());
                }
            }

            _target = null;
            _coroutine = StartCoroutine(ShootCoroutine(TargetSelector.GetNearestEnemy()));
        }
    }
}