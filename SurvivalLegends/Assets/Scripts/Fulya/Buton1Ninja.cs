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

    private Button button;

    private void Awake()
    {
        controller = FindObjectOfType<expController>();
        player = FindObjectOfType<NinjaPlayer>();
        button = GetComponent<Button>();
        UpdateButtonInteractivity();
    }

    private void Update()
    {
        if (isSpeedBoostActive)
        {
            speedBoostDuration -= Time.deltaTime;

            if (speedBoostDuration <= 0f)
            {
                DisableSpeedBoost();
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
        CollectCoin collectCoinScript = FindObjectOfType<CollectCoin>();
        if (collectCoinScript != null && collectCoinScript.coinAmount >= coinCost && !isSpeedBoostActive)
        {
            int playerCoins = collectCoinScript.coinAmount;

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
            collectCoinScript.coinAmount = playerCoins;
            collectCoinScript.coinUI.text = playerCoins.ToString();

            UpdateButtonInteractivity();
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

        UpdateButtonInteractivity();
    }

    public void UpdateButtonInteractivity()
    {
        CollectCoin collectCoinScript = FindObjectOfType<CollectCoin>();
        if (collectCoinScript != null && collectCoinScript.coinAmount >= coinCost && !isSpeedBoostActive)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
}
