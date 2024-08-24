using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeAbilitiesFiller : MonoBehaviour
{
    private const int UpgradeAmount = 3;

    [SerializeField] private List<Upgrade> _allUpgrages;
    [SerializeField] private List<TextMeshProUGUI> _names;
    [SerializeField] private List<TextMeshProUGUI> _descriptions;
    [SerializeField] private List<Image> _images;

    private void Awake()
    {
        MessageBroker.Default
          .Receive<LevelUpSource>()
          .Subscribe(_ => FillUpgradeAbilities())
          .AddTo(this);

        ResetUpgradesLevel();
    }

    private List<Upgrade> GetRandomUpgrades()
    {
        List<Upgrade> availableUpgrades = _allUpgrages.Where(u => !u.IsMaxLevel).ToList();
        List<Upgrade> selectedUpgrades = new();

        for (int i = 0; i < UpgradeAmount; i++)
        {
            if (availableUpgrades.Count == 0)
                break;

            int randomIndex = Random.Range(0, availableUpgrades.Count);
            selectedUpgrades.Add(availableUpgrades[randomIndex]);
            availableUpgrades.RemoveAt(randomIndex);
        }

        return selectedUpgrades;
    }

    private void FillUpgradeAbilities()
    {
        List<Upgrade> selectedUpgrades = GetRandomUpgrades();

        for (int i = 0; i < selectedUpgrades.Count; i++)
        {
            _names[i].text = selectedUpgrades[i].Name;
            _descriptions[i].text = selectedUpgrades[i].Description;
            _images[i].sprite = selectedUpgrades[i].Sprite;
        }

        MessageBroker.Default.Publish(selectedUpgrades);
    }

    private void ResetUpgradesLevel()
    {
        foreach (Upgrade upgrade in _allUpgrages)
            upgrade.LevelReset();
    }
}