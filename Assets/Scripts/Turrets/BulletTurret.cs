using System;
using System.Collections;
using UnityEngine;

public class BulletTurret : Turret
{
    [SerializeField] private float _damage = 1;
    [SerializeField] private float _maxAmmo = 100;
    [SerializeField] private float _reloadTime = 3f;
    [SerializeField] private float _shootDelay = 0.5f;

    private Coroutine _shootCoroutine;
    private float _currentAmmo;

    private void Start()
    {
        _currentAmmo = _maxAmmo;
    }

    //private void OnEnable()
    //{
    //    Scanner.EnemyEntered += AddNewEnemy;
    //    Scanner.EnemyExited += RemoveEnemy;
    //    Scanner.StartingShoot += StartShoot;
    //}

    //private void OnDisable()
    //{
    //    Scanner.EnemyEntered -= AddNewEnemy;
    //    Scanner.EnemyExited -= RemoveEnemy;
    //    Scanner.StartingShoot -= StartShoot;
    //}

    public override void StartShoot()
    {
        if (_shootCoroutine != null)
            StopCoroutine(ShootCoroutine());

        _shootCoroutine = StartCoroutine(ShootCoroutine());
    }

    private void Shoot()
    {
        FindNearestEnemy();

        if (NearestTarget.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
            _currentAmmo--;
            _currentAmmo = Mathf.Clamp(_currentAmmo, 0, _maxAmmo);

            if (_currentAmmo == 0)
                Reload();

        }
    }

    private IEnumerator ShootCoroutine()
    {
        WaitForSeconds wait = new(_shootDelay);

        while (CanShoot && Enemies.Count > 0)
        {
            Shoot();
            yield return wait;
        }

        _shootCoroutine = null;
    }

    private void Reload()
    {
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        CanShoot = false;
        yield return new WaitForSeconds(_reloadTime);
        _currentAmmo = _maxAmmo;
        CanShoot = true;

        if (NearestTarget != null)
            StartShoot();
    }
}