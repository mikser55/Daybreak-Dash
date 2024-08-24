using UnityEngine;
using UniRx;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private void Awake()
    {
        MessageBroker.Default
            .Receive<LevelUpSource>()
            .Subscribe(_ => ShowUpgrages())
            .AddTo(this);

        MessageBroker.Default
            .Receive<UpgradeAppliedSource>()
            .Subscribe(_ => HideUpgrades())
            .AddTo(this);
    }

    private void ShowUpgrages()
    {
        _canvas.gameObject.SetActive(true);
    }

    private void HideUpgrades()
    {
        _canvas.gameObject.SetActive(false);
    }
}