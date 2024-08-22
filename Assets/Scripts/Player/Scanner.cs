using System;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public event Action<ScrapMine> ResourceFound;
    public event Action<ScrapMine> ResourceLost;

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