using System.Collections;
using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    [SerializeField] private TurretScanner _scanner;
    [SerializeField] private float _reloadTime;
    [SerializeField] private int _maxAmmo;

    protected bool IsReloading;
    protected TargetSelector TargetSelector;
    protected int CurrentAmmo;

    private void Awake()
    {
        TargetSelector = new TargetSelector(transform);
    }

    private void OnEnable()
    {
        _scanner.EnemyEntered += TargetSelector.AddEnemy;
        _scanner.EnemyExited += TargetSelector.RemoveEnemy;
        TargetSelector.EnemySelected += Shoot;
    }

    private void OnDisable()
    {
        _scanner.EnemyEntered -= TargetSelector.AddEnemy;
        _scanner.EnemyExited -= TargetSelector.RemoveEnemy;
        TargetSelector.EnemySelected -= Shoot;
    }

    private void Start()
    {
        CurrentAmmo = _maxAmmo;
    }

    public abstract void Shoot();

    protected IEnumerator ReloadCoroutine()
    {
        IsReloading = true;
        yield return new WaitForSeconds(_reloadTime);
        CurrentAmmo = _maxAmmo;
        IsReloading = false;
    }
}