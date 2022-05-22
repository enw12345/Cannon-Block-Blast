﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DestructionScripts
{
    [RequireComponent(typeof(DestructableObject))]
    public class MeshDestroy : MonoBehaviour
    {
        public float ExplodeForce;
        public bool createNewMeshDestroy;
        private Plane edgePlane;
        private bool edgeSet;
        private Vector2 edgeUV = Vector2.zero;
        private Vector3 edgeVertex = Vector3.zero;

        public void DestroyMeshOnContact(Vector3 contactPoint, Vector3 contactPointNormal, int cutAmount)
        {
            var originalMesh = GetComponent<MeshFilter>().mesh;
            originalMesh.RecalculateBounds();
            var parts = new List<PartMesh>();
            var subParts = new List<PartMesh>();

            var mainPart = new PartMesh
            {
                UV = originalMesh.uv,
                Vertices = originalMesh.vertices,
                Normals = originalMesh.normals,
                Triangles = new int[originalMesh.subMeshCount][],
                Bounds = originalMesh.bounds
            };
            for (var i = 0; i < originalMesh.subMeshCount; i++)
                mainPart.Triangles[i] = originalMesh.GetTriangles(i);

            parts.Add(mainPart);

            for (var c = 0; c < cutAmount; c++)
            {
                for (var i = 0; i < parts.Count; i++)
                {
                    var bounds = parts[i].Bounds;
                    bounds.Expand(0.5f);

                    var planePoint = transform.InverseTransformPoint(contactPoint);
                    var planeNormal = Vector3.Cross(contactPointNormal + Random.onUnitSphere, contactPoint).normalized;

                    var planeNormal2 = Vector3.Cross(contactPoint, contactPointNormal + Random.onUnitSphere).normalized;
                    // #if UNITY_EDITOR
                    //                 Debug.DrawRay(contactPoint, planeNormal * 100, Color.red, 1000);
                    //                 Debug.DrawRay(contactPoint, planeNormal2 * 100, Color.blue, 1000);

                    //                 Debug.Log("PlaneNormal: " + planeNormal);
                    //                 Debug.Log("PlaneNormal 2: " + planeNormal2);
                    // #endif
                    var plane = new Plane(planeNormal, planePoint);

                    subParts.Add(GenerateMesh(parts[i], plane, true));
                    subParts.Add(GenerateMesh(parts[i], plane, false));
                }

                parts = new List<PartMesh>(subParts);
                subParts.Clear();
            }

            for (var i = 0; i < parts.Count; i++)
            {
                parts[i].MakeGameObject(this);
                parts[i].GameObject.GetComponent<Rigidbody>()
                    .AddForceAtPosition(parts[i].Bounds.center * ExplodeForce, transform.position);
            }

            Destroy(gameObject);
        }

        public void DestroyMesh(int cutAmount)
        {
            var originalMesh = GetComponent<MeshFilter>().mesh;
            originalMesh.RecalculateBounds();
            var parts = new List<PartMesh>();
            var subParts = new List<PartMesh>();

            var mainPart = new PartMesh
            {
                UV = originalMesh.uv,
                Vertices = originalMesh.vertices,
                Normals = originalMesh.normals,
                Triangles = new int[originalMesh.subMeshCount][],
                Bounds = originalMesh.bounds
            };
            for (var i = 0; i < originalMesh.subMeshCount; i++)
                mainPart.Triangles[i] = originalMesh.GetTriangles(i);

            parts.Add(mainPart);

            for (var c = 0; c < cutAmount; c++)
            {
                for (var i = 0; i < parts.Count; i++)
                {
                    var bounds = parts[i].Bounds;
                    bounds.Expand(0.5f);

                    var plane = new Plane(Random.onUnitSphere, new Vector3(Random.Range(bounds.min.x, bounds.max.x),
                        Random.Range(bounds.min.y, bounds.max.y),
                        Random.Range(bounds.min.z, bounds.max.z)));


                    subParts.Add(GenerateMesh(parts[i], plane, true));
                    subParts.Add(GenerateMesh(parts[i], plane, false));
                }

                parts = new List<PartMesh>(subParts);
                subParts.Clear();
            }

            for (var i = 0; i < parts.Count; i++)
            {
                parts[i].MakeGameObject(this);
                parts[i].GameObject.GetComponent<Rigidbody>()
                    .AddForceAtPosition(parts[i].Bounds.center * ExplodeForce, transform.position);
            }

            Destroy(gameObject);
        }

        private PartMesh GenerateMesh(PartMesh original, Plane plane, bool left)
        {
            var partMesh = new PartMesh();
            var ray1 = new Ray();
            var ray2 = new Ray();


            for (var i = 0; i < original.Triangles.Length; i++)
            {
                var triangles = original.Triangles[i];
                edgeSet = false;
                //Iterate over all of the triangles on the mesh
                for (var j = 0; j < triangles.Length; j = j + 3)
                {
                    var sideA = plane.GetSide(original.Vertices[triangles[j]]) == left;
                    var sideB = plane.GetSide(original.Vertices[triangles[j + 1]]) == left;
                    var sideC = plane.GetSide(original.Vertices[triangles[j + 2]]) == left;

                    var sideCount = (sideA ? 1 : 0) +
                                    (sideB ? 1 : 0) +
                                    (sideC ? 1 : 0);

                    switch (sideCount)
                    {
                        //If none of the points are on the left side of the plane skip the triangle
                        case 0:
                            continue;
                        //If all of the points are on the left side of the triangle add the triangle to the new mesh
                        case 3:
                            partMesh.AddTriangle(i,
                                original.Vertices[triangles[j]], original.Vertices[triangles[j + 1]],
                                original.Vertices[triangles[j + 2]],
                                original.Normals[triangles[j]], original.Normals[triangles[j + 1]],
                                original.Normals[triangles[j + 2]],
                                original.UV[triangles[j]], original.UV[triangles[j + 1]], original.UV[triangles[j + 2]]);
                            continue;
                    }

                    //Else

                    //cut points
                    //if sideB equals sideC return 0 else return if sideA equals sideC return 1 else return 2
                    var singleIndex = sideB == sideC ? 0 : sideA == sideC ? 1 : 2;

                    ray1.origin = original.Vertices[triangles[j + singleIndex]];
                    var dir1 = original.Vertices[triangles[j + (singleIndex + 1) % 3]] -
                               original.Vertices[triangles[j + singleIndex]];
                    ray1.direction = dir1;
                    plane.Raycast(ray1, out var enter1);
                    var lerp1 = enter1 / dir1.magnitude;

                    ray2.origin = original.Vertices[triangles[j + singleIndex]];
                    var dir2 = original.Vertices[triangles[j + (singleIndex + 2) % 3]] -
                               original.Vertices[triangles[j + singleIndex]];
                    ray2.direction = dir2;
                    plane.Raycast(ray2, out var enter2);
                    var lerp2 = enter2 / dir2.magnitude;

                    //first vertex = anchor
                    AddEdge(i,
                        partMesh,
                        left ? plane.normal * -1f : plane.normal,
                        ray1.origin + ray1.direction.normalized * enter1,
                        ray2.origin + ray2.direction.normalized * enter2,
                        Vector2.Lerp(original.UV[triangles[j + singleIndex]],
                            original.UV[triangles[j + (singleIndex + 1) % 3]], lerp1),
                        Vector2.Lerp(original.UV[triangles[j + singleIndex]],
                            original.UV[triangles[j + (singleIndex + 2) % 3]], lerp2));

                    switch (sideCount)
                    {
                        case 1:
                            partMesh.AddTriangle(i,
                                original.Vertices[triangles[j + singleIndex]],
                                ray1.origin + ray1.direction.normalized * enter1,
                                ray2.origin + ray2.direction.normalized * enter2,
                                original.Normals[triangles[j + singleIndex]],
                                Vector3.Lerp(original.Normals[triangles[j + singleIndex]],
                                    original.Normals[triangles[j + (singleIndex + 1) % 3]], lerp1),
                                Vector3.Lerp(original.Normals[triangles[j + singleIndex]],
                                    original.Normals[triangles[j + (singleIndex + 2) % 3]], lerp2),
                                original.UV[triangles[j + singleIndex]],
                                Vector2.Lerp(original.UV[triangles[j + singleIndex]],
                                    original.UV[triangles[j + (singleIndex + 1) % 3]], lerp1),
                                Vector2.Lerp(original.UV[triangles[j + singleIndex]],
                                    original.UV[triangles[j + (singleIndex + 2) % 3]], lerp2));

                            continue;
                        case 2:
                            partMesh.AddTriangle(i,
                                ray1.origin + ray1.direction.normalized * enter1,
                                original.Vertices[triangles[j + (singleIndex + 1) % 3]],
                                original.Vertices[triangles[j + (singleIndex + 2) % 3]],
                                Vector3.Lerp(original.Normals[triangles[j + singleIndex]],
                                    original.Normals[triangles[j + (singleIndex + 1) % 3]], lerp1),
                                original.Normals[triangles[j + (singleIndex + 1) % 3]],
                                original.Normals[triangles[j + (singleIndex + 2) % 3]],
                                Vector2.Lerp(original.UV[triangles[j + singleIndex]],
                                    original.UV[triangles[j + (singleIndex + 1) % 3]], lerp1),
                                original.UV[triangles[j + (singleIndex + 1) % 3]],
                                original.UV[triangles[j + (singleIndex + 2) % 3]]);
                            partMesh.AddTriangle(i,
                                ray1.origin + ray1.direction.normalized * enter1,
                                original.Vertices[triangles[j + (singleIndex + 2) % 3]],
                                ray2.origin + ray2.direction.normalized * enter2,
                                Vector3.Lerp(original.Normals[triangles[j + singleIndex]],
                                    original.Normals[triangles[j + (singleIndex + 1) % 3]], lerp1),
                                original.Normals[triangles[j + (singleIndex + 2) % 3]],
                                Vector3.Lerp(original.Normals[triangles[j + singleIndex]],
                                    original.Normals[triangles[j + (singleIndex + 2) % 3]], lerp2),
                                Vector2.Lerp(original.UV[triangles[j + singleIndex]],
                                    original.UV[triangles[j + (singleIndex + 1) % 3]], lerp1),
                                original.UV[triangles[j + (singleIndex + 2) % 3]],
                                Vector2.Lerp(original.UV[triangles[j + singleIndex]],
                                    original.UV[triangles[j + (singleIndex + 2) % 3]], lerp2));
                            break;
                    }
                }
            }

            partMesh.FillArrays();
            StartCoroutine((partMesh.DestroySelf()));
            return partMesh;
        }

        private void AddEdge(int subMesh, PartMesh partMesh, Vector3 normal, Vector3 vertex1, Vector3 vertex2, Vector2 uv1,
            Vector2 uv2)
        {
            if (!edgeSet)
            {
                edgeSet = true;
                edgeVertex = vertex1;
                edgeUV = uv1;
            }
            else
            {
                edgePlane.Set3Points(edgeVertex, vertex1, vertex2);

                partMesh.AddTriangle(subMesh,
                    edgeVertex,
                    edgePlane.GetSide(edgeVertex + normal) ? vertex1 : vertex2,
                    edgePlane.GetSide(edgeVertex + normal) ? vertex2 : vertex1,
                    normal,
                    normal,
                    normal,
                    edgeUV,
                    uv1,
                    uv2);
            }
        }

        /*
    Part Mesh is a piece from the original mesh
    */
        private class PartMesh
        {
            private readonly List<Vector3> _Normals = new List<Vector3>();
            private readonly List<List<int>> _Triangles = new List<List<int>>();
            private readonly List<Vector2> _UVs = new List<Vector2>();
            private readonly List<Vector3> _Vertices = new List<Vector3>();
            public Bounds Bounds;
            public GameObject GameObject;
            public Vector3[] Normals;
            public int[][] Triangles;
            public Vector2[] UV;
            public Vector3[] Vertices;

            public IEnumerator DestroySelf()
            {
                yield return new WaitForSeconds(1);
                Destroy(GameObject);
                
            }
            public void AddTriangle(int subMesh, Vector3 vert1, Vector3 vert2, Vector3 vert3, Vector3 normal1,
                Vector3 normal2, Vector3 normal3, Vector2 uv1, Vector2 uv2, Vector2 uv3)
            {
                if (_Triangles.Count - 1 < subMesh)
                    _Triangles.Add(new List<int>());

                _Triangles[subMesh].Add(_Vertices.Count);
                _Vertices.Add(vert1);
                _Triangles[subMesh].Add(_Vertices.Count);
                _Vertices.Add(vert2);
                _Triangles[subMesh].Add(_Vertices.Count);
                _Vertices.Add(vert3);
                _Normals.Add(normal1);
                _Normals.Add(normal2);
                _Normals.Add(normal3);
                _UVs.Add(uv1);
                _UVs.Add(uv2);
                _UVs.Add(uv3);

                Bounds.min = Vector3.Min(Bounds.min, vert1);
                Bounds.min = Vector3.Min(Bounds.min, vert2);
                Bounds.min = Vector3.Min(Bounds.min, vert3);
                Bounds.max = Vector3.Min(Bounds.max, vert1);
                Bounds.max = Vector3.Min(Bounds.max, vert2);
                Bounds.max = Vector3.Min(Bounds.max, vert3);
            }

            public void FillArrays()
            {
                Vertices = _Vertices.ToArray();
                Normals = _Normals.ToArray();
                UV = _UVs.ToArray();
                Triangles = new int[_Triangles.Count][];
                for (var i = 0; i < _Triangles.Count; i++)
                    Triangles[i] = _Triangles[i].ToArray();
            }

            public void MakeGameObject(MeshDestroy original)
            {
                var transform1 = original.transform;
                GameObject = new GameObject(original.name)
                {
                    transform =
                    {
                        position = transform1.position,
                        rotation = transform1.rotation,
                        localScale = transform1.localScale
                    }
                };

                var mesh = new Mesh
                {
                    name = original.GetComponent<MeshFilter>().mesh.name,
                    vertices = Vertices,
                    normals = Normals,
                    uv = UV
                };

                for (var i = 0; i < Triangles.Length; i++)
                    mesh.SetTriangles(Triangles[i], i, true);
                Bounds = mesh.bounds;

                var renderer = GameObject.AddComponent<MeshRenderer>();
                renderer.materials = original.GetComponent<MeshRenderer>().materials;

                var filter = GameObject.AddComponent<MeshFilter>();
                filter.mesh = mesh;

                var collider = GameObject.AddComponent<MeshCollider>();
                collider.convex = true;

                var rigidbody = GameObject.AddComponent<Rigidbody>();
                rigidbody.detectCollisions = false;

                if (!original.createNewMeshDestroy) return;
                
                var destrucableObject = GameObject.AddComponent<DestructableObject>();
                var meshDestroy = GameObject.AddComponent<MeshDestroy>();
                meshDestroy.ExplodeForce = original.ExplodeForce;
            }
        }
    }
}