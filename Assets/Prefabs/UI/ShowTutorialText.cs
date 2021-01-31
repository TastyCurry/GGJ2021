using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShowTutorialText : MonoBehaviour
{
   [SerializeField]
   private Transform playerPos, lightPos;
   
   [SerializeField][TextArea]
   private string textPlayer, textLight;

   [SerializeField]
   private Text textField;

   [SerializeField]
   private GameObject tutorialUI;
  
   [SerializeField]
   private float distance;
   
   private void Start()
   {
      StartCoroutine(WaitUntilPos());
   }

   IEnumerator WaitUntilPos()
   {
      yield return new WaitUntil(() => playerPos.position.x >= this.gameObject.transform.position.x);
      tutorialUI.SetActive(true);
      textField.text = textPlayer; 
      yield return new WaitUntil(() => playerPos.position.x - distance > this.gameObject.transform.position.x);
      tutorialUI.SetActive(false);
      if (textLight == "") yield break;
      yield return new WaitForSeconds(2.0f);
      tutorialUI.SetActive(true);
      textField.text = textLight; 
      yield return new WaitUntil(() => lightPos.position.x - 4.5 > this.gameObject.transform.position.x);
      tutorialUI.SetActive(false);
   }
}