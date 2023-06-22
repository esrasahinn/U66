using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ButtonCooldown : MonoBehaviour
{
    public Button button; // Kullanýlacak buton
    public float cooldownTime = 5f; // Cooldown süresi (saniye)
    private bool isCooldown = false; // Cooldown durumu
    [SerializeField] private TMP_Text cooldownText; // Geriye doðru sayan metin

    void Start()
    {
        button.onClick.AddListener(StartCooldown); // Butona týklandýðýnda cooldown baþlatýlýr
    }

    void Update()
    {
        if (isCooldown)
        {
            button.interactable = false; // Buton etkileþimini devre dýþý býrak
            float remainingTime = cooldownTime - (Time.time % cooldownTime); // Kalan süreyi hesapla
            cooldownText.text = Mathf.CeilToInt(remainingTime).ToString(); // Metne kalan süreyi yaz
        }
        else
        {
            button.interactable = true; // Buton etkileþimini etkinleþtir
            cooldownText.text = ""; // Metni boþalt
        }
    }

    void StartCooldown()
    {
        if (!isCooldown)
        {
            StartCoroutine(Cooldown()); // Cooldown sürecini baþlat
        }
    }

    IEnumerator Cooldown()
    {
        isCooldown = true;

        yield return new WaitForSeconds(cooldownTime);

        isCooldown = false;
    }
}