using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public int characterIndex;
    public Vector3 lastCheckPointPos;

    private GameObject currentPlayer;
    private Transform cameraFollowTransform;

    void Awake()
    {
        characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        currentPlayer = Instantiate(playerPrefabs[characterIndex], lastCheckPointPos, Quaternion.identity);


        cameraFollowTransform = currentPlayer.transform;
    }

    void Update()
    {

        LookAtObject lookAtObject = currentPlayer.GetComponentInChildren<LookAtObject>();
        if (lookAtObject != null)
        {
            lookAtObject.SetObjectToLookAt(cameraFollowTransform);
        }
        // Kamera kontrolünü güncel karakterin rotasyonuna bağlamak için gerekli kodlar
        CameraController cameraController = FindObjectOfType<CameraController>();
        if (cameraController != null)
        {
            cameraController.followTrans = cameraFollowTransform;
        }
    }


}