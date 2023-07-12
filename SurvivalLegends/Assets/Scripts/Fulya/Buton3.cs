using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buton3 : MonoBehaviour
{
    private bool canDoldurmaAktif = false; // Can doldurma durumu
    private expController controller;
    private ArcherPlayerBehaviour player;
    private Healthbar _healthbar; // _healthbar referansý eklendi
    public Image buton4;
    public Text countdownText;

    [SerializeField]
    private int coinCost = 5; // Alým için gereken coin miktarý

    private void Awake()
    {
        controller = FindObjectOfType<expController>();
        player = ArcherPlayerBehaviour.GetInstance();
        _healthbar = FindObjectOfType<Healthbar>();
    }

    public void ButonTiklama()
    {
        int playerCoins = PlayerPrefs.GetInt("CoinAmount", 0); // Oyuncunun sahip olduðu coin miktarý

        if (playerCoins >= coinCost && !canDoldurmaAktif)
        {
            playerCoins -= coinCost; // Coinlerden düþülüyor
            PlayerPrefs.SetInt("CoinAmount", playerCoins);

            canDoldurmaAktif = true;
            controller.HidePopup();
            controller.ResumeGame();
            Debug.Log("Karakterin caný dolduruldu.");

            // Coin sayýsýný güncelle
            CollectCoin collectCoinScript = FindObjectOfType<CollectCoin>();
            if (collectCoinScript != null)
            {
                collectCoinScript.coinAmount = playerCoins;
                collectCoinScript.coinUI.text = playerCoins.ToString();
            }

            countdownText.text = "5";
            countdownText.gameObject.SetActive(true);
            buton4.gameObject.SetActive(true);

            InvokeRepeating(nameof(UpdateCountdown), 1f, 1f);
        }
        else
        {
            Debug.Log("Yeterli coininiz yok veya can doldurma zaten aktif.");
        }
    }

    public void PlayerHeal(int healing)
    {
        player.PlayerHeal(healing);
        _healthbar.SetHealth(player._health);
        controller.HidePopup();
    }

    private void UpdateCountdown()
    {
        int remainingTime = int.Parse(countdownText.text);
        countdownText.text = (remainingTime - 1).ToString();

        if (remainingTime <= 1)
        {
            CancelInvoke(nameof(UpdateCountdown));
            countdownText.text = "";
            countdownText.gameObject.SetActive(false);
            buton4.gameObject.SetActive(false);
            Debug.Log("Geri sayým tamamlandý.");
        }
    }
}
