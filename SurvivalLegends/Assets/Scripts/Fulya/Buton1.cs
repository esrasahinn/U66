using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buton1 : MonoBehaviour
{
    private Player player;
    private NinjaPlayer Nplayer;
    private expController controller;
    public Image buton1;
    public Text countdownText;
    public Text coinInsufficientText;
    private Button button;
    private CollectCoin collectCoinScript;
    public int coinCost = 5; // Alým için gereken coin miktarý

    private void Start()
    {
        button = GetComponent<Button>();
        collectCoinScript = FindObjectOfType<CollectCoin>();
        controller = FindObjectOfType<expController>();
        player = FindObjectOfType<Player>();
        Nplayer = FindObjectOfType<NinjaPlayer>();
        UpdateButtonInteractivity();
    }

    public void ButonTiklama()
    {
        if (collectCoinScript != null && collectCoinScript.coinAmount >= coinCost)
        {
            collectCoinScript.coinAmount -= coinCost;
            PlayerPrefs.SetInt("CoinAmount", collectCoinScript.coinAmount);
            collectCoinScript.coinUI.text = collectCoinScript.coinAmount.ToString();

            if (!player.isSpeedBoostActive)
            {
                collectCoinScript.coinUI.text = collectCoinScript.coinAmount.ToString();
                collectCoinScript.coinUI.gameObject.SetActive(true);

                player.isSpeedBoostActive = true;
                player.originalMoveSpeed = player.moveSpeed;
                player.moveSpeed += 10f;
                player.speedBoostDuration = 20f; // Hýzlanma süresini belirle

                countdownText.text = Mathf.CeilToInt(player.speedBoostDuration).ToString();
                countdownText.gameObject.SetActive(true);

                button.interactable = false; // Butonu interactable durumunu pasif yap

                controller.HidePopup();
                controller.ResumeGame();
                InvokeRepeating(nameof(UpdateCountdown), 1f, 1f);
                Invoke(nameof(DisableSpeedBoost), player.speedBoostDuration);
            }
        }
        else
        {
            Debug.Log("Yeterli coininiz yok.");
            StartCoroutine(ShowCoinInsufficientText());
        }
    }

    public void ButonTiklamaNinja()
    {
        if (collectCoinScript != null && collectCoinScript.coinAmount >= coinCost)
        {
            collectCoinScript.coinAmount -= coinCost;
            PlayerPrefs.SetInt("CoinAmount", collectCoinScript.coinAmount);
            collectCoinScript.coinUI.text = collectCoinScript.coinAmount.ToString();

            if (!Nplayer.isSpeedBoostActive)
            {
                collectCoinScript.coinUI.text = collectCoinScript.coinAmount.ToString();
                collectCoinScript.coinUI.gameObject.SetActive(true);

                Nplayer.isSpeedBoostActive = true;
                Nplayer.originalMoveSpeed = Nplayer.moveSpeed;
                Nplayer.moveSpeed += 10f;
                Nplayer.speedBoostDuration = 20f; // Hýzlanma süresini belirle

                countdownText.text = Mathf.CeilToInt(Nplayer.speedBoostDuration).ToString();
                countdownText.gameObject.SetActive(true);

                button.interactable = false; // Butonu interactable durumunu pasif yap

                controller.HidePopup();
                controller.ResumeGame();
                InvokeRepeating(nameof(UpdateCountdown), 1f, 1f);
                Invoke(nameof(DisableSpeedBoost), Nplayer.speedBoostDuration);
            }
        }
        else
        {
            Debug.Log("Yeterli coininiz yok.");
            StartCoroutine(ShowCoinInsufficientText());
        }
    }

    private void UpdateCountdown()
    {
        if (player.isSpeedBoostActive)
        {
            int remainingTime = Mathf.CeilToInt(player.speedBoostDuration);
            countdownText.text = remainingTime.ToString();
            player.speedBoostDuration -= 1f;

            if (player.speedBoostDuration <= 0)
            {
                CancelInvoke(nameof(UpdateCountdown));
                DisableSpeedBoost();
            }
        }
        if (Nplayer.isSpeedBoostActive)
        {
            int remainingTime = Mathf.CeilToInt(Nplayer.speedBoostDuration);
            countdownText.text = remainingTime.ToString();
            Nplayer.speedBoostDuration -= 1f;

            if (Nplayer.speedBoostDuration <= 0)
            {
                CancelInvoke(nameof(UpdateCountdown));
                DisableSpeedBoost();
            }
        }

    }

    private void DisableSpeedBoost()
    {
        if (player.isSpeedBoostActive)
        {
            player.isSpeedBoostActive = false;
            player.moveSpeed = player.originalMoveSpeed;
            Debug.Log("Hýzlanma süresi bitti, hareket hýzý normale döndü.");
            countdownText.text = "";
            countdownText.gameObject.SetActive(false);
            buton1.gameObject.SetActive(false);
            UpdateButtonInteractivity();
        }
        else if (Nplayer != null && Nplayer.isSpeedBoostActive)
        {
            Nplayer.isSpeedBoostActive = false;
            Nplayer.moveSpeed = Nplayer.originalMoveSpeed;
            Debug.Log("Hýzlanma süresi bitti, hareket hýzý normale döndü.");
            countdownText.text = "";
            countdownText.gameObject.SetActive(false);
            buton1.gameObject.SetActive(false);
            UpdateButtonInteractivity();
        }
    }

    private IEnumerator ShowCoinInsufficientText()
    {
        coinInsufficientText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        coinInsufficientText.gameObject.SetActive(false);
    }

    public void UpdateButtonInteractivity()
    {
        if (collectCoinScript != null && collectCoinScript.coinAmount >= coinCost)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
}
