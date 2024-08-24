using System.Collections.Generic;
using UnityEngine;

public class ScrapMineSpawner : MonoBehaviour
{
    [SerializeField] private ScrapSpawner _scrapSpawner;
    [SerializeField] private Player _player;
    [SerializeField] private ScrapMine _prefab;
    [SerializeField] private int _numberOfResources = 10;
    [SerializeField] private float _spawnRadius = 50f;
    [SerializeField] private float _minDistanceBetweenResources = 5f;
    [SerializeField] private Transform _baseTransform;
    [SerializeField] private float _baseRadius = 10f;

    private float _sqrBaseRadius;
    private float _sqrMinResourseDistance;
    private readonly int _maxAttempts = 100;
    private readonly List<Vector3> _spawnedPositions = new();

    private void Start()
    {
        _sqrBaseRadius = _baseRadius * _baseRadius;
        _sqrMinResourseDistance = _minDistanceBetweenResources * _minDistanceBetweenResources;
        SpawnResources();
    }

    private void SpawnResources()
    {
        for (int i = 0; i < _numberOfResources; i++)
        {
            Vector3 spawnPosition = GetValidSpawnPosition();

            if (spawnPosition != Vector3.zero)
            {
                ScrapMine scrapMine = Instantiate(_prefab, spawnPosition, Quaternion.identity);
                scrapMine.Initialize(_player, _scrapSpawner);
                _spawnedPositions.Add(spawnPosition);
            }
        }
    }

    private Vector3 GetValidSpawnPosition()
    {
        for (int attempts = 0; attempts < _maxAttempts; attempts++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * _spawnRadius;
            randomPosition.y = 0.5f;

            if (IsValidPosition(randomPosition))
                return randomPosition;
        }

        return Vector3.zero;
    }

    private bool IsValidPosition(Vector3 position)
    {
        if ((_baseTransform.position - position).sqrMagnitude < _sqrBaseRadius)
            return false;

        foreach (Vector3 spawnedPosition in _spawnedPositions)
            if (((spawnedPosition - position).sqrMagnitude) < _sqrMinResourseDistance)
                return false;

        return true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_baseTransform.position, _baseRadius);
    }
}