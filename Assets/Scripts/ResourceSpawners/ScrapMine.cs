using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class ScrapMine : MonoBehaviour
{
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Damaged += DisposeScrap;
    }

    private void OnDisable()
    {
        _health.Damaged -= DisposeScrap;
    }

    private void DisposeScrap()
    {
    }
}