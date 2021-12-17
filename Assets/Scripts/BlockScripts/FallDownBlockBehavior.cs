using UnityEngine;

public class FallDownBlockBehavior : BlockBehavior
{
    private bool isTouchingGround;

    protected override void Initialize()
    {
        isTouchingGround = false;
        FallDownBlockType fallDownBlockType = (FallDownBlockType)blockType;
        fallDownBlockType.IsTouchingGround = false;
    }

    public override void FindNeighborBlocksToDestroy()
    {
        return;
    }

    public override void DestroySelfAndNeighborBlocks()
    {
        return;
    }

    public override void FindNeighborBlocksToDestroyRowsAndColumns()
    {
        return;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (!isTouchingGround)
            {
                isTouchingGround = true;
                FallDownBlockType fallDownBlockType = (FallDownBlockType)blockType;
                fallDownBlockType.IsTouchingGround = isTouchingGround;
                BlocksToDestroy.Add(this);
                // DestroyBlock();
            }
        }
    }
}
