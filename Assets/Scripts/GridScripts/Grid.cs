using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private static Grid instance;
    public static Grid Instance
    {
        get { return instance; }
    }

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
    private List<GameObject> blocks = new List<GameObject>();

    private bool blocksAreSpawned = false;

    private float zOffset;
    private float yOffset;

    public float xSpawnPosition;
    private Bounds blockBounds;

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

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isStarted && !blocksAreSpawned)
        {
            StartCoroutine(CreateGridOfBlocksStep());
            blocksAreSpawned = true;
        }
    }

    private IEnumerator CreateGridOfBlocksStep()
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

                BlockBehavior blockBehavior = currentBlock.GetComponent<BlockBehavior>();
                blockBehavior.InitializeBlock();

                blocks.Add(currentBlock);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    private IEnumerator SpawnNewBlocks()
    {

        yield return new WaitForSeconds(0.05f);
    }
}
