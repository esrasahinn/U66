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

    private ProjectileController projectileController; // ProjectileController referans� eklendi

    private void Awake()
    {
        Buton1.onClick.AddListener(ActivateAbility1);
        Buton2.onClick.AddListener(ActivateAbility2);
        projectileController = FindObjectOfType<ProjectileController>(); // ProjectileController referans� al�nd�
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

    private void HidePopup()
    {
        popupObject.SetActive(false);
        isPopupShowing = false;
    }

    private void ActivateAbility1()
    {
        // Karakterin hareket h�z�n� 30 saniyeli�ine artt�r
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.ActivateAbility1();
        }
        HidePopup();
    }

    private void ActivateAbility2()
    {
        if (projectileController != null) // Null kontrol� eklendi
        {
            projectileController.ActivateAbility2();
        }
        HidePopup();
    }
}