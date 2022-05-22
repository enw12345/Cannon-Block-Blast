using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static EventHandler<StartLevelEventArgs> StartLevel;
        public List<Level> Levels;
        private Level currentLevel;
        private int currentLevelNum;

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

        public class StartLevelEventArgs : EventArgs
        {
            public Level currentLevel;
            public int[] objectiveAmounts;
            public Objective[] objectives;

            public StartLevelEventArgs(Level level)
            {
                objectives = level.objectives;
                objectiveAmounts = level.objectiveAmountsToComplete;
                currentLevel = level;
            }
        }
    }
}