using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BlockScripts;
using Managers;
using UnityEngine;

namespace GridScripts
{
    public class Grid : MonoBehaviour
    {
        public float xSpawnPosition;
        private Bounds blockBounds;

        private Block _newBlockToSpawn;

        private const float SecondsToWait = 0.1f;
        private float spawnHeight;
        private float yOffset;

        private float zOffset;

        public bool BlocksAreSpawned { get; set; } = false;

        private void Awake()
        {
            LevelManager.StartLevel += CreateGridFromLevelManager;
        }

        private void FixedUpdate()
        {
            if (BlockBehavior.BlocksToDestroy.Count != 0) StartCoroutine(DestroyBlocks());
        }

        private void CreateGridFromLevelManager(object sender, LevelManager.StartLevelEventArgs e)
        {
            ClearGrid();
            StartCoroutine(CreateGridOfBlocksStep(e.currentLevel.rows, e.currentLevel.columns, e.currentLevel.Blocks,
                e.currentLevel.newBlockToSpawn));
        }

        private IEnumerator CreateGridOfBlocksStep(int rows, int columns, IReadOnlyList<Block> blocks, Block newBlockToSpawn)
        {
            ClearGrid();
            blockBounds = blocks[0].blockPrefab.GetComponent<MeshRenderer>().bounds;
            spawnHeight = rows * blockBounds.size.y;
            _newBlockToSpawn = newBlockToSpawn;

            for (var y = rows-1; y >= 0; y--)
            for (var x = columns-1; x >= 0; x--)
            {
                var index = y * rows + x;

                var block = blocks[index];

                blockBounds = block.blockPrefab.GetComponent<MeshRenderer>().bounds;

                zOffset = -(columns + blockBounds.size.z);
                yOffset = rows * 0.5f;

                var spawnPosition = new Vector3(
                    xSpawnPosition,
                    (rows - y) * blockBounds.size.y + yOffset,
                    x * blockBounds.size.z * 1.035f + transform.position.z + zOffset);

                var currentBlock = Instantiate(block.blockPrefab,
                    spawnPosition, Quaternion.identity, transform);

                var blockBehavior = currentBlock.GetComponent<BlockBehavior>();
                blockBehavior.InitializeBlock();
                yield return new WaitForSeconds(SecondsToWait);
            }
        }

        private IEnumerator DestroyBlocks()
        {
            var blocksToDestroy = BlockBehavior.BlocksToDestroy.Distinct();
            var blockPositions = new List<Vector3>();

            foreach (var blockToDestroy in blocksToDestroy)
                if (blockToDestroy != null)
                {
                    var blockPos = blockToDestroy.transform.position;

                    blockToDestroy.DestroyBlock();

                    blockPositions.Add(blockToDestroy.transform.position);
                }

            BlockBehavior.BlocksToDestroy.Clear();

            for (var i = blockPositions.Count() - 1; i >= 0; i--)
            {
                SpawnNewBlocks(blockPositions[i], _newBlockToSpawn);
                yield return new WaitForSeconds(SecondsToWait);
            }
        }

        private void SpawnNewBlocks(Vector3 blockPosition, Block blockToSpawn)
        {
            var spawnPos = new Vector3(
                xSpawnPosition,
                spawnHeight + blockPosition.y + blockBounds.size.y, // + yOffset,
                blockPosition.z
            );

            var currentBlock = Instantiate(blockToSpawn.blockPrefab,
                spawnPos, Quaternion.identity, transform);

            var blockBehavior = currentBlock.GetComponent<BlockBehavior>();
            blockBehavior.InitializeBlock();
        }

        private static void ClearGrid()
        {
            var blocks = FindObjectsOfType<BlockBehavior>();

            if (blocks.Length <= 0) return;
            for (var i = 0; i < blocks.Length; i++)
                Destroy(blocks[i].gameObject);
        }
    }
}