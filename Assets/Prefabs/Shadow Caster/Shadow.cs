using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    MeshFilter meshFilter;
    PolygonCollider2D polygonCollider;

    Vector3[] shadowVertices = new Vector3[0];

    public Vector3[] ShadowVertices
    {
        get => shadowVertices;

        set
        {
            Matrix4x4 worldToLocal = transform.worldToLocalMatrix;



            var shadowVertices2D = value.Select(v =>
            {
                var local = worldToLocal.MultiplyPoint3x4(v);                
                return new Vector2(local.x, local.y);
            }).ToArray();

            List<Vector2> convexHull = GetConvexHull(shadowVertices2D.ToList());

            shadowVertices = convexHull.Select(v => new Vector3(v.x, v.y, 0.0f)).ToArray();

            Mesh mesh = new Mesh();
            meshFilter.mesh = mesh;

            meshFilter.mesh.vertices = shadowVertices;

            List<Triangle> triangles = TriangulateConvexPolygon(shadowVertices);
            int[] tri = new int[triangles.Count * 3];
            for (int i = 0; i < triangles.Count; i++)
            {

                tri[i * 3] = 0;
                tri[(i * 3) + 1] = i + 1;
                tri[(i * 3) + 2] = i + 2;
            }


            Vector3[] normals = new Vector3[shadowVertices.Length];
            for (int i = 0; i < normals.Length; i++)
            {
                normals[i] = Vector3.forward;
            }


            meshFilter.mesh.normals = normals;
            meshFilter.mesh.triangles = tri;

            polygonCollider.points = shadowVertices2D;
        }
    }
    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public static List<Triangle> TriangulateConvexPolygon(Vector3[] convexHullpoints)
    {
        List<Triangle> triangles = new List<Triangle>();

        for (int i = 2; i < convexHullpoints.Length; i++)
        {
            Vector3 a = convexHullpoints[0];
            Vector3 b = convexHullpoints[i - 1];
            Vector3 c = convexHullpoints[i];

            triangles.Add(new Triangle(a, b, c));
        }

        return triangles;
    }

    public static List<Vector2> GetConvexHull(List<Vector2> points)
    {
        //If we have just 3 points, then they are the convex hull, so return those
        if (points.Count == 3)
        {
            //These might not be ccw, and they may also be colinear
            return points;
        }

        //If fewer points, then we cant create a convex hull
        if (points.Count < 3)
        {
            return null;
        }



        //The list with points on the convex hull
        List<Vector2> convexHull = new List<Vector2>();

        //Step 1. Find the vertex with the smallest x coordinate
        //If several have the same x coordinate, find the one with the smallest z
        Vector2 startVertex = points[0];


        for (int i = 1; i < points.Count; i++)
        {
            Vector2 testPos = points[i];

            if (testPos.x < startVertex.x)
            {
                startVertex = points[i];

            }
        }

        //This vertex is always on the convex hull
        convexHull.Add(startVertex);

        points.Remove(startVertex);



        //Step 2. Loop to generate the convex hull
        Vector2 currentPoint = convexHull[0];

        //Store colinear points here - better to create this list once than each loop
        List<Vector2> colinearPoints = new List<Vector2>();

        int counter = 0;

        while (true)
        {
            //After 2 iterations we have to add the start position again so we can terminate the algorithm
            //Cant use convexhull.count because of colinear points, so we need a counter
            if (counter == 2)
            {
                points.Add(convexHull[0]);
            }

            //Pick next point randomly
            Vector2 nextPoint = points[UnityEngine.Random.Range(0, points.Count)];

            //To 2d space so we can see if a point is to the left is the vector ab
            Vector2 a = currentPoint;

            Vector2 b = nextPoint;

            //Test if there's a point to the right of ab, if so then it's the new b
            for (int i = 0; i < points.Count; i++)
            {
                //Dont test the point we picked randomly
                if (points[i].Equals(nextPoint))
                {
                    continue;
                }

                Vector2 c = points[i];

                //Where is c in relation to a-b
                // < 0 -> to the right
                // = 0 -> on the line
                // > 0 -> to the left
                float relation = IsAPointLeftOfVectorOrOnTheLine(a, b, c);

                //Colinear points
                //Cant use exactly 0 because of floating point precision issues
                //This accuracy is smallest possible, if smaller points will be missed if we are testing with a plane
                float accuracy = 0.00001f;

                if (relation < accuracy && relation > -accuracy)
                {
                    colinearPoints.Add(points[i]);
                }
                //To the right = better point, so pick it as next point on the convex hull
                else if (relation < 0f)
                {
                    nextPoint = points[i];

                    b = nextPoint;

                    //Clear colinear points
                    colinearPoints.Clear();
                }
                //To the left = worse point so do nothing
            }



            //If we have colinear points
            if (colinearPoints.Count > 0)
            {
                colinearPoints.Add(nextPoint);

                //Sort this list, so we can add the colinear points in correct order
                colinearPoints = colinearPoints.OrderBy(n => Vector3.SqrMagnitude(n - currentPoint)).ToList();

                convexHull.AddRange(colinearPoints);

                currentPoint = colinearPoints[colinearPoints.Count - 1];

                //Remove the points that are now on the convex hull
                for (int i = 0; i < colinearPoints.Count; i++)
                {
                    points.Remove(colinearPoints[i]);
                }

                colinearPoints.Clear();
            }
            else
            {
                convexHull.Add(nextPoint);

                points.Remove(nextPoint);

                currentPoint = nextPoint;
            }

            //Have we found the first point on the hull? If so we have completed the hull
            if (currentPoint.Equals(convexHull[0]))
            {
                //Then remove it because it is the same as the first point, and we want a convex hull with no duplicates
                convexHull.RemoveAt(convexHull.Count - 1);

                break;
            }

            counter += 1;
        }

        return convexHull;
    }

    public static float IsAPointLeftOfVectorOrOnTheLine(Vector2 a, Vector2 b, Vector2 p)
    {
        float determinant = (a.x - p.x) * (b.y - p.y) - (a.y - p.y) * (b.x - p.x);

        return determinant;
    }

}

public class Triangle
{
    //Corners
    public Vector3 v1;
    public Vector3 v2;
    public Vector3 v3;


    public Triangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        this.v1 = v1;
        this.v2 = v2;
        this.v3 = v3;
    }


}
