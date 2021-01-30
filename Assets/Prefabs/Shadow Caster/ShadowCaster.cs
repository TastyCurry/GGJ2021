using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShadowCaster : MonoBehaviour
{
    Shadow shadow;
    Mesh mesh;
    Vector3[] vertices;
    Vector3[] allVertices;

    public Vector3[] AllVertices => allVertices;

    public Shadow Shadow => shadow;

    void Awake()
    {
        shadow = transform.GetComponentInChildren<Shadow>();

        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices.Select(v => v).Distinct().ToArray();

        allVertices = CalculateWorldVertices(vertices);
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
        allVertices = CalculateWorldVertices(vertices);
    }
}
