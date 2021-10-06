using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private BulletType bulletType;
    private Rigidbody rigidBody;

    protected ContactPoint[] contactPoints = new ContactPoint[1];

    protected abstract void HandleDestruction(BlockBehavior blockToDestroy);

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out BlockBehavior blockToDestroy))
        {
            HandleDestruction(blockToDestroy);
        }

        rigidBody.detectCollisions = false;
    }
}