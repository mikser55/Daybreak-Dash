using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] EnergyMine _energyMine;
    [SerializeField] private int _maxEnergyValue = 10;
    [SerializeField] private int _maxScrapValue = 100;
    [SerializeField] private ScrapCollector _scrapCollector;

    private int _currentScrap;
    private int _currentEnergy;

    public event Action<int> ScrapAmountChanged;
    public event Action<int> EnergyAmountChanged;

    private void OnEnable()
    {
        _scrapCollector.Arrived += AddScrap;
        _energyMine.ResourceCollected += AddEnergy;
    }

    private void OnDisable()
    {
        _scrapCollector.Arrived -= AddScrap;
        _energyMine.ResourceCollected -= AddEnergy;

    }

    private void AddScrap(Scrap _)
    {
        if (_currentScrap + 1 > _maxScrapValue)
            return; //сделать UI ошибку

        _currentScrap++;
        ScrapAmountChanged?.Invoke(_currentScrap);
    }

    private void AddEnergy()
    {
        if (_currentEnergy + 1 > _maxEnergyValue)
            return; //сделать UI ошибку

        _currentEnergy++;
        EnergyAmountChanged?.Invoke(_currentEnergy);
    }
}
