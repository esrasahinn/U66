using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public int characterIndex;
    public Vector3 lastCheckPointPos;

    private GameObject currentPlayer;

    void Awake()
    {
        characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        currentPlayer = Instantiate(playerPrefabs[characterIndex], lastCheckPointPos, Quaternion.identity);
        currentPlayer.SetActive(true);
    }
}
