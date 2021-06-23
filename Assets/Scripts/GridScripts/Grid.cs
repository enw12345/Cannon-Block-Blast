using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SocialPlatforms.GameCenter;

public class Grid : MonoBehaviour
{
    private static Grid instance;
    public static Grid Instance
    {
        get { return instance; }
    }

    [Header("Block Spawn Properties")]
    [SerializeField]
    private int rows = 4, columns = 4;

    public static int xSizeIndex
    {
        get { return instance.rows - 1; }
    }
    public static int ySizeIndex
    {
        get { return instance.columns - 1; }
    }
    public static int GridSize
    {
        get { return instance.rows * instance.columns; }
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
        zOffset = -(columns + blockBounds.size.z);
        yOffset = (rows / 2);
    }

    private void FixedUpdate()
    {
        if (BlockBehavior.BlocksToDestroy.Count != 0)
        {
            StartCoroutine(DestroyBlocksStep());
        }
    }

    public IEnumerator CreateGridOfBlocksStep()
    {
        blockBounds = block.blockPrefab.GetComponent<MeshRenderer>().bounds;

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

                BlockBehavior blockBehavior = currentBlock.GetComponent<BlockBehavior>(); ;
                blockBehavior.InitializeBlock(block.blockType);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    private IEnumerator DestroyBlocksStep()
    {
        BlockBehavior blockBehavior;
        foreach (MeshDestroy blockToDestroy in BlockBehavior.BlocksToDestroy)
        {
            if (block != null && blockToDestroy != null)
            {
                Vector3 blockPos = blockToDestroy.transform.position;
                blockBehavior = blockToDestroy.GetComponent<BlockBehavior>();

                OnBlockDestroyed?.Invoke(this, new OnBlockDestroyedEventArgs { blockBehavior1 = blockBehavior });

                blockToDestroy.DestroyMesh(2);

                SpawnNewBlocks(blockPos);
            }
            yield return new WaitForSeconds(0.05f);
        }

        BlockBehavior.BlocksToDestroy.Clear();
    }

    private void SpawnNewBlocks(Vector3 blockPosition)
    {
        Vector3 spawnPos = new Vector3(
            xSpawnPosition,
            rows * blockBounds.size.y + yOffset,
            blockPosition.z// * blockBounds.size.z * 1.035f + transform.position.z + zOffset
        );

        GameObject currentBlock = Instantiate(block.blockPrefab,
        spawnPos, Quaternion.identity, transform);

        BlockBehavior blockBehavior = currentBlock.GetComponent<BlockBehavior>();
        blockBehavior.InitializeBlock(block.blockType);

        // yield return new WaitForSeconds(0.05f);
    }
}
