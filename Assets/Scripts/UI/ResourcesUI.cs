using TMPro;
using UnityEngine;

public class ResourcesUI : MonoBehaviour
{
    [SerializeField] private ResourceManager _resourceManager;
    [SerializeField] private TextMeshProUGUI _scrapText;
    [SerializeField] private TextMeshProUGUI _energyText;

    private void OnEnable()
    {
        _resourceManager.ScrapAmountChanged += UpdateScrapText;
        _resourceManager.EnergyAmountChanged += UpdateEnergyText;
    }

    private void OnDisable()
    {
        _resourceManager.ScrapAmountChanged -= UpdateScrapText;
        _resourceManager.EnergyAmountChanged -= UpdateEnergyText;
    }

    private void Start()
    {
        UpdateEnergyText(0);
        UpdateScrapText(0);
    }

    private void UpdateScrapText(int value)
    {
        _scrapText.text = $"Металлолом: {value}";
    }

    private void UpdateEnergyText(int value)
    {
        _energyText.text = $"Энергия: {value}";
    }
}