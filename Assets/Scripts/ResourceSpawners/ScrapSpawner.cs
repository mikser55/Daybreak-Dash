using UnityEngine;

public class ScrapSpawner : MonoBehaviour
{
    [SerializeField] private ScrapObjectPool _pool;
    [SerializeField] private ScrapCollector _collector;
    [SerializeField] private float _initialForce = 5f;
    [SerializeField] private float _upwardForce = 2f;

    private void OnEnable()
    {
        _collector.Arrived += ReturnScrap;
    }

    private void OnDisable()
    {
        _collector.Arrived -= ReturnScrap;
    }

    public Scrap SpawnScrap(Vector3 spawnPosition)
    {
        Scrap scrap = _pool.GetScrap();
        scrap.transform.position = spawnPosition;

        if (scrap.TryGetComponent(out Rigidbody rigidbody))
        {
            Vector3 randomDirection = Random.insideUnitSphere;
            randomDirection.y = Mathf.Abs(randomDirection.y);
            randomDirection = randomDirection.normalized;

            rigidbody.AddForce((randomDirection * _initialForce) + (Vector3.up * _upwardForce), ForceMode.Impulse);
        }

        return scrap;
    }

    private void ReturnScrap(Scrap scrap)
    {
        _pool.ReturnScrap(scrap);
    }
}