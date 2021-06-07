using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BlockBehavior : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private bool isSetToBeDestroyed = false;

    public bool IsSetToBeDestroyed
    {
        get { return isSetToBeDestroyed; }
        set { isSetToBeDestroyed = value; }
    }

    private static int SpaceBetweenEachBlock
    {
        set { SpaceBetweenEachBlock = value; }
    }

    public void InitializeBlock()
    {
        Initialize();
        BlockManager.Blocks.Add(this);
    }

    public abstract void Initialize();

    public abstract void DestroySelfAndNeighborBlocks();

    public abstract void FindNeighborBlocksToDestroy();

    public virtual void FindNeighborBlocksToDestroyRowsAndColumns()
    {
        BlockBehavior[] leftBlocks = FindBlocksThroughRay(-transform.forward);
        BlockBehavior[] rightBlocks = FindBlocksThroughRay(transform.forward);
        BlockBehavior[] downBlocks = FindBlocksThroughRay(-transform.up);
        BlockBehavior[] upBlocks = FindBlocksThroughRay(transform.up);

        foreach (BlockBehavior block in leftBlocks)
        {
            BlockManager.BlocksToDestroy.Add(block.gameObject.GetComponent<MeshDestroy>());
        }
        foreach (BlockBehavior block in rightBlocks)
        {
            BlockManager.BlocksToDestroy.Add(block.gameObject.GetComponent<MeshDestroy>());
        }
        foreach (BlockBehavior block in downBlocks)
        {
            BlockManager.BlocksToDestroy.Add(block.gameObject.GetComponent<MeshDestroy>());
        }
        foreach (BlockBehavior block in upBlocks)
        {
            BlockManager.BlocksToDestroy.Add(block.gameObject.GetComponent<MeshDestroy>());
        }

        BlockManager.BlocksToDestroy.Add(this.GetComponent<MeshDestroy>());
    }

    protected BlockBehavior FindBlockThroughRay(Vector3 castDirection)
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, castDirection, out hit);
        BlockBehavior potentialBlock = null;

        if (hit.collider != null && hit.distance < 2)
            potentialBlock = hit.collider.gameObject.GetComponent<BlockBehavior>();

        if (potentialBlock != null)
        {
            return potentialBlock;
        }

        return null;
    }

    protected BlockBehavior[] FindBlocksThroughRay(Vector3 castDirection)
    {
        RaycastHit[] hits;

        hits = Physics.RaycastAll(transform.position, castDirection, Mathf.Infinity, layerMask).OrderBy(h => h.distance).ToArray();

        List<BlockBehavior> blocks = new List<BlockBehavior>(hits.Length);

        int index = 0;
        foreach (RaycastHit hit in hits)
        {
            //Check if there is a space between the blocks
            if (index == 0 && hit.distance >= 2)
                break;
            else
            {
                BlockBehavior block = hit.collider.gameObject.GetComponent<BlockBehavior>();
                blocks.Add(block);
            }

            index++;
        }

        return blocks.ToArray();
    }
}