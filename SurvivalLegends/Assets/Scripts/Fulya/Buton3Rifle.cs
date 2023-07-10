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
    public Text countdownText; // UI metin ��esi
    private void Awake()
    {
        controller = FindObjectOfType<expController>();
        player = PlayerBehaviour.GetInstance();
        _healthbar = FindObjectOfType<Healthbar>(); // _healthbar referans� al�nd�
    }
    public void ButonTiklama()
    {
        PlayerBehaviour.GetInstance().PerformLeftShiftAction();
        controller.HidePopup();
        controller.ResumeGame(); // Oyunu devam ettir
        Debug.Log("Karakterin can� dolduruldu.");
        countdownText.text = "5"; // Metin ��esini g�ncelle
        countdownText.gameObject.SetActive(true); // Metin ��esini etkinle�tir
        buton4.gameObject.SetActive(true); // Resmi etkinle�tir

        InvokeRepeating(nameof(UpdateCountdown), 1f, 1f); // Saniyede bir geri say�m� g�ncelle
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
        }
    }
}
