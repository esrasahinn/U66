using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buton3Rifle : MonoBehaviour
{
    private bool canDoldurmaAktif = false; // Can doldurma durumu
    private expController controller;
    private PlayerBehaviour player;
    private Healthbar _healthbar; // _healthbar referans� eklendi
    public Image buton4;
    public GameObject healPrefab; // Heal prefab� eklendi
    private GameObject healInstance; // Heal prefab� �rne�i eklendi
    public Text countdownText; // UI metin ��esi
    [SerializeField]
    private int coinCost = 5; // Al�m i�in gereken coin miktar�

    private void Awake()
    {
        controller = FindObjectOfType<expController>();
        player = PlayerBehaviour.GetInstance();
        _healthbar = FindObjectOfType<Healthbar>(); // _healthbar referans� al�nd�
    }

    public void ButonTiklama()
    {
        CollectCoin collectCoinScript = FindObjectOfType<CollectCoin>();
        if (collectCoinScript != null && collectCoinScript.coinAmount >= coinCost)
        {
            collectCoinScript.coinAmount -= coinCost; // Coin miktar�ndan d���l�yor
            PlayerPrefs.SetInt("CoinAmount", collectCoinScript.coinAmount);
            collectCoinScript.coinUI.text = collectCoinScript.coinAmount.ToString();

            PlayerBehaviour.GetInstance().PerformLeftShiftAction();
            controller.HidePopup();
            controller.ResumeGame(); // Oyunu devam ettir
            Debug.Log("Karakterin can� dolduruldu.");
            countdownText.text = "5"; // Metin ��esini g�ncelle
            countdownText.gameObject.SetActive(true); // Metin ��esini etkinle�tir
            buton4.gameObject.SetActive(true); // Resmi etkinle�tir

            // Heal prefab�n� olu�tur ve player'�n alt nesnesi yap
            healInstance = Instantiate(healPrefab, player.transform.position, Quaternion.identity);
            healInstance.transform.parent = player.transform;

            // Heal prefab�n� aktifle�tir
            healInstance.SetActive(true);

            InvokeRepeating(nameof(UpdateCountdown), 1f, 1f); // Saniyede bir geri say�m� g�ncelle
        }
        else
        {
            Debug.Log("Yeterli coininiz yok.");
        }
    }

    public void PlayerHeal(int healing)
    {
        player.PlayerHeal(healing); // PlayerBehaviour s�n�f�ndaki PlayerHeal metodunu �a��r
        _healthbar.SetHealth(player._health);
        controller.HidePopup();
    }

    private void UpdateCountdown()
    {
        int remainingTime = int.Parse(countdownText.text); // Geri say�m s�resini al

        remainingTime--; // Geri say�m s�resini azalt
        countdownText.text = remainingTime.ToString(); // Metin ��esini g�ncelle

        if (remainingTime <= 0)
        {
            CancelInvoke(nameof(UpdateCountdown));
            countdownText.text = ""; // Metin ��esini temizle
            countdownText.gameObject.SetActive(false); // Metin ��esini devre d��� b�rak
            buton4.gameObject.SetActive(false); // Resmi devre d��� b�rak
            Debug.Log("Geri say�m tamamland�.");

            // Heal prefab�n� kapat
            if (healInstance != null)
            {
                healInstance.SetActive(false);
                Destroy(healInstance);
            }
        }
    }
}

