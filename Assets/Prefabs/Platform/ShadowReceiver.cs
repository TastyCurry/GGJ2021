using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowReceiver : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Vector3[] frontVertices;

    public Vector3[] FrontVertices => frontVertices;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;

        frontVertices = CalculateFrontVertices(CalculateWorldVertices(vertices));
    }

    private Vector3[] CalculateFrontVertices(Vector3[] vertices)
    {
        List<Vector3> frontVertices = new List<Vector3>();
        for (var i = 0; i < vertices.Length; i++)
        {
            var vertex = vertices[i];

            if (vertex.z < 0)
            {

                frontVertices.Add(vertex);
            }
        }

        return frontVertices.ToArray();
    }

    private Vector3[] CalculateWorldVertices(Vector3[] vertices)
    {
        Matrix4x4 localToWorld = transform.localToWorldMatrix;
        List<Vector3> worldVertices = new List<Vector3>();

        for (int i = 0; i < vertices.Length; ++i)
        {
            Vector3 worldVertex = localToWorld.MultiplyPoint3x4(vertices[i]);
            worldVertices.Add(worldVertex);
        }

        return worldVertices.ToArray();
    }

    void Start()
    {
        //for (var i = 0; i < frontVertices.Length; i++)
        //{
        //    print(frontVertices[i]);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        frontVertices = CalculateFrontVertices(CalculateWorldVertices(vertices));


    }
}
