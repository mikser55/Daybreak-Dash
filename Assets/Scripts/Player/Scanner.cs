using System;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private const float MinValue = 0.1f;
    private const float MaxValue = 0.2f;

    [Tooltip("Scanner`s collider")]
    [SerializeField] private SphereCollider _collider;
    [Tooltip("Radius of Scanner. Range from 0.1 to 0.2")]
    [SerializeField] private float _radius;

    public event Action<ScrapMine> ResourceFound;
    public event Action<ScrapMine> ResourceLost;

    private void OnValidate()
    {
        _radius = Mathf.Clamp(_radius, MinValue, MaxValue);
        _collider.radius = _radius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ScrapMine resource))
            ResourceFound?.Invoke(resource);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ScrapMine resource))
            ResourceLost?.Invoke(resource);
    }
}