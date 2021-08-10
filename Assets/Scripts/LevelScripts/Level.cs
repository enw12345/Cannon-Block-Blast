using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Level")]
[Serializable]
public class Level : ScriptableObject
{
    public int columns;
    public int rows;

    [SerializeField] public Objective[] objectives;
    [NonSerialized] public BlockType[] blockTypes;
    [SerializeField] public Block[] Blocks;
}