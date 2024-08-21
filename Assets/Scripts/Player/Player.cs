using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const int FirstLevelExp = 100;

    [SerializeField] private float _levelMultiplier = 1.2f;

    public event Action ExperienceChanged;

    public int ExperienceToNextLevel { get; private set; }
    public int CurrentLevel { get; private set; } = 1;
    public int CurrentExperience { get; private set; }


    public void Start()
    {
        ExperienceToNextLevel = CalculateExperienceForNextLevel(CurrentLevel);
        AddExperience(5);
    }

    public void AddExperience(int amount)
    {
        if (amount > 0)
        {
            CurrentExperience += amount;
            ExperienceChanged?.Invoke();

            if (CurrentExperience >= ExperienceToNextLevel)
            {
                LevelUp();
            }
        }
    }

    private void LevelUp()
    {
        CurrentExperience -= ExperienceToNextLevel;
        CurrentLevel++;
        ExperienceToNextLevel = CalculateExperienceForNextLevel(CurrentLevel);

        if (CurrentExperience >= ExperienceToNextLevel)
            LevelUp();
    }

    private int CalculateExperienceForNextLevel(int level)
    {
        int levelProgression = level - 1;
        return Mathf.RoundToInt(FirstLevelExp * Mathf.Pow(_levelMultiplier, levelProgression));
    }
}