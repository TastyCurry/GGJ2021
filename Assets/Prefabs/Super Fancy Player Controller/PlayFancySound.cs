using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFancySound : MonoBehaviour
{
    [SerializeField]
    private AudioSource fancySound;

    void Awake()
    {
        fancySound = GetComponent<AudioSource>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var leertasteWurdeGedrueckt = Input.GetKeyDown(KeyCode.UpArrow);

        if (leertasteWurdeGedrueckt) {
            fancySound.Play();
        }
    }
}
