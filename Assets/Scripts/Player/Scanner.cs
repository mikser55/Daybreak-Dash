using System;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public event Action<Resource> ResourceFound;
    public event Action<Resource> ResourceLost;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
            ResourceFound?.Invoke(resource);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
            ResourceLost?.Invoke(resource);
    }
}