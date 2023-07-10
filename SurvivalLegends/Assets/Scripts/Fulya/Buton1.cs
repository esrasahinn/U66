using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buton1 : MonoBehaviour
{
    private bool isSpeedBoostActive = false;
    private float speedBoostDuration = 20f;
    private float originalMoveSpeed;
    private Player player;
    private expController controller;
    public Image buton1;
    public Text countdownText; // UI metin öðesi

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

            if (speedBoostDuration <= 0)
            {
                isSpeedBoostActive = false;
                player.moveSpeed = originalMoveSpeed;
                Debug.Log("Hýzlanma süresi bitti, hareket hýzý normale döndü.");
                countdownText.text = ""; // Metin öðesini temizle
                countdownText.gameObject.SetActive(false); // Metin öðesini devre dýþý býrak
                buton1.gameObject.SetActive(false);
            }
            else
            {
                int remainingTime = Mathf.CeilToInt(speedBoostDuration);
                countdownText.text = "Kalan Süre: " + remainingTime.ToString(); // Metin öðesini güncelle
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
            countdownText.text = "Kalan Süre: " + Mathf.CeilToInt(speedBoostDuration).ToString(); // Metin öðesini güncelle
            countdownText.gameObject.SetActive(true); // Metin öðesini etkinleþtir
            buton1.gameObject.SetActive(true);
            controller.HidePopup();
            controller.ResumeGame(); // Oyunu devam ettir
            InvokeRepeating(nameof(UpdateCountdown), 1f, 1f); // Saniyede bir geri sayýmý güncelle
            Invoke(nameof(DisableSpeedBoost), speedBoostDuration);
        }
    }

    private void UpdateCountdown()
    {
        int remainingTime = Mathf.CeilToInt(speedBoostDuration);
        countdownText.text = "Kalan Süre: " + remainingTime.ToString(); // Metin öðesini güncelle
        speedBoostDuration -= 1f;

        if (speedBoostDuration <= 0)
        {
            CancelInvoke(nameof(UpdateCountdown));
        }
    }

    private void DisableSpeedBoost()
    {
        isSpeedBoostActive = false;
        player.moveSpeed = originalMoveSpeed;
        Debug.Log("Hýzlanma süresi bitti, hareket hýzý normale döndü.");
        countdownText.text = ""; // Metin öðesini temizle
        countdownText.gameObject.SetActive(false); // Metin öðesini devre dýþý býrak
        buton1.gameObject.SetActive(false);
    }
}

