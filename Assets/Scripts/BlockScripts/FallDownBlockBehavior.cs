using BlockScripts;
using UnityEngine;

public class FallDownBlockBehavior : BlockBehavior
{
    private bool isTouchingGround;

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Ground")) return;
        if (isTouchingGround) return;
        isTouchingGround = true;
        var fallDownBlockType = (FallDownBlockType) blockType;
        fallDownBlockType.IsTouchingGround = isTouchingGround;
        BlocksToDestroy.Add(this);
    }

    protected override void Initialize()
    {
        isTouchingGround = false;
        var fallDownBlockType = (FallDownBlockType) blockType;
        fallDownBlockType.IsTouchingGround = false;
    }

    public override void FindNeighborBlocksToDestroy()
    {
    }

    public override void DestroySelfAndNeighborBlocks()
    {
    }

    public override void FindNeighborBlocksToDestroyRowsAndColumns()
    {
    }
}