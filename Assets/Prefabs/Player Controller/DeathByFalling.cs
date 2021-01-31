using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
public class DeathByFalling : MonoBehaviour
{
    private AudioSource audioSource;
    private Rigidbody2D rigidbody;

    [SerializeField]
    private AudioClip fallingAudioClip;


    [SerializeField]
    private float fallingVelecityForDeath;
    private bool falling;

    void Awake()
    {
        rigidbody = FindObjectOfType<Rigidbody2D>();
        audioSource = FindObjectOfType<AudioSource>();
    }

    public bool Falling
    {
        get
        {
            return falling;
        }

        set
        {
            if (falling != value)
            {
                falling = value;

                if (falling)
                {
                    audioSource.clip = fallingAudioClip;
                    audioSource.Play();

                    StartCoroutine(waitForReload()); //Start Coroutine
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        print(rigidbody.velocity.y);
        Falling = rigidbody.velocity.y < fallingVelecityForDeath;
    }

    IEnumerator waitForReload()
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
