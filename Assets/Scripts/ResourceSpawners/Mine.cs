using System;
using System.Collections;
using UnityEngine;

public abstract class Mine : MonoBehaviour
{
    [SerializeField] private float _collectTime = 1f;

    private Coroutine _coroutine;

    public event Action ResourceCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ResourceManager player))
        {
            _coroutine = StartCoroutine(CollectResource(player));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ResourceManager _))
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }
    }

    private IEnumerator CollectResource(ResourceManager _)
    {
        WaitForSeconds wait = new(_collectTime);

        yield return wait;

        while (enabled)
        {
            ResourceCollected?.Invoke();
            yield return wait;
        }
    }
}