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
   private Transform playerPos;
   
   [SerializeField]
   private string text;

   [SerializeField]
   private Text textField;

   [SerializeField]
   private GameObject tutorialUI;
   private void Start()
   {
      StartCoroutine(WaitUntilPos());
   }

   IEnumerator WaitUntilPos()
   {
      tutorialUI.SetActive(true);
      yield return new WaitUntil(() => playerPos.position.x >= this.gameObject.transform.position.x);
      textField.text = text; 
      yield return new WaitUntil(() => playerPos.position.x - 0.5 > this.gameObject.transform.position.x);
      tutorialUI.SetActive(false);
   }
}