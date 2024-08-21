using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class TimeOfDayUI : MonoBehaviour
{
    private const int RedZone = 3;
    private const string NightText = "NIGHT";

    [SerializeField] private DayTimer _timer;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextAnimator _textAnimator;

    private bool _animationIsStarted;

    private void Awake()
    {
        ChangeText(_timer.StartTime);

    }
    private void OnEnable()
    {
        _timer.SecondSpent += ChangeText;
    }

    private void OnDisable()
    {
        _timer.SecondSpent -= ChangeText;
    }

    private void ChangeText(int value)
    {
        if (value <= RedZone && _animationIsStarted == false)
        {
            _animationIsStarted = true;
            _textAnimator.AnimateText(_timerText);
        }

        _timerText.text = value.ToString();

        if (value == 0)
            _timerText.text = NightText;
    }
}