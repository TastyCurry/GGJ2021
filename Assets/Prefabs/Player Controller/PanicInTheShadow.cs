using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicInTheShadow : MonoBehaviour
{
    LightSource lightSource;

    void Start()
    {
        lightSource = FindObjectOfType<LightSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //lightSource.t
    }
}
