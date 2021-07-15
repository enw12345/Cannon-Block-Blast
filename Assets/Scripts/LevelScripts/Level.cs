using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "Level")]
public class Level : ScriptableObject
{
    public int columns;
    public int rows;

    public Objective[] objectives;
    [NonSerialized] public BlockType[] blockTypes;

    public void InitalizeLevel()
    {
        blockTypes = new BlockType[objectives.Count()];

        for (int i = 0; i < blockTypes.Count(); i++)
        {
            blockTypes[i] = objectives[i].objectiveType.blockType;
        }
    }
}