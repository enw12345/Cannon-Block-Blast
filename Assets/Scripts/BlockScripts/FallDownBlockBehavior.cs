using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownBlockBehavior : BlockBehavior
{
    private bool isTouchingGround;
    public bool IsTouchingGround
    {
        get { return isTouchingGround; }
    }
    protected override void Initialize()
    {
        isTouchingGround = false;
    }

    public override void FindNeighborBlocksToDestroy()
    {
        return;
    }

    public override void DestroySelfAndNeighborBlocks()
    {
        return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isTouchingGround = true;
        }
    }
}
