using UnityEngine;
using System.Collections.Generic;
using System;

public class LevelManager : MonoBehaviour
{
    public List<Level> Levels;

    public static EventHandler<StartLevelEventArgs> StartLevel;
    public class StartLevelEventArgs : EventArgs
    {
        public Objective[] objectives;
        public int[] objectiveAmounts;
        public Level currentLevel;

        public StartLevelEventArgs(Level level)
        {
            objectives = level.objectives;
            objectiveAmounts = level.objectiveAmounts;
            currentLevel = level;
        }

    }
    private Level currentLevel = null;
    private int currentLevelNum = 0;

    public void SelectLevel(int levelNum)
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

    public void ResetLevel()
    {
        // UIManager.instance.HideButtons();

        // Grid.Instance.CreateGrid(currentLevel.rows, currentLevel.columns, currentLevel.Blocks, currentLevel.newBlockToSpawn);

        StartLevel?.Invoke(this, new StartLevelEventArgs(currentLevel));
    }

    private void InitalizeLevel()
    {
        // UIManager.instance.HideButtons();

        // Grid.Instance.CreateGrid(currentLevel.rows, currentLevel.columns, currentLevel.Blocks, currentLevel.newBlockToSpawn);

        StartLevel?.Invoke(this, new StartLevelEventArgs(currentLevel));
    }

}