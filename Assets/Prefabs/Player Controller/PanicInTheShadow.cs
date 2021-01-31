using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class PanicInTheShadow : MonoBehaviour
{
    [SerializeField]
    private AudioSource mildPanicAudio;
    [SerializeField]
    private AudioSource panicAudio;
    [SerializeField]
    private AudioSource extremePanicAudio;
    [SerializeField]
    private AudioSource deathAudio;

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

                mildPanicAudio.Stop();
                panicAudio.Stop();
                extremePanicAudio.Stop();
                deathAudio.Stop();

                switch (panicMode)
                {
                    case PanicModes.mildPanic:
                        mildPanicAudio.Play();
                        break;
                    case PanicModes.panic:
                        panicAudio.Play();
                        break;
                    case PanicModes.extremePanic:
                        extremePanicAudio.Play();
                        break;
                    case PanicModes.death:
                        deathAudio.Play();
                        StartCoroutine(waitForReload());
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
    }

    // Update is called once per frame
    void Update()
    {
        var lightVector = lightSource.transform.position;
        var playerVector = transform.position;

        DistanceToLightSource = Vector2.Distance(lightVector, playerVector);
    }

    IEnumerator waitForReload()
    {
        while (deathAudio.isPlaying)
        {
            yield return null;
        }
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
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