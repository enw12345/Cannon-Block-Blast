using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public abstract class BlockBehavior : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask = 0;
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

    public static List<BlockBehavior> BlocksToDestroy = new List<BlockBehavior>();

    [SerializeField] protected BlockType blockType;

    public BlockType BlockType
    {
        get { return blockType; }
    }

    public void InitializeBlock()
    {
        Initialize();
    }

    public static event EventHandler<OnBlockDestroyedEventArgs> OnBlockDestroyed;
    public class OnBlockDestroyedEventArgs : EventArgs
    {
        public BlockBehavior blockBehavior1;
    }

    public virtual void DestroyBlock()
    {
        OnBlockDestroyed?.Invoke(this, new OnBlockDestroyedEventArgs { blockBehavior1 = this });
        GetComponent<MeshDestroy>().DestroyMesh(2);
    }

    protected abstract void Initialize();

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
            BlocksToDestroy.Add(block);
        }
        foreach (BlockBehavior block in rightBlocks)
        {
            BlocksToDestroy.Add(block);
        }
        foreach (BlockBehavior block in downBlocks)
        {
            BlocksToDestroy.Add(block);
        }
        foreach (BlockBehavior block in upBlocks)
        {
            BlocksToDestroy.Add(block);
        }

        BlocksToDestroy.Add(this);
    }

    protected BlockBehavior FindBlockThroughRay(Vector3 castDirection)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, castDirection, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.gameObject.GetComponent<BlockBehavior>() && VerifyBlockType(hit.collider.gameObject.GetComponent<BlockBehavior>().BlockType))
                return hit.collider.gameObject.GetComponent<BlockBehavior>();
        }

        return null;
    }

    protected BlockBehavior[] FindBlocksThroughRay(Vector3 castDirection)
    {
        RaycastHit[] hits;

        hits = Physics.RaycastAll(transform.position, castDirection, Mathf.Infinity, layerMask).OrderBy(h => h.distance).ToArray();

        List<BlockBehavior> blocks = new List<BlockBehavior>(hits.Length);
        BlockBehavior potentialBlock;

        for (int i = 0; i < hits.Length; i++)
        {
            potentialBlock = hits[i].collider.gameObject.GetComponent<BlockBehavior>();

            if (i == 0 && hits[i].distance > 2.5f || !VerifyBlockType(potentialBlock.BlockType))
                break;
            else
                blocks.Add(hits[i].collider.gameObject.GetComponent<BlockBehavior>());

        }

        return blocks.ToArray();
    }

    private bool VerifyBlockType(BlockType blockTypeToCheck)
    {
        if (blockType == blockTypeToCheck)
            return true;

        return false;
    }
}