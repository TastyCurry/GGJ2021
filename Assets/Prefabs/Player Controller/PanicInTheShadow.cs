using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PanicInTheShadow : MonoBehaviour
{


    private AudioSource audioSource;


    [SerializeField]
    private AudioClip mildPanicAudioClip;
    [SerializeField]
    private AudioClip panicAudioClip;
    [SerializeField]
    private AudioClip extremePanicAudioClip;
    [SerializeField]
    private AudioClip deathAudioClip;

    [SerializeField]
    private float distanceForMildPanic;

    [SerializeField]
    private float distanceForPanic;

    [SerializeField]
    private float distanceForExtremePanic;

    [SerializeField]
    private float distanceForDeath;

    LightSource lightSource;

    private PanicModes panicMode;

    public PanicModes PanicMode
    {
        get
        {
            return panicMode;
        }

        set
        {

            if (value != panicMode)
            {
                panicMode = value;

                switch (panicMode)
                {
                    case PanicModes.noPanic:
                        audioSource.Stop();
                        break;
                    case PanicModes.mildPanic:
                        audioSource.clip = mildPanicAudioClip;
                        audioSource.Play();
                        break;
                    case PanicModes.panic:
                        audioSource.clip = panicAudioClip;
                        audioSource.Play();
                        break;
                    case PanicModes.extremePanic:
                        audioSource.clip = extremePanicAudioClip;
                        audioSource.Play();
                        break;
                    case PanicModes.death:
                        audioSource.clip = deathAudioClip;
                        audioSource.Play();
                        break;
                    default:
                        break;
                }
            }

        }
    }

    private float distanceToLightSource;

    public float DistanceToLightSource
    {
        get
        {
            return distanceToLightSource;
        }

        set
        {
            distanceToLightSource = value;

            if (distanceToLightSource < distanceForMildPanic)
            {
                PanicMode = PanicModes.noPanic;
            }
            else if (distanceToLightSource >= distanceForMildPanic && distanceToLightSource < distanceForPanic)
            {
                PanicMode = PanicModes.mildPanic;

            }
            else if (distanceToLightSource >= distanceForPanic && distanceToLightSource < distanceForExtremePanic)
            {
                PanicMode = PanicModes.panic;

            }
            else if (distanceToLightSource >= distanceForExtremePanic && distanceToLightSource < distanceForDeath)
            {
                PanicMode = PanicModes.extremePanic;

            }
            else if (distanceToLightSource >= distanceForDeath)
            {
                PanicMode = PanicModes.death;
            }
        }
    }

    void Awake()
    {
        lightSource = FindObjectOfType<LightSource>();
        audioSource = FindObjectOfType<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        var lightVector = lightSource.transform.position;
        var playerVector = transform.position;

        DistanceToLightSource = Vector2.Distance(lightVector, playerVector);
    }
}

public enum PanicModes
{
    noPanic,
    mildPanic,
    panic,
    extremePanic,
    death
}