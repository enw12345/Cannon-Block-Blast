using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Grid : MonoBehaviour
{
    private static Grid instance;
    public static Grid Instance
    {
        get { return instance; }
    }

    public Block block;

    private bool blocksAreSpawned = false;
    public bool BlocksAreSpawned
    {
        get { return blocksAreSpawned; }
        set { blocksAreSpawned = value; }
    }
    private float zOffset;
    private float yOffset;
    private static float spawnHeight;

    public float xSpawnPosition;
    private Bounds blockBounds;

    public static event EventHandler<OnBlockDestroyedEventArgs> OnBlockDestroyed;
    public class OnBlockDestroyedEventArgs : EventArgs
    {
        public BlockBehavior blockBehavior1;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        blockBounds = block.blockPrefab.GetComponent<MeshRenderer>().bounds;
    }

    private void FixedUpdate()
    {
        if (BlockBehavior.BlocksToDestroy.Count != 0)
        {
            DestroyBlocks();
        }
    }

    public IEnumerator CreateGridOfBlocksStep(int rows, int columns, BlockType[] blocks)
    {
        blockBounds = block.blockPrefab.GetComponent<MeshRenderer>().bounds;
        zOffset = -(columns + blockBounds.size.z);
        yOffset = (rows / 2);
        spawnHeight = rows;

        // for (int y = 0; y < rows; y++)
        // {
        //     for (int x = 0; x < columns; x++)
        //     {
        //         Vector3 spawnPosition = new Vector3(
        //         xSpawnPosition,
        //         y * blockBounds.size.y + yOffset,
        //         x * blockBounds.size.z * 1.035f + transform.position.z + zOffset);

        //         GameObject currentBlock = Instantiate(block.blockPrefab,
        //         spawnPosition, Quaternion.identity, transform);

        //         BlockBehavior blockBehavior = currentBlock.GetComponent<BlockBehavior>();
        //         blockBehavior.InitializeBlock(block.blockType);
        //         yield return new WaitForSeconds(0.05f);
        //     }
        // }
        
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                Vector3 spawnPosition = new Vector3(
                xSpawnPosition,
                y * blockBounds.size.y + yOffset,
                x * blockBounds.size.z * 1.035f + transform.position.z + zOffset);

                GameObject currentBlock = Instantiate(block.blockPrefab,
                spawnPosition, Quaternion.identity, transform);

                BlockBehavior blockBehavior = currentBlock.GetComponent<BlockBehavior>();
                blockBehavior.InitializeBlock(block.blockType);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    private void DestroyBlocks()
    {
        IEnumerable<MeshDestroy> BlocksToDestroy = BlockBehavior.BlocksToDestroy.Distinct();
        List<Vector3> blockPositions = new List<Vector3>();

        BlockBehavior blockBehavior;

        foreach (MeshDestroy blockToDestroy in BlocksToDestroy)
        {
            if (blockToDestroy != null)
            {
                Vector3 blockPos = blockToDestroy.transform.position;
                blockBehavior = blockToDestroy.GetComponent<BlockBehavior>();

                OnBlockDestroyed?.Invoke(this, new OnBlockDestroyedEventArgs { blockBehavior1 = blockBehavior });

                blockToDestroy.DestroyMesh(2);

                blockPositions.Add(blockBehavior.transform.position);
            }
        }

        BlockBehavior.BlocksToDestroy.Clear();
        StartCoroutine(SpawnNewBlocksStep(blockPositions));
    }

    private IEnumerator SpawnNewBlocksStep(List<Vector3> blockPositions)
    {
        foreach (Vector3 blockPosition in blockPositions)
        {
            Vector3 spawnPos = new Vector3(
                xSpawnPosition,
                spawnHeight * blockBounds.size.y + yOffset + blockPosition.y,
                blockPosition.z
            );

            GameObject currentBlock = Instantiate(block.blockPrefab,
            spawnPos, Quaternion.identity, transform);

            BlockBehavior blockBehavior = currentBlock.GetComponent<BlockBehavior>();
            blockBehavior.InitializeBlock(block.blockType);

            yield return new WaitForSeconds(0.05f);
        }
    }
}
