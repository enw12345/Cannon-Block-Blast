using UnityEngine;

public class BombBullet : Bullet
{
    public float explosionRadius = 5f;

    protected override void HandleDestruction(BlockBehavior blockBehavior)
    {
        Collider[] colliders = Physics.OverlapSphere(blockBehavior.gameObject.transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            Grid.BlocksToDestroy.Add(collider.GetComponent<MeshDestroy>());
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}