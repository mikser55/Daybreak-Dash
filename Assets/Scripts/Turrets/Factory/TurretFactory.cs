using System.Collections.Generic;
using UnityEngine;

public abstract class TurretFactory : MonoBehaviour
{
    [SerializeField] protected List<Transform> SpawnPoints;

    public abstract void CreateTurret();
}