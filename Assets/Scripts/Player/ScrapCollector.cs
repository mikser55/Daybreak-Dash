using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapCollector : MonoBehaviour
{
    private const int Delay = 1;
    private const int MaxColliders = 10;
    private const float MinDistanceToCollect = 0.8f;

    [SerializeField] private float _scrapSpeed = 30f;
    [SerializeField] private float _radius;
    [SerializeField] LayerMask _scrapMask;

    private WaitForSeconds _wait;
    private Collider[] _colliders;

    public event Action<Scrap> Arrived;

    private void Start()
    {
        
        _wait = new WaitForSeconds(Delay);
        _colliders = new Collider[MaxColliders];
        StartCoroutine(FindCoroutine());
    }

    public void IncreaseRadius(float radius)
    {
        if (radius > 0)
            _radius += radius;
    }

    private IEnumerator FindCoroutine()
    {
        while (enabled)
        {
            FindScrap();
            yield return _wait;
        }
    }

    private void FindScrap()
    {
        int collidersNumber = Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders, _scrapMask);

        if (collidersNumber > 0)
        {
            foreach (Collider collider in _colliders)
            {
                if (collider != null && collider.TryGetComponent(out Scrap scrap))
                    StartCoroutine(CollectCoroutine(scrap));
            }
        }
    }

    private IEnumerator CollectCoroutine(Scrap scrap)
    {
        if (scrap.TryGetComponent(out Transform scrapTransform))
        {
            while ((transform.position - scrapTransform.position).sqrMagnitude > MinDistanceToCollect * MinDistanceToCollect)
            {
                scrapTransform.position = Vector3.MoveTowards(scrapTransform.position, transform.position, _scrapSpeed * Time.deltaTime);
                yield return null;
            }

            Arrived?.Invoke(scrap);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}