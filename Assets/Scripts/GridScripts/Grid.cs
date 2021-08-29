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

    private Block newBlockToSpawn;

    private float SecondsToWait = 0.1f;
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
    }

    private void FixedUpdate()
    {
        if (BlockBehavior.BlocksToDestroy.Count != 0)
        {
            StartCoroutine(DestroyBlocks());
        }
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
                blockBehavior.InitializeBlock(block.blockType);
                yield return new WaitForSeconds(SecondsToWait);
            }
        }
    }

    private IEnumerator DestroyBlocks()
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

        for (int i = 0; i < blockPositions.Count(); i++)
        {
            SpawnNewBlocks(blockPositions[i], newBlockToSpawn);
            yield return new WaitForSeconds(SecondsToWait);
        }
    }

    private void SpawnNewBlocks(Vector3 blockPosition, Block blockToSpawn)
    {
        Vector3 spawnPos = new Vector3(
            xSpawnPosition,
            spawnHeight + blockBounds.size.y,
            blockPosition.z
        );

        GameObject currentBlock = Instantiate(blockToSpawn.blockPrefab,
        spawnPos, Quaternion.identity, transform);

        BlockBehavior blockBehavior = currentBlock.GetComponent<BlockBehavior>();
        blockBehavior.InitializeBlock(blockToSpawn.blockType);
    }

    private void ClearGrid()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        Debug.Log(blocks.Length);

        if (blocks.Length > 0)
        {
            for (int i = 0; i < blocks.Length; i++)
            {
                GameObject.Destroy(blocks[i]);
            }
        }
    }
}
