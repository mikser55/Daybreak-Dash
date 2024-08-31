using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private DayTimer _timer;
    [SerializeField] private Transform _player;
    [SerializeField] private int _maxEnemies = 10;
    [SerializeField] private float _minSpawnDistance = 10f;
    [SerializeField] private float _maxSpawnDistance = 12f;
    [SerializeField] private float _spawnDelay = 3f;

    private List<Enemy> _enemies;
    private Coroutine _coroutine;

    private void Awake()
    {
        _enemies = new(_maxEnemies);
        StartCoroutine(PrepareCoroutine());
    }

    private void OnEnable()
    {
        _timer.NightFallen += InitiateSpawning;
    }

    private void OnDisable()
    {
        _timer.NightFallen -= InitiateSpawning;
    }

    public void ReturnEnemy(Health health)
    {
        if (health.TryGetComponent(out Enemy enemy))
        {
            _enemyPool.ReturnObject(enemy);
            _enemies.Remove(enemy);

            health.EnemyDied -= InitiateSpawning;
            health.ObjectDied -= ReturnEnemy;
        }
    }

    public void ReturnAllEnemies()
    {
        foreach (Enemy enemy in _enemies)
        {
            _enemyPool.ReturnObject(enemy);

            if (enemy.TryGetComponent(out Health health))
            {
                health.EnemyDied -= InitiateSpawning;
                health.ObjectDied -= ReturnEnemy;
            }
        }
    }

    private void InitiateSpawning()
    {
        if (_coroutine == null)
            StartCoroutine(SpawnCoroutine());
    }

    private void SpawnEnemy()
    {
        Enemy enemy = _enemyPool.GetObject();
        _enemies.Add(enemy);
        PositionEnemyAroundPlayer(enemy);

        if (enemy.TryGetComponent(out Health health))
        {
            health.ObjectDied += ReturnEnemy;
            health.EnemyDied += InitiateSpawning;
        }
    }

    private void PositionEnemyAroundPlayer(Enemy enemy)
    {
        Vector3 randomPosition = GetRandomPositionAroundPlayer();
        enemy.transform.position = randomPosition;
    }

    private Vector3 GetRandomPositionAroundPlayer()
    {
        Vector2 randomCircle = Random.insideUnitCircle.normalized * Random.Range(_minSpawnDistance, _maxSpawnDistance);
        Vector3 randomOffset = new(randomCircle.x, 0, randomCircle.y);
        return _player.position + randomOffset;
    }

    private IEnumerator SpawnCoroutine()
    {
        WaitForSeconds wait = new(_spawnDelay);

        while (_enemies.Count != _maxEnemies)
        {
            SpawnEnemy();

            yield return wait;
        }

        _coroutine = null;
    }

    private IEnumerator PrepareCoroutine()
    {
        while (_enemies.Count != _maxEnemies)
        {
            SpawnEnemy();
            yield return null;
        }

        ReturnAllEnemies();
        _enemies.Clear();
    }
}