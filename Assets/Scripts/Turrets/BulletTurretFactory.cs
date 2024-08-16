using UnityEngine;

public class BulletTurretFactory : Factory
{
    public override void CreateTurret()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/BulletTurret");
        GameObject.Instantiate(prefab);
    }
}