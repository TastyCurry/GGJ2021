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

   private void Start()
   {
      StartCoroutine(WaitUntilPos());
   }

   IEnumerator WaitUntilPos()
   {
      yield return new WaitUntil(() => playerPos.position.x >= this.gameObject.transform.position.x);
      textField.text = text; //laufen ad buttons 
      // Lichtsteuerung erklären pfeiltasten warten bis licht verschben wurde
      //
      yield return new WaitUntil(() => playerPos.position.x - 0.5 > this.gameObject.transform.position.x);
   }
}