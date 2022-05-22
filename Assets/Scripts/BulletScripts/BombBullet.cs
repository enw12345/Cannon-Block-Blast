using BlockScripts;
using UnityEngine;

namespace BulletScripts
{
    public class BombBullet : Bullet
    {
        public float explosionRadius = 5f;

        protected override void HandleDestruction(BlockBehavior blockToDestroy)
        {
            var colliders = Physics.OverlapSphere(blockToDestroy.gameObject.transform.position, explosionRadius);

            foreach (var collider in colliders)
                if (collider.GetComponent<BlockBehavior>() is FallDownBlockBehavior)
                    continue;
                else
                    BlockBehavior.BlocksToDestroy.Add(collider.GetComponent<BlockBehavior>());
        }
    }
}