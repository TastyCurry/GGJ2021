using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{

    float timeUntilNextScene = 1;

    CollectMemory collectMemory;

    [SerializeField]
    private Animator crossfade;

    void Awake()
    {
        collectMemory = GetComponent<CollectMemory>();
    }

    void Start()
    {
        CurrentState = State.audioHasntStartedYet;
    }

    void Update()
    {
        if (CurrentState == State.audioHasntStartedYet)
        {
            if (collectMemory.State == MemoryState.collected)
            {
                CurrentState = State.playingAudio;
            }
        }

        if (currentState == State.playingAudio)
        {
            if (!collectMemory.Source.isPlaying)
            {
                CurrentState = State.fadeOut;
            }
        }

        if (currentState == State.fadeOut)
        {
            timeUntilNextScene -= Time.deltaTime;

            if (timeUntilNextScene <= 0)
            {
                SceneManager.LoadScene("Test Level Anna");
            }

        }

    }

    private State currentState;
    public State CurrentState
    {
        get => currentState; set
        {
            if (currentState != value)
            {
                currentState = value;

                switch (currentState)
                {
                    case State.fadeOut:
                        crossfade.SetTrigger("Fade_in");
                        break;
                }
            }
        }
    }

}

public enum State
{
    audioHasntStartedYet,
    playingAudio,
    fadeOut,
}