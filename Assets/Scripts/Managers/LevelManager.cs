using UnityEngine;
using System.Collections.Generic;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> Levels = new List<Level>();
    [SerializeField] private ObjectivesManager objectivesManager = null;
    private Level currentLevel = null;
    private int currentLevelNum = 0;

    public void StartLevel(int levelNum)
    {
        currentLevelNum = levelNum;
        currentLevel = Levels[currentLevelNum];
        InitalizeLevel();
    }

    public void NextLevel()
    {
        if (currentLevelNum + 1 < Levels.Count)
        {
            currentLevelNum++;
            currentLevel = Levels[currentLevelNum];
            InitalizeLevel();
        }
        else
        {
            currentLevelNum = 0;
            currentLevel = Levels[currentLevelNum];
            InitalizeLevel();
        }
    }

    private void InitalizeLevel()
    {
        objectivesManager.ResetObjectives();
        objectivesManager.InitializeObjectives(currentLevel.objectives, currentLevel.objectiveAmounts);
        Grid.Instance.CreateGrid(currentLevel.rows, currentLevel.columns, currentLevel.Blocks);
    }
}