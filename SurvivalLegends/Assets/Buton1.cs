using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Buton1 : MonoBehaviour
{
    private bool isSpeedBoostActive = false;
    private float speedBoostDuration = 30f;
    private float originalMoveSpeed;
    private Player player;
    private expController controller;


    private void Awake()
    {
        controller = FindObjectOfType<expController>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (isSpeedBoostActive)
        {
            speedBoostDuration -= Time.deltaTime;

            if (speedBoostDuration <= 0f)
            {
                isSpeedBoostActive = false;
                player.moveSpeed = originalMoveSpeed;
                Debug.Log("H�zlanma s�resi bitti, hareket h�z� normale d�nd�.");
            }
        }
    }

    public void ButonTiklama()
    {
        if (!isSpeedBoostActive)
        {
            isSpeedBoostActive = true;
            originalMoveSpeed = player.moveSpeed;
            player.moveSpeed += 10f; // Hareket h�z�n� 10 birim art�r
            controller.HidePopup();
            Debug.Log("Karakter 30 saniye boyunca h�zland�.");
            Invoke(nameof(DisableSpeedBoost), speedBoostDuration);
        }
    }

    private void DisableSpeedBoost()
    {
        isSpeedBoostActive = false;
        player.moveSpeed = originalMoveSpeed;
        Debug.Log("H�zlanma s�resi bitti, hareket h�z� normale d�nd�.");
    }
}