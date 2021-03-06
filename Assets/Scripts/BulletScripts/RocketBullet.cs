using BlockScripts;

namespace BulletScripts
{
    public class RocketBullet : Bullet
    {
        protected override void HandleDestruction(BlockBehavior blockToDestroy)
        {
            blockToDestroy.FindNeighborBlocksToDestroyRowsAndColumns();
        }
    }
}