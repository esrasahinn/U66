using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class expController : MonoBehaviour
{

    public Image expBar;
    public GameObject popupObject;
    private float expIncreaseAmount = 0.2f;
    private float maxFillAmount = 1f;
    private float currentFillAmount = 0f;
    private bool isPopupShowing = false;

    private void Update()
    {
        if (currentFillAmount >= maxFillAmount)
        {
            if (!isPopupShowing)
            {
                ShowPopup();
                currentFillAmount = 0f; // EXP BAR'ý sýfýrla
                expBar.fillAmount = currentFillAmount; // Fill Amount'i güncelle
            }
        }
        //else if (Input.GetKeyDown(KeyCode.T))
        //{
        //    UpdateExpBar();
        //}
    }

    public void UpdateExpBar()
    {
        currentFillAmount += expIncreaseAmount;
        currentFillAmount = Mathf.Clamp(currentFillAmount, 0f, maxFillAmount);
        expBar.fillAmount = currentFillAmount;
        HidePopup();
    }

    private void ShowPopup()
    {
        popupObject.SetActive(true);
        isPopupShowing = true;
    }

    private void HidePopup()
    {
        popupObject.SetActive(false);
        isPopupShowing = false;
    }
}