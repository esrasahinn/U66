using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    public GameObject confirmationPopup;
    public TextMeshProUGUI confirmationText;
    private int selectedLevel;

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void OpenMap(int mapId)
    {
        selectedLevel = mapId;
        string MapName = "Map " + mapId;
        confirmationText.text = "Are you sure you want to access Level " + mapId + "?";
        confirmationPopup.SetActive(true);
    }

    public void ConfirmOpenMap()
    {
        string MapName = "Map " + selectedLevel;
        SceneManager.LoadScene(MapName);
    }

    public void CancelOpenMap()
    {
        confirmationPopup.SetActive(false);
    }

    public void UnlockNextLevel()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        int nextLevel = unlockedLevel + 1;
        PlayerPrefs.SetInt("UnlockedLevel", nextLevel);
        buttons[unlockedLevel].interactable = true;
    }
}
