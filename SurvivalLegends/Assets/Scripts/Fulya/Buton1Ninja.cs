using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buton1Ninja : MonoBehaviour
{
    private bool isSpeedBoostActive = false;
    private float speedBoostDuration = 20f;
    private float originalMoveSpeed;
    private NinjaPlayer player;
    private expController controller;
    public Image buton1;
    public Text countdownText;

    [SerializeField]
    private int coinCost = 5; // Alým için gereken coin miktarý

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
                countdownText.text = "";
                countdownText.gameObject.SetActive(false);
                buton1.gameObject.SetActive(false);
            }
            else
            {
                int remainingTime = Mathf.CeilToInt(speedBoostDuration);
                countdownText.text = remainingTime.ToString();
            }
        }
    }

    public void ButonTiklama()
    {
        int playerCoins = PlayerPrefs.GetInt("CoinAmount", 0); // Oyuncunun sahip olduðu coin miktarý

        if (playerCoins >= coinCost && !isSpeedBoostActive)
        {
            playerCoins -= coinCost; // Coinlerden düþülüyor
            PlayerPrefs.SetInt("CoinAmount", playerCoins);

            isSpeedBoostActive = true;
            originalMoveSpeed = player.moveSpeed;
            player.moveSpeed += 10f;
            countdownText.text = Mathf.CeilToInt(speedBoostDuration).ToString();
            countdownText.gameObject.SetActive(true);
            buton1.gameObject.SetActive(true);
            controller.HidePopup();
            controller.ResumeGame();
            InvokeRepeating(nameof(UpdateCountdown), 1f, 1f);
            Invoke(nameof(DisableSpeedBoost), speedBoostDuration);

            // Coin sayýsýný güncelle
            CollectCoin collectCoinScript = FindObjectOfType<CollectCoin>();
            if (collectCoinScript != null)
            {
                collectCoinScript.coinAmount = playerCoins;
                collectCoinScript.coinUI.text = playerCoins.ToString();
            }
        }
        else
        {
            Debug.Log("Yeterli coininiz yok veya hýzlanma zaten aktif.");
        }
    }

    private void UpdateCountdown()
    {
        int remainingTime = Mathf.CeilToInt(speedBoostDuration);
        countdownText.text = remainingTime.ToString();
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
        countdownText.text = "";
        countdownText.gameObject.SetActive(false);
        buton1.gameObject.SetActive(false);
    }
}
