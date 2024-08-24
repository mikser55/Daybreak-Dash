using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public float StatValue { get; private set; }
    [field: SerializeField] public int CurrentLevel { get; protected set; }
    [field: SerializeField] public int MaxLevel { get; private set; } = 5;
    public bool IsMaxLevel => CurrentLevel == MaxLevel;

    public abstract void ApplyUpgrade(Player player);
    public virtual void IncreaseLevel()
    {
        if (CurrentLevel + 1 > MaxLevel)
            return;

        CurrentLevel++;
    }

    public void LevelReset()
    {
        CurrentLevel = 0;
    }
}