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
    private bool isGamePaused = false;

    private void Update()
    {
        if (!isGamePaused && currentFillAmount >= maxFillAmount)
        {
            PauseGame();
            ShowPopup();
            currentFillAmount = 0f; // Deneyim çubuðunu sýfýrla
            expBar.fillAmount = currentFillAmount; // Fill Amount'i güncelle
        }

        if (isGamePaused && !isPopupShowing && Input.GetKeyDown(KeyCode.Space))
        {
            ResumeGame();
        }
    }

    public void UpdateExpBar()
    {
        if (!isGamePaused)
        {
            currentFillAmount += expIncreaseAmount;
            currentFillAmount = Mathf.Clamp(currentFillAmount, 0f, maxFillAmount);
            expBar.fillAmount = currentFillAmount;
            HidePopup();
        }
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

    private void PauseGame()
    {
        Time.timeScale = 0f; // Oyun zamanýný duraklat
        isGamePaused = true;
    }
   
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Oyun zamanýný devam ettir
        isGamePaused = false;
    }
}
