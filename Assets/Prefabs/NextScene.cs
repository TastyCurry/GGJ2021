using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    // Start is called before the first frame update
    
    [SerializeField]
    private Animator crossfade;
    void Start()
    {
        StartCoroutine(WaitUntilAudio());
    }

    IEnumerator WaitUntilAudio()
    {
        yield return new WaitUntil(() => CollectMemory.IsTrigger);
        yield return new WaitWhile(() => source.isPlaying);
        crossfade.SetTrigger("Fade_in");
        yield return new WaitForSeconds(10.0f);
        SceneManager.LoadScene("Test Level Anna");
    }
    
}
