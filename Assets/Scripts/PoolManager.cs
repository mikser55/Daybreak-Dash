using System.Collections.Generic;
using UnityEngine;

public abstract class PoolManager<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private List<T> _prefabs;

    private CustomObjectPool<T> _objects;

    private void Awake()
    {
        _objects = InitializePool();
    }

    public T GetObject()
    {
        return _objects.Get();
    }

    public void ReturnObject(T item)
    {
        _objects.Release(item);
    }

    protected T GetRandomObject()
    {
        return _prefabs[Random.Range(0, _prefabs.Count)];
    }

    protected T CreateObject()
    {
        T obj = Object.Instantiate(GetRandomObject());

        return obj;
    }

    private CustomObjectPool<T> InitializePool()
    {
        return new CustomObjectPool<T>(CreateObject);
    }
}