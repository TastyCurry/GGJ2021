using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMemory : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private GameObject memory;
    private bool _isTrigger;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {   
            if(_isTrigger)memory.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!source.isPlaying)source.Play();
        _isTrigger = true;
        print(_isTrigger);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        _isTrigger = false;
        print(_isTrigger);
    }
}
