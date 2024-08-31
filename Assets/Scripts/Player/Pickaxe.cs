using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    private readonly float MinMineDelay = 1f;
    private readonly float MaxMineDelay = 2f;

    [SerializeField] private Scanner _scanner;
    [SerializeField] private float _mineSpeed = 1f;

    private readonly List<ScrapMine> _resources = new();
    private readonly float _mineDamage = 1f;
    private Coroutine _mineCoroutine;

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

    public void IncreaseMineSpeed(float value)
    {
        if (value > 0)
        {
            _mineSpeed += value;
            _mineSpeed = Mathf.Clamp(_mineSpeed, MinMineDelay, MaxMineDelay);
        }
    }

    private void AddResource(ScrapMine resource)
    {
        if (!_resources.Contains(resource))
        {
            _resources.Add(resource);
            resource.Destroyed += DeleteResource;
        }

        _mineCoroutine ??= StartCoroutine(MineResources());
    }

    private void DeleteResource(ScrapMine resource)
    {
        if (_resources.Contains(resource))
        {
            _resources.Remove(resource);
            resource.Destroyed -= DeleteResource;
        }
    }

    private IEnumerator MineResources()
    {
        float mineDelay = 1 / _mineSpeed;
        WaitForSeconds wait = new(mineDelay);

        while (_resources.Count > 0)
        {
            ScrapMine nearestResource = FindNearestResource();

            if (nearestResource != null && nearestResource.TryGetComponent(out Health health))
            {
                health.TakeDamage(_mineDamage);

                if (health.CurrentHealth == 0)
                    DeleteResource(nearestResource);
            }

            yield return wait;
        }

        _mineCoroutine = null;
    }

    private ScrapMine FindNearestResource()
    {
        ScrapMine nearestResource;

        if (_resources.Count > 0)
            nearestResource = _resources.OrderBy(resource => (resource.transform.position - transform.position).magnitude).FirstOrDefault();
        else
            nearestResource = null;

        return nearestResource;
    }
}