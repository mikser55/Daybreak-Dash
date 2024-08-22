using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class ScrapMine : MonoBehaviour
{
    [SerializeField] private int _experienceValue = 10;

    private Player _player;
    private Health _health;

    public event Action<ScrapMine> Destroyed;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Damaged += DropScrap;
        _health.Damaged += GiveExperience;
    }

    private void OnDisable()
    {
        _health.Damaged -= GiveExperience;
    }

    public void Initialize(Player player)
    {
        _player = player;
    }

    private void DropScrap()
    {

    } 

    private void GiveExperience()
    {
        _player.AddExperience(_experienceValue);
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke(this);
    }
}