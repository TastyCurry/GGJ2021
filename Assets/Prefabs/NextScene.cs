using System.Collections;
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
        yield return new WaitWhile(() => !source.isPlaying);
        yield return new WaitWhile(() => source.isPlaying);
        crossfade.SetTrigger("Fade_in");
        yield return new WaitForSeconds(0.9f);
        SceneManager.LoadScene("Test Level Anna");
    }
    
}
