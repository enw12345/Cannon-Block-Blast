using BlockScripts;
using UnityEngine;

namespace BulletScripts
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public abstract class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletType bulletType;

        protected ContactPoint[] ContactPoints = new ContactPoint[1];
        private Rigidbody rigidBody;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out BlockBehavior blockToDestroy)) HandleDestruction(blockToDestroy);

            rigidBody.detectCollisions = false;
        }

        protected abstract void HandleDestruction(BlockBehavior blockToDestroy);
    }
}