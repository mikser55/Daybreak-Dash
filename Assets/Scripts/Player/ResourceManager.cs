using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] ScrapMine _scrapMine;
    [SerializeField] EnergyMine _energyMine;
    [SerializeField] private int _maxEnergyValue = 10;
    [SerializeField] private int _maxScrapValue = 100;

    private int _currentScrap;
    private int _currentEnergy;

    public event Action<int> ScrapAmountChanged;
    public event Action<int> EnergyAmountChanged;

    private void OnEnable()
    {
        _energyMine.ResourceCollected += AddEnergy;
    }

    private void OnDisable()
    {
        _energyMine.ResourceCollected -= AddEnergy;
        
    }

    private void AddScrap()
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
