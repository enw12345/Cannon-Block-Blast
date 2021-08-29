﻿using UnityEngine;

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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Collider Touched Ground");
            isTouchingGround = true;
            BlocksToDestroy.Add(this.GetComponent<MeshDestroy>());
        }
    }
}
