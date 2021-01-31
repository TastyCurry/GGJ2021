using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMemory : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {    
            //collect memory
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("hdffklsjflskdj");
    }
}
