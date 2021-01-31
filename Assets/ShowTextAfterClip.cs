using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTextAfterClip : MonoBehaviour
{
    AudioSource source;

    public CanvasGroup canvasGroup;


    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    bool clipFinished = false;




    void Update()
    {
        if (clipFinished && canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime;
        }


        if (!source.isPlaying) {
            clipFinished = true;
        }
    }
}
