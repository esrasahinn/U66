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

    private void Awake()
    {
        Buton1.onClick.AddListener(ActivateAbility1);
        Buton2.onClick.AddListener(ActivateAbility2);
    }
    private void Start()
    {
        // Diðer baþlatma iþlemleri
    }

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

    private void ActivateAbility1()
    {
        // Karakterin hareket hýzýný 30 saniyeliðine arttýr
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.ActivateAbility1();
        }
        HidePopup();
    }
    private void ActivateAbility2()
    {
        
        ProjectileController player = FindObjectOfType<ProjectileController>();
        if (player != null)
        {
            player.ActivateAbility2();
        }

        HidePopup();
    }

}