using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class expController : MonoBehaviour
{
    public Image expBar;
    public GameObject popupObject;
    public List<Button> allButtons;
    public int maxButtonCount = 3;
    private float maxFillAmount = 1f;
    private float currentFillAmount = 0f;
    private bool isPopupShowing = false;
    private bool isGamePaused = false;
    private float buttonSpacing = 200f; // Butonlar arasýndaki yatay boþluk

    private List<Button> activeButtons = new List<Button>();

    private void Update()
    {
        if (!isGamePaused && currentFillAmount >= maxFillAmount)
        {
            PauseGame();
            ShowPopup();
            currentFillAmount = 0f;
            expBar.fillAmount = currentFillAmount;
            SetRandomButtons();
        }

        if (isGamePaused && !isPopupShowing && Input.GetKeyDown(KeyCode.Space))
        {
            ResumeGame();
        }
    }

    public void UpdateExpBar(float expIncreaseAmount)
    {
        if (!isGamePaused)
        {
            currentFillAmount += expIncreaseAmount * maxFillAmount;
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
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    private void SetRandomButtons()
    {
        activeButtons.Clear();

        int buttonCount = allButtons.Count;

        if (buttonCount <= maxButtonCount)
        {
            activeButtons.AddRange(allButtons);
        }
        else
        {
            List<Button> availableButtons = new List<Button>(allButtons);
            while (activeButtons.Count < maxButtonCount)
            {
                int randomIndex = Random.Range(0, availableButtons.Count);
                activeButtons.Add(availableButtons[randomIndex]);
                availableButtons.RemoveAt(randomIndex);
            }
        }

        float totalButtonWidth = activeButtons.Count * activeButtons[0].GetComponent<RectTransform>().sizeDelta.x;
        float totalSpacing = (activeButtons.Count - 1) * buttonSpacing;
        float totalWidth = totalButtonWidth + totalSpacing;
        float startX = -totalWidth / 2f;

        for (int i = 0; i < buttonCount; i++)
        {
            Button button = allButtons[i];
            RectTransform buttonRectTransform = button.GetComponent<RectTransform>();

            if (activeButtons.Contains(button))
            {
                float buttonX = startX + (activeButtons.IndexOf(button) * (buttonRectTransform.sizeDelta.x + buttonSpacing));
                buttonRectTransform.anchoredPosition = new Vector2(buttonX, 0f);
                button.gameObject.SetActive(true);
            }
            else
            {
                button.gameObject.SetActive(false);
            }
        }
    }
}
