using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    private readonly float MinMineDelay = 1f;
    private readonly float MaxMineDelay = 2f;

    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private float _mineSpeed = 1f;

    private readonly List<ScrapMine> _scrapMines = new();
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

    private void AddResource(ScrapMine scrapMine)
    {
        if (!_scrapMines.Contains(scrapMine))
        {
            _scrapMines.Add(scrapMine);
            scrapMine.Destroyed += DeleteResource;
        }

        _mineCoroutine ??= StartCoroutine(MineResources());
        _playerAnimator.UpdateExtracting(true);
    }

    private void DeleteResource(ScrapMine scrapMine)
    {
        if (_scrapMines.Contains(scrapMine))
        {
            _scrapMines.Remove(scrapMine);
            scrapMine.Destroyed -= DeleteResource;
        }
    }

    private IEnumerator MineResources()
    {
        float mineDelay = 1 / _mineSpeed;
        WaitForSeconds wait = new(mineDelay);

        while (_scrapMines.Count > 0)
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
        _playerAnimator.UpdateExtracting(false);
    }

    private ScrapMine FindNearestResource()
    {
        ScrapMine nearestResource;

        if (_scrapMines.Count > 0)
            nearestResource = _scrapMines.OrderBy(resource => (resource.transform.position - transform.position).magnitude).FirstOrDefault();
        else
            nearestResource = null;

        return nearestResource;
    }
}