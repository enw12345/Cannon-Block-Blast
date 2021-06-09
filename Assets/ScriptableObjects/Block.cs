using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Block Object", menuName = "Block Object")]
public class Block : ScriptableObject
{
    [Header("Block Data")]
    public BlockType blockType;
    public GameObject blockPrefab;

    [Header("Block UI Data")]
    public Sprite BlockUI;
    public int BlockSelectionNumber;

    public void InitBlock(BlockBehavior block)
    {
        block.InitializeBlock(blockType);
    }
}