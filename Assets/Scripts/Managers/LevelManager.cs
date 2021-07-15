using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public List<Level> Levels;
    public ObjectivesManager objectivesManager;

    public void StartLevel(Level level)
    {
        objectivesManager.InitializeObjectives(level.objectives);
        StartCoroutine(Grid.Instance.CreateGridOfBlocksStep(level.rows, level.columns, level.blockTypes));
    }
}