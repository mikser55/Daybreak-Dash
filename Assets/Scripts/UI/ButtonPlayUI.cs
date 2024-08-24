using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class ButtonPlayUI : MonoBehaviour
{
    private const string Player = nameof(Player);

    [SerializeField] private Button _button;
    [SerializeField] private Image _pausePanel;

    private void Awake()
    {
        _button.OnClickAsObservable()
            .Subscribe(button =>
        {
            MessageBroker.Default.Publish(new PlaySource(Player));
            _pausePanel.gameObject.SetActive(false);
        })
            .AddTo(this);
    }
}