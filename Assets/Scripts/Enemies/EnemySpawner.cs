using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private DayTimer _timer;
    [SerializeField] private Transform _player;
    [SerializeField] private int _maxEnemies = 10;
    [SerializeField] private float _minSpawnDistance = 10f;
    [SerializeField] private float _maxSpawnDistance = 12f;
    [SerializeField] private float _spawnDelay = 3f;

    private readonly List<Enemy> _unsubscribeEnemies = new();
    private List<Enemy> _enemies;
    private Coroutine _coroutine;

    private void Awake()
    {
        _enemies = new(_maxEnemies);
        StartCoroutine(PrepareCoroutine());

        _timer._reactiveCurrentTime
            .Where(time => time <= 0)
            .Subscribe(_ => InitiateSpawning());
    }

    public void ReturnEnemy(Health health)
    {
        if (health.TryGetComponent(out Enemy enemy))
        {
            _enemyPool.ReturnObject(enemy);
            _enemies.Remove(enemy);
        }
    }

    public void ReturnAllEnemies()
    {
        foreach (Enemy enemy in _enemies)
            _enemyPool.ReturnObject(enemy);
    }

    private void InitiateSpawning()
    {
        _coroutine ??= StartCoroutine(SpawnCoroutine());
    }

    private void SpawnEnemy()
    {
        Enemy enemy = _enemyPool.GetObject();
        _enemies.Add(enemy);
        PositionEnemyAroundPlayer(enemy);
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

        foreach (Enemy enemy in _enemies)
        {
            enemy.Health.ObjectDied += ReturnEnemy;
            enemy.Health.EnemyDied += InitiateSpawning;
            _unsubscribeEnemies.Add(enemy);
        }

        ReturnAllEnemies();
        _enemies.Clear();
    }

    private void OnDestroy()
    {
        if (_unsubscribeEnemies.Count > 0)
        {
            foreach (Enemy enemy in _unsubscribeEnemies)
            {
                enemy.Health.ObjectDied -= ReturnEnemy;
                enemy.Health.EnemyDied -= InitiateSpawning;
            }
        }
    }
}