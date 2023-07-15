//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class TimedText : MonoBehaviour
//{
//    public Text textObject;
//    public float displayTime = 0.5f;

//    private Coroutine hideCoroutine;

//    private void Start()
//    {
//        textObject.gameObject.SetActive(false);
//    }

//    public void ShowText(string message)
//    {
//        textObject.text = message;

//        if (hideCoroutine != null)
//        {
//            StopCoroutine(hideCoroutine);
//        }

//        textObject.gameObject.SetActive(true);
//        hideCoroutine = StartCoroutine(HideText());
//    }

//    private IEnumerator HideText()
//    {
//        yield return new WaitForSeconds(displayTime);
//        textObject.gameObject.SetActive(false);
//    }
//}
