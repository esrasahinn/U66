using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ButtonCooldown : MonoBehaviour
{
    public Button button; // Kullanýlacak buton
    public float cooldownTime = 5f; // Cooldown süresi (saniye)
    private bool isCooldown = false; // Cooldown durumu
    private float startTime; // Baþlangýç zamaný
    [SerializeField] private TMP_Text cooldownText; // Geriye doðru sayan metin
    [SerializeField] private Image cooldownFillImage; // Dolum görüntüsü

    void Start()
    {
        button.onClick.AddListener(StartCooldown); // Butona týklandýðýnda cooldown baþlatýlýr
    }

    void Update()
    {
        if (isCooldown)
        {
            button.interactable = false; // Buton etkileþimini devre dýþý býrak
            float elapsedTime = Time.time - startTime; // Geçen süreyi hesapla
            float remainingTime = cooldownTime - elapsedTime; // Kalan süreyi hesapla

            if (remainingTime <= 0f)
            {
                isCooldown = false;
                cooldownText.text = ""; // Metni boþalt
                cooldownFillImage.fillAmount = 0f; // Dolum görüntüsünü sýfýrla
            }
            else
            {
                cooldownText.text = Mathf.CeilToInt(remainingTime).ToString(); // Metne kalan süreyi yaz
                cooldownFillImage.fillAmount = remainingTime / cooldownTime; // Dolum görüntüsünü ayarla
            }
        }
        else
        {
            button.interactable = true; // Buton etkileþimini etkinleþtir
            cooldownText.text = ""; // Metni boþalt
            cooldownFillImage.fillAmount = 0f; // Dolum görüntüsünü sýfýrla
        }
    }

    void StartCooldown()
    {
        if (!isCooldown)
        {
            startTime = Time.time; // Baþlangýç zamanýný kaydet
            isCooldown = true;
        }
    }
}