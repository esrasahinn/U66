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
    public Text countdownText; // UI metin ��esi

    [SerializeField]
    private int coinCost = 5; // Al�m i�in gereken coin miktar�

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
                Debug.Log("H�zlanma s�resi bitti, hareket h�z� normale d�nd�.");
                countdownText.text = ""; // Metin ��esini temizle
                countdownText.gameObject.SetActive(false); // Metin ��esini devre d��� b�rak
                buton1.gameObject.SetActive(false);
            }
            else
            {
                int remainingTime = Mathf.CeilToInt(speedBoostDuration);
                countdownText.text = remainingTime.ToString(); // Metin ��esini g�ncelle
            }
        }
    }

    public void ButonTiklama()
    {
        CollectCoin collectCoinScript = FindObjectOfType<CollectCoin>();
        if (collectCoinScript != null && collectCoinScript.coinAmount >= coinCost)
        {
            collectCoinScript.coinAmount -= coinCost; // Coin miktar�ndan d���l�yor
            PlayerPrefs.SetInt("CoinAmount", collectCoinScript.coinAmount);
            collectCoinScript.coinUI.text = collectCoinScript.coinAmount.ToString();

            if (!isSpeedBoostActive)
            {
                isSpeedBoostActive = true;
                originalMoveSpeed = player.moveSpeed;
                player.moveSpeed += 10f; // Hareket h�z�n� 10 birim art�r
                countdownText.text = Mathf.CeilToInt(speedBoostDuration).ToString(); // Metin ��esini g�ncelle
                countdownText.gameObject.SetActive(true); // Metin ��esini etkinle�tir
                buton1.gameObject.SetActive(true);
                controller.HidePopup();
                controller.ResumeGame(); // Oyunu devam ettir
                InvokeRepeating(nameof(UpdateCountdown), 1f, 1f); // Saniyede bir geri say�m� g�ncelle
                Invoke(nameof(DisableSpeedBoost), speedBoostDuration);
            }
        }
        else
        {
            Debug.Log("Yeterli coininiz yok.");
        }
    }

    private void UpdateCountdown()
    {
        int remainingTime = Mathf.CeilToInt(speedBoostDuration);
        countdownText.text = remainingTime.ToString(); // Metin ��esini g�ncelle
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
        Debug.Log("H�zlanma s�resi bitti, hareket h�z� normale d�nd�.");
        countdownText.text = ""; // Metin ��esini temizle
        countdownText.gameObject.SetActive(false); // Metin ��esini devre d��� b�rak
        buton1.gameObject.SetActive(false);
    }
}
