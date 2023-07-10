using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Buton1Ninja : MonoBehaviour
{
    private bool isSpeedBoostActive = false;
    private float speedBoostDuration = 20f;
    private float originalMoveSpeed;
    private NinjaPlayer player;
    private expController controller;


    private void Awake()
    {
        controller = FindObjectOfType<expController>();
        player = FindObjectOfType<NinjaPlayer>();
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
                Debug.Log("Hýzlanma süresi bitti, hareket hýzý normale döndü.");
            }
        }
    }

    public void ButonTiklama()
    {
        if (!isSpeedBoostActive)
        {
            isSpeedBoostActive = true;
            originalMoveSpeed = player.moveSpeed;
            player.moveSpeed += 10f; // Hareket hýzýný 10 birim artýr
            controller.HidePopup();
            controller.ResumeGame(); // Oyunu devam ettir
            Invoke(nameof(DisableSpeedBoost), speedBoostDuration);
        }
    }

    private void DisableSpeedBoost()
    {
        isSpeedBoostActive = false;
        player.moveSpeed = originalMoveSpeed;
        Debug.Log("Hýzlanma süresi bitti, hareket hýzý normale döndü.");
    }
}
