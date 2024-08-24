using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeApplier : MonoBehaviour
{
    private const string LevelUp = nameof(LevelUp);

    [SerializeField] private Player _player;
    [SerializeField] private List<Button> _upgradeButtons;

    private void Start()
    {
        MessageBroker.Default
            .Receive<List<Upgrade>>()
            .Subscribe(ApplyUpgrades)
            .AddTo(this);
    }

    private void ApplyUpgrades(List<Upgrade> selectedUpgrades)
    {
        for (int i = 0; i < selectedUpgrades.Count; i++)
        {
            if (i < _upgradeButtons.Count)
            {
                int index = i;
                Upgrade upgrade = selectedUpgrades[index];

                _upgradeButtons[index].onClick.RemoveAllListeners();

                _upgradeButtons[index].onClick.AddListener(() => ApplyUpgrade(upgrade));
            }
        }
    }

    private void ApplyUpgrade(Upgrade upgrade)
    {
        upgrade.ApplyUpgrade(_player);
        upgrade.IncreaseLevel();

        MessageBroker.Default.Publish(new UpgradeAppliedSource());
        MessageBroker.Default.Publish(new PlaySource(LevelUp));
    }
}