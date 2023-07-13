using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    public Sprite kilitliSprite;
    public Sprite[] aktifSprites; // Aktif sprite görüntülerini içeren dizi
    public GameObject confirmationPopup;
    public GameObject loadingScreen;
    public Image loadingBarFill;

    private int selectedLevel;

    private PlayerScripts playerScripts;

private void Awake()
{
    playerScripts = FindObjectOfType<PlayerScripts>();
    playerScripts.LoadPlayer();
    Debug.Log(playerScripts.level);
    if (playerScripts != null)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", playerScripts.level);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.sprite = kilitliSprite; // Tüm butonların görüntüsünü kilitli sprite ile başlat
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].image.sprite = aktifSprites[i]; // Açık olan butonların görüntüsünü ilgili aktif sprite ile değiştir
            buttons[i].interactable = true;
        }
    }
    else
    {
        Debug.LogError("PlayerScripts component not found in the scene!");
    }
}
    public void OpenMap(int mapId)
    {
        selectedLevel = mapId;
        // confirmationText.text = "Are you sure you want to access Level " + mapId + "?";
        confirmationPopup.SetActive(true);
    }

    public void ConfirmOpenMap()
    {
        StartCoroutine(LoadMapAsync());
    }

    private IEnumerator LoadMapAsync()
    {
        loadingScreen.SetActive(true); // Loading ekranını etkinleştir
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(selectedLevel); // Haritayı asenkron olarak yükle

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // İlerleme yüzdesini hesapla (0 ile 1 arasında)
            loadingBarFill.fillAmount = progress; // Loading barın doluluk oranını güncelle
            yield return null;
        }
    }

    public void CancelOpenMap()
    {
        confirmationPopup.SetActive(false);
    }

    public void UnlockNextLevel()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 2);
        int nextLevel = unlockedLevel + 1;
        PlayerPrefs.SetInt("UnlockedLevel", nextLevel);
        buttons[unlockedLevel].image.sprite = aktifSprites[unlockedLevel]; // Yeni açılan butonun görüntüsünü ilgili aktif sprite ile değiştir
        buttons[unlockedLevel].interactable = true;
    }
}
