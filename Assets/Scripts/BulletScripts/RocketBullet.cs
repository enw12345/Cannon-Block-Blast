using UnityEngine;

public class RocketBullet : Bullet
{
    protected override void HandleDestruction(BlockBehavior blockToDestroy)
    {
        blockToDestroy.FindNeighborBlocksToDestroyRowsAndColumns();
    }
}