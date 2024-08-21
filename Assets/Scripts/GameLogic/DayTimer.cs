using System;
using System.Collections;
using UnityEngine;

public class DayTimer : MonoBehaviour
{
    private const int Delay = 1;

    private int _currentTime;
    private WaitForSeconds _wait;

    public event Action<int> SecondSpent;
    public event Action NightFallen;

    [field: SerializeField] public int StartTime { get; private set; } = 30;

    private void Start()
    {
        _wait = new(Delay);
        _currentTime = StartTime;
        StartCoroutine(DayCoroutine());
    }

    private IEnumerator DayCoroutine()
    {
        while (_currentTime > 0)
        {
            _currentTime--;
            SecondSpent?.Invoke(_currentTime);

            yield return _wait;
        }

        if (_currentTime == 0)
            NightFallen?.Invoke();
    }
}