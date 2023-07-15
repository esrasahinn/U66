using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buton3Rifle : MonoBehaviour
{
    private bool canDoldurmaAktif = false; // Can doldurma durumu
    private expController controller;
    private PlayerBehaviour player;
    private Healthbar _healthbar; // _healthbar referansý eklendi
    public Image buton4;
    public GameObject healPrefab; // Heal prefabý eklendi
    private GameObject healInstance; // Heal prefabý örneði eklendi
    public Text countdownText; // UI metin öðesi
    [SerializeField]
    private int coinCost = 5; // Alým için gereken coin miktarý

    private void Awake()
    {
        controller = FindObjectOfType<expController>();
        player = PlayerBehaviour.GetInstance();
        _healthbar = FindObjectOfType<Healthbar>(); // _healthbar referansý alýndý
    }

    public void ButonTiklama()
    {
        CollectCoin collectCoinScript = FindObjectOfType<CollectCoin>();
        if (collectCoinScript != null && collectCoinScript.coinAmount >= coinCost)
        {
            collectCoinScript.coinAmount -= coinCost; // Coin miktarýndan düþülüyor
            PlayerPrefs.SetInt("CoinAmount", collectCoinScript.coinAmount);
            collectCoinScript.coinUI.text = collectCoinScript.coinAmount.ToString();

            PlayerBehaviour.GetInstance().PerformLeftShiftAction();
            controller.HidePopup();
            controller.ResumeGame(); // Oyunu devam ettir
            Debug.Log("Karakterin caný dolduruldu.");
            countdownText.text = "5"; // Metin öðesini güncelle
            countdownText.gameObject.SetActive(true); // Metin öðesini etkinleþtir
            buton4.gameObject.SetActive(true); // Resmi etkinleþtir

            // Heal prefabýný oluþtur ve player'ýn alt nesnesi yap
            healInstance = Instantiate(healPrefab, player.transform.position, Quaternion.identity);
            healInstance.transform.parent = player.transform;

            // Heal prefabýný aktifleþtir
            healInstance.SetActive(true);

            InvokeRepeating(nameof(UpdateCountdown), 1f, 1f); // Saniyede bir geri sayýmý güncelle
        }
        else
        {
            Debug.Log("Yeterli coininiz yok.");
        }
    }

    public void PlayerHeal(int healing)
    {
        player.PlayerHeal(healing); // PlayerBehaviour sýnýfýndaki PlayerHeal metodunu çaðýr
        _healthbar.SetHealth(player._health);
        controller.HidePopup();
    }

    private void UpdateCountdown()
    {
        int remainingTime = int.Parse(countdownText.text); // Geri sayým süresini al

        remainingTime--; // Geri sayým süresini azalt
        countdownText.text = remainingTime.ToString(); // Metin öðesini güncelle

        if (remainingTime <= 0)
        {
            CancelInvoke(nameof(UpdateCountdown));
            countdownText.text = ""; // Metin öðesini temizle
            countdownText.gameObject.SetActive(false); // Metin öðesini devre dýþý býrak
            buton4.gameObject.SetActive(false); // Resmi devre dýþý býrak
            Debug.Log("Geri sayým tamamlandý.");

            // Heal prefabýný kapat
            if (healInstance != null)
            {
                healInstance.SetActive(false);
                Destroy(healInstance);
            }
        }
    }
}

