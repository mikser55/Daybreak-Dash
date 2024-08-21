using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextAnimator : MonoBehaviour
{
    private const LoopType YoyoLoopType = LoopType.Yoyo;

    [SerializeField] private float _scaleFactor = 1.2f;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _blinkDuration = 0.2f;

    public void AnimateText(TextMeshProUGUI text)
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(text.transform.DOScale(_scaleFactor, _duration).SetEase(Ease.InOutQuad)
            .SetLoops(int.MaxValue, YoyoLoopType));
        sequence.Join(text.DOColor(Color.red, _blinkDuration)
            .SetLoops(int.MaxValue, YoyoLoopType));

        sequence.Play();
    }
}