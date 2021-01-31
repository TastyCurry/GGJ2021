using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectMemory : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private GameObject memory;
    [HideInInspector]
    public static bool IsTrigger;

    // Update is called once per frame
    void Update()
    {
        if (IsTrigger)
        {
            memory.SetActive(false);
            source.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IsTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IsTrigger = false;
    }
}
