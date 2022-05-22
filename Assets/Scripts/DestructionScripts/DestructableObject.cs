using UnityEngine;

namespace DestructionScripts
{
    [RequireComponent(typeof(MeshDestroy))]
    public class DestructableObject : MonoBehaviour
    {
        public void DestructOnContact(ContactPoint[] contactPoints, int destroyAmount)
        {
            var meshDestroy = gameObject.GetComponent<MeshDestroy>();

            var point = contactPoints[0];
            var planeNormal = Vector3.Cross(point.normal, point.point).normalized;

            var plane = new Plane(planeNormal, point.point);

            meshDestroy.DestroyMeshOnContact(point.point, point.normal, destroyAmount);
        }
    }
}