using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ButtonCooldown : MonoBehaviour
{
    public Button button; // Kullan�lacak buton
    public float cooldownTime = 5f; // Cooldown s�resi (saniye)
    private bool isCooldown = false; // Cooldown durumu
    private float startTime; // Ba�lang�� zaman�
    [SerializeField] private TMP_Text cooldownText; // Geriye do�ru sayan metin
    [SerializeField] private Image cooldownFillImage; // Dolum g�r�nt�s�

    void Start()
    {
        button.onClick.AddListener(StartCooldown); // Butona t�kland���nda cooldown ba�lat�l�r
    }

    void Update()
    {
        if (isCooldown)
        {
            button.interactable = false; // Buton etkile�imini devre d��� b�rak
            float elapsedTime = Time.time - startTime; // Ge�en s�reyi hesapla
            float remainingTime = cooldownTime - elapsedTime; // Kalan s�reyi hesapla

            if (remainingTime <= 0f)
            {
                isCooldown = false;
                cooldownText.text = ""; // Metni bo�alt
                cooldownFillImage.fillAmount = 0f; // Dolum g�r�nt�s�n� s�f�rla
            }
            else
            {
                cooldownText.text = Mathf.CeilToInt(remainingTime).ToString(); // Metne kalan s�reyi yaz
                cooldownFillImage.fillAmount = remainingTime / cooldownTime; // Dolum g�r�nt�s�n� ayarla
            }
        }
        else
        {
            button.interactable = true; // Buton etkile�imini etkinle�tir
            cooldownText.text = ""; // Metni bo�alt
            cooldownFillImage.fillAmount = 0f; // Dolum g�r�nt�s�n� s�f�rla
        }
    }

    void StartCooldown()
    {
        if (!isCooldown)
        {
            startTime = Time.time; // Ba�lang�� zaman�n� kaydet
            isCooldown = true;
        }
    }
}