using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class PlayerPlayUI : MonoBehaviour
{
    private const string Player = "player";

    [SerializeField] private Button _button;

    private void Awake()
    {
        _button.OnClickAsObservable().Subscribe(button =>
        {
            MessageBroker.Default.Publish(new PlaySource(Player));
        });
    }
}