using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class expController : MonoBehaviour
{
    public Image expBar;
    public GameObject popupObject;
    public List<Button> allButtons;
    public List<Image> allImages;
    public int maxButtonCount = 3;
    public int maxImageCount = 3;
    private float maxFillAmount = 1f;
    private float currentFillAmount = 0f;
    private bool isPopupShowing = false;
    private bool isGamePaused = false;
    private float buttonSpacing = 300f;
    private AudioSource audiosource;
    private List<Button> activeButtons = new List<Button>();

    private void Update()
    {
        audiosource = GetComponent<AudioSource>();
        if (!isGamePaused && currentFillAmount >= maxFillAmount)
        {
            PauseGame();
            ShowPopup();
            currentFillAmount = 0f;
            expBar.fillAmount = currentFillAmount;
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
        SetPopupShowing(true);
        audiosource.Play();
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

    public void SetRandomButtons()
    {
        foreach (Image image in allImages)
        {
            image.gameObject.SetActive(false);
        }

        activeButtons.Clear();

        int imageCount = Mathf.Min(maxButtonCount, maxImageCount, allImages.Count);
        List<Image> availableImages = new List<Image>(allImages);

        float totalButtonWidth = imageCount * buttonSpacing;
        float startX = -totalButtonWidth / 2f + (buttonSpacing / 2f); // Saða kaydýrma eklendi

        for (int i = 0; i < imageCount; i++)
        {
            int randomIndex = Random.Range(0, availableImages.Count);
            Image randomImage = availableImages[randomIndex];
            availableImages.RemoveAt(randomIndex);

            Button button = randomImage.GetComponentInChildren<Button>();
            if (button != null)
            {
                activeButtons.Add(button);
                randomImage.gameObject.SetActive(true);
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => OnButtonClick(randomImage));

                RectTransform imageRectTransform = randomImage.GetComponent<RectTransform>();
                float imageX = startX + (i * buttonSpacing);
                imageRectTransform.anchoredPosition = new Vector2(imageX, -10f);
            }
        }

        foreach (Button button in allButtons)
        {
            if (!activeButtons.Contains(button))
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    private void OnButtonClick(Image clickedImage)
    {
        Debug.Log("Button clicked: " + clickedImage.name);
        SetPopupShowing(false);
    }

    public void SetPopupShowing(bool showing)
    {
        isPopupShowing = showing;
        popupObject.SetActive(showing);

        if (!showing)
        {
            ResumeGame();
        }
    }
}
