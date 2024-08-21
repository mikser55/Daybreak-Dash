using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;

    private readonly List<Resource> _resources = new();
    private readonly float _mineDistance = 0.2f;
    private readonly float _mineDelay = 1f;
    private readonly float _mineAmount = 1f;

    private Coroutine _mineCoroutine;
    private WaitForSeconds wait;

    private void OnEnable()
    {
        _scanner.ResourceFound += AddResource;
        _scanner.ResourceLost += DeleteResource;
    }

    private void OnDisable()
    {
        _scanner.ResourceFound -= AddResource;
        _scanner.ResourceLost -= DeleteResource;
    }

    private void Start()
    {
        wait = new(_mineDelay);
    }

    private void AddResource(Resource resource)
    {
        if (!_resources.Contains(resource))
            _resources.Add(resource);

        _mineCoroutine ??= StartCoroutine(MineResources());
    }

    private void DeleteResource(Resource resource)
    {
        if (_resources.Contains(resource))
            _resources.Remove(resource);
    }

    private IEnumerator MineResources()
    {
        while (_resources.Count > 0)
        {
            Resource nearestResource = FindNearestResource();

            if (nearestResource != null && nearestResource.TryGetComponent(out Health health))
            {
                health.TakeDamage(_mineAmount);

                if (health.CurrentHealth == 0)
                    DeleteResource(nearestResource);
            }

            yield return wait;
        }

        _mineCoroutine = null;
    }

    private Resource FindNearestResource()
    {
        float sqrMineDistance = _mineDistance * _mineDistance;
        Resource nearestResource;

        if (_resources.Count > 0)
        {
            nearestResource = _resources
                .OrderBy(resource => (resource.transform.position - transform.position).magnitude)
                .FirstOrDefault();
        }
        else
        {
            nearestResource = null;
        }

        return nearestResource;
    }
}