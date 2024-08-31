using UnityEngine;

public class BulletTurretFactory : TurretFactory
{
    [SerializeField] private BulletTurret _prefab;

    public override void CreateTurret()
    {
        foreach (var spawnPoint in SpawnPoints)
            Instantiate(_prefab, spawnPoint.position, Quaternion.LookRotation(Vector3.right));
    }
}