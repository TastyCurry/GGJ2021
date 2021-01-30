using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCanvas : MonoBehaviour
{

    Plane plane;

    public Plane Plane => plane;

    void Start()
    {
        plane = new Plane(new Vector3(0, 0, -1), 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
