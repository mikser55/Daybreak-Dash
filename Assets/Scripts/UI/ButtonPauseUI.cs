using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class ButtonPauseUI : MonoBehaviour
{
    private const string Player = nameof(Player);

    [SerializeField] private Button _button;
    [SerializeField] private Image _pausePanel;

    private void Awake()
    {
        SubscribeActions();
    }

    private void ChangeButtonState()
    {
        _button.gameObject.SetActive(!_button.isActiveAndEnabled);
    }

    private void SubscribeActions()
    {
        _button.OnClickAsObservable()
            .Subscribe(button =>
    {
        MessageBroker.Default.Publish(new PauseSource(Player));
        _pausePanel.gameObject.SetActive(true);
    }).AddTo(this);

        MessageBroker.Default
            .Receive<LevelUpSource>()
            .Subscribe(_ => ChangeButtonState())
            .AddTo(this);

        MessageBroker.Default
            .Receive<ShopSource>()
            .Subscribe(_ => ChangeButtonState())
            .AddTo(this);
    }
}