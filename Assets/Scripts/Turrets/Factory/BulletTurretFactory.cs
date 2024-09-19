using UnityEngine;

public class BulletTurretFactory : TurretFactory
{
    [SerializeField] private BulletTurret _prefab;
    private bool _isInstantiated;

    private void Start()
    {
        CreateTurrets();
    }

    public override void CreateTurrets()
    {
        if (_isInstantiated == false)
        {
            foreach (var spawnPoint in SpawnPoints)
                Instantiate(_prefab, spawnPoint.position, Quaternion.identity);

            _isInstantiated = true;
        }
    }
}