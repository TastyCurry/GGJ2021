using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{

    ShadowCaster[] shadowReceivers;
    ShadowCanvas shadowCanvas;
    Vector3[] shadowVertices = new Vector3[0];

    void Awake()
    {
        shadowReceivers = FindObjectsOfType<ShadowCaster>();
        shadowCanvas = FindObjectOfType<ShadowCanvas>();

        
    }

    void Start()
    {
    }

    void Update()
    {
        var shadowCasterPosition = transform.position;

        for (int i = 0; i < shadowReceivers.Length; i++)
        {
            var shadowReceiver = shadowReceivers[i];
            var frontVertices = shadowReceiver.AllVertices;

            shadowReceiver.Shadow.ShadowVertices = CalculateShadowVertices(shadowCasterPosition, frontVertices);
        }

        float h = Input.GetAxis("Horizontal2");
        float v = Input.GetAxis("Vertical2");

        transform.Translate(h * 0.03f, v * 0.03f, 0f);

    }

    private Vector3[] CalculateShadowVertices(Vector3 shadowCasterPosition, Vector3[] frontVertices)
    {
        List<Vector3> shadowVertices = new List<Vector3>();
        for (int j = 0; j < frontVertices.Length; j++)
        {
            var shadowReceiverVertex = frontVertices[j];

            var rayDirection = shadowReceiverVertex - shadowCasterPosition;

            var lightRay = new Ray(shadowCasterPosition, rayDirection);

            float intersectionDistance;
            var hit = shadowCanvas.Plane.Raycast(lightRay, out intersectionDistance);
            if (hit)
            {
                var shadowVertex = lightRay.GetPoint(intersectionDistance);
                shadowVertices.Add(shadowVertex);
            }
        }

        return shadowVertices.ToArray();
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;

        for (int i = 0; i < shadowVertices.Length; i++)
        {
            var shadowVertex = shadowVertices[i];
            Gizmos.DrawSphere(shadowVertex, 0.1f);
        }

    }
}
