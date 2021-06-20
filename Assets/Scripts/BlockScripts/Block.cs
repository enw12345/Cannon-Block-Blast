using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Block Object", menuName = "Block Object")]
public class Block : ScriptableObject
{
    [Header("Block Data")]
    public BlockType blockType;
    public GameObject blockPrefab;

    [Header("Block UI Data")]
    public int BlockSelectionNumber;
}