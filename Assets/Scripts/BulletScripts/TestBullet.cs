using BlockScripts;

namespace BulletScripts
{
    public class TestBullet : Bullet
    {
        protected override void HandleDestruction(BlockBehavior blockToDestroy)
        {
            blockToDestroy.FindNeighborBlocksToDestroy();
        }
    }
}