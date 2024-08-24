using System.Collections.Generic;
using UnityEngine;

public class ScrapObjectPool : MonoBehaviour
{
    [SerializeField] private List<Scrap> _scrapPrefabs;

    private CustomObjectPool<Scrap> _scraps;

    private void Awake()
    {
        _scraps = new(CreateObject);
    }

    public Scrap GetScrap()
    {
        return _scraps.Get();
    }

    public void ReturnScrap(Scrap scrap)
    {
        _scraps.Release(scrap);
    }

    private Scrap GetRandomScrap()
    {
        return _scrapPrefabs[Random.Range(0, _scrapPrefabs.Count)];
    }

    private Scrap CreateObject()
    {
        Scrap obj = Object.Instantiate(GetRandomScrap());
        return obj;
    }
}