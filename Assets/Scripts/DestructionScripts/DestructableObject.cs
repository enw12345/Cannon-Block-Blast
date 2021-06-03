using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshDestroy))]
public class DestructableObject : MonoBehaviour
{
    public void DestructOnContact(ContactPoint[] contactPoints, int DestroyAmount)
    {
        MeshDestroy meshDestroy = gameObject.GetComponent<MeshDestroy>();

        ContactPoint point = contactPoints[0];
        Vector3 planeNormal = Vector3.Cross(point.normal, point.point).normalized;

        Plane plane = new Plane(planeNormal, point.point);

        meshDestroy.DestroyMeshOnContact(point.point, point.normal, DestroyAmount);
    }

    public void Destruct(int DestroyAmount)
    {
        MeshDestroy meshDestroy = gameObject.GetComponent<MeshDestroy>();

        meshDestroy.DestroyMesh(DestroyAmount);
    }
}
