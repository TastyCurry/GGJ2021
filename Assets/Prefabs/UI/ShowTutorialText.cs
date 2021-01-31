using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShowTutorialText : MonoBehaviour
{
   [SerializeField]
   private Transform playerPos,lightPos;
   
   [SerializeField]
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
      yield return new WaitForSeconds(3.0f);
      tutorialUI.SetActive(true);
      textField.text = textLight; 
      yield return new WaitUntil(() => lightPos.position.x - 1.5 > this.gameObject.transform.position.x);
      tutorialUI.SetActive(false);
   }
}