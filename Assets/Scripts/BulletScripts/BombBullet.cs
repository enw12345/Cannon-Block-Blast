using UnityEngine;

public class BombBullet : Bullet
{
    public float explosionRadius = 5f;

    protected override void HandleDestruction(BlockBehavior blockToDestroy)
    {
        Collider[] colliders = Physics.OverlapSphere(blockToDestroy.gameObject.transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<BlockBehavior>() is FallDownBlockBehavior)
                continue;
            else
                BlockBehavior.BlocksToDestroy.Add(collider.GetComponent<MeshDestroy>());
        }
    }
}