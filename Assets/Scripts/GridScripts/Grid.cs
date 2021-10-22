using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Grid : MonoBehaviour
{
    private bool blocksAreSpawned = false;
    public bool BlocksAreSpawned
    {
        get { return blocksAreSpawned; }
        set { blocksAreSpawned = value; }
    }

    private float zOffset;
    private float yOffset;
    private float spawnHeight;

    public float xSpawnPosition;
    private Bounds blockBounds;

    private Block newBlockToSpawn;

    private float SecondsToWait = 0.1f;

    private void Awake()
    {
        LevelManager.StartLevel += CreateGridFromLevelManager;
    }

    private void FixedUpdate()
    {
        if (BlockBehavior.BlocksToDestroy.Count != 0)
        {
            StartCoroutine(DestroyBlocks());
        }
    }

    private void CreateGridFromLevelManager(object sender, LevelManager.StartLevelEventArgs e)
    {
        ClearGrid();
        StartCoroutine(CreateGridOfBlocksStep(e.currentLevel.rows, e.currentLevel.columns, e.currentLevel.Blocks, e.currentLevel.newBlockToSpawn));
    }

    public void CreateGrid(int rows, int columns, Block[] blocks, Block _newBlockToSpawn)
    {
        ClearGrid();
        StartCoroutine(CreateGridOfBlocksStep(rows, columns, blocks, _newBlockToSpawn));
    }

    private IEnumerator CreateGridOfBlocksStep(int rows, int columns, Block[] blocks, Block _newBlockToSpawn)
    {
        ClearGrid();
        blockBounds = blocks[0].blockPrefab.GetComponent<MeshRenderer>().bounds;
        spawnHeight = rows * blockBounds.size.y;
        int index = 0;
        newBlockToSpawn = _newBlockToSpawn;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                index = y * rows + columns;

                Block block = blocks[index];
                blockBounds = block.blockPrefab.GetComponent<MeshRenderer>().bounds;

                zOffset = -(columns + blockBounds.size.z);
                yOffset = (rows / 2);

                Vector3 spawnPosition = new Vector3(
                xSpawnPosition,
                y * blockBounds.size.y + yOffset,
                x * blockBounds.size.z * 1.035f + transform.position.z + zOffset);

                GameObject currentBlock = Instantiate(block.blockPrefab,
                spawnPosition, Quaternion.identity, transform);

                BlockBehavior blockBehavior = currentBlock.GetComponent<BlockBehavior>();
                blockBehavior.InitializeBlock();
                yield return new WaitForSeconds(SecondsToWait);
            }
        }
    }

    private IEnumerator DestroyBlocks()
    {
        IEnumerable<BlockBehavior> BlocksToDestroy = BlockBehavior.BlocksToDestroy.Distinct();
        List<Vector3> blockPositions = new List<Vector3>();

        foreach (BlockBehavior blockToDestroy in BlocksToDestroy)
        {
            if (blockToDestroy != null)
            {
                Vector3 blockPos = blockToDestroy.transform.position;

                blockToDestroy.DestroyBlock();

                blockPositions.Add(blockToDestroy.transform.position);
            }
        }

        BlockBehavior.BlocksToDestroy.Clear();

        for (int i = blockPositions.Count() - 1; i >= 0; i--)
        {
            SpawnNewBlocks(blockPositions[i], newBlockToSpawn);
            yield return new WaitForSeconds(SecondsToWait);
        }
    }

    private void SpawnNewBlocks(Vector3 blockPosition, Block blockToSpawn)
    {
        Vector3 spawnPos = new Vector3(
            xSpawnPosition,
            spawnHeight + blockPosition.y + blockBounds.size.y,// + yOffset,
            blockPosition.z
        );

        GameObject currentBlock = Instantiate(blockToSpawn.blockPrefab,
        spawnPos, Quaternion.identity, transform);

        BlockBehavior blockBehavior = currentBlock.GetComponent<BlockBehavior>();
        blockBehavior.InitializeBlock();
    }

    private void ClearGrid()
    {
        BlockBehavior[] blocks = FindObjectsOfType<BlockBehavior>();

        if (blocks.Length > 0)
        {
            for (int i = 0; i < blocks.Length; i++)
            {
                GameObject.Destroy(blocks[i].gameObject);
            }
        }
    }
}
