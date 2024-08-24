using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomObjectPool<T> where T : MonoBehaviour
{
    private readonly HashSet<T> _availableObjects;
    private readonly Transform _parentTransform;
    private readonly Func<T> _createFunc;

    public CustomObjectPool(Func<T> createFunc, Transform parentTransform = null)
    {
        _createFunc = createFunc ?? throw new ArgumentNullException("createFunc");
        _availableObjects = new HashSet<T>();
        _parentTransform = parentTransform;
    }

    public T Get()
    {
        T obj = null;

        foreach (var item in _availableObjects)
        {
            obj = item;
            break;
        }

        if (obj != null)
        {
            _availableObjects.Remove(obj);
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = _createFunc();
        }

        return obj;
    }

    public void Release(T obj)
    {
        if (_availableObjects.Contains(obj))
            return;

        obj.gameObject.SetActive(false);
        _availableObjects.Add(obj);
    }
}