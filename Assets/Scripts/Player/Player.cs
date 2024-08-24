using System;
using UniRx;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const string LevelUp = nameof(LevelUp);
    private const int FirstLevelExp = 100;

    [SerializeField] private float _levelMultiplier = 1.2f;

    public event Action ExperienceChanged;

    public int ExperienceToNextLevel { get; private set; }
    public int CurrentLevel { get; private set; } = 1;
    public int CurrentExperience { get; private set; }


    public void Start()
    {
        ExperienceToNextLevel = CalculateExperienceForNextLevel(CurrentLevel);
    }

    public void AddExperience(int amount)
    {
        if (amount > 0)
        {
            CurrentExperience += amount;

            if (CurrentExperience >= ExperienceToNextLevel)
                IncreaseLevel();

            ExperienceChanged?.Invoke();
        }
    }

    private void IncreaseLevel()
    {
        CurrentExperience -= ExperienceToNextLevel;
        CurrentLevel++;
        MessageBroker.Default.Publish(new PauseSource(LevelUp));
        MessageBroker.Default.Publish(new LevelUpSource());
        ExperienceToNextLevel = CalculateExperienceForNextLevel(CurrentLevel);

        if (CurrentExperience >= ExperienceToNextLevel)
            IncreaseLevel();
    }

    private int CalculateExperienceForNextLevel(int level)
    {
        int levelProgression = level - 1;
        return Mathf.RoundToInt(FirstLevelExp * Mathf.Pow(_levelMultiplier, levelProgression));
    }
}