using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMemory : MonoBehaviour
{

    private ParticleSystem particleSystem;
    private AudioSource source;
    public AudioSource Source { get => source; }

    private MemoryState state;

    public void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        source = GetComponent<AudioSource>();
        State = MemoryState.notCollectedYet;
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case MemoryState.collecting:
                source.Play();
                State = MemoryState.collected;
                break;
            case MemoryState.collected:
                particleSystem.Stop();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (State == MemoryState.notCollectedYet) {
            State = MemoryState.collecting;
        }
    }

    public MemoryState State
    {
        get => state;
        set
        {
            if (state != value)
            {
                state = value;
            }
        }
    }
}

public enum MemoryState
{
    notCollectedYet,
    collecting,
    collected
}

