using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Level")]
[Serializable]
public class Level : ScriptableObject
{
    public int columns;
    public int rows;

    [SerializeField] public Objective[] objectives;
    [SerializeField] public int[] objectiveAmounts;
    [SerializeField] public Block[] Blocks;
    [SerializeField] public bool spawnNewColorBlocks;
    // [SerializeField] public Block[] newBlocksToSpawn;
    [SerializeField] public Block newBlockToSpawn;
}