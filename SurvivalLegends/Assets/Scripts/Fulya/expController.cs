using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class expController : MonoBehaviour
{
    public Image expBar;
    public GameObject popupObject;
    private float expIncreaseAmount = 0.9f;
    private float maxFillAmount = 1f;
    private float currentFillAmount = 0f;
    private bool isPopupShowing = false;
    public Button Buton1;
    public Button Buton2;
    public Button Buton3;
    public Button Buton4;
    private ProjectileController projectileController; // ProjectileController referans� eklendi
  
    private void Awake()
    {
        projectileController = GetComponent<ProjectileController>(); // ProjectileController referans� al�nd�
    }

    private void Start()
    {
        // Di�er ba�latma i�lemleri
    }

    private void Update()
    {
        if (currentFillAmount >= maxFillAmount)
        {
            if (!isPopupShowing)
            {
                ShowPopup();
                currentFillAmount = 0f; // EXP BAR'� s�f�rla
                expBar.fillAmount = currentFillAmount; // Fill Amount'i g�ncelle
            }
        }
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

    public void HidePopup()
    {
        popupObject.SetActive(false);
        isPopupShowing = false;
    }


}