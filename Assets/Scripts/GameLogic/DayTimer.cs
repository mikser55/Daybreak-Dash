using System;
using System.Collections;
using UnityEngine;
using UniRx;

public class DayTimer : MonoBehaviour
{
    private const int Delay = 1;

    private int _currentTime;
    private WaitForSeconds _wait;

    public ReactiveProperty<int> _reactiveCurrentTime;
    public event Action<int> SecondSpent;

    [field: SerializeField] public int StartTime { get; private set; } = 30;

    private void Start()
    {
        _wait = new(Delay);
        _currentTime = StartTime;
        _reactiveCurrentTime = new(_currentTime);
        StartCoroutine(DayCoroutine());
    }

    private IEnumerator DayCoroutine()
    {
        while (_currentTime > 0)
        {
            _currentTime--;
            _reactiveCurrentTime.Value = _currentTime;
            SecondSpent?.Invoke(_currentTime);

            yield return _wait;
        }
    }
}