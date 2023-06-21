using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ButtonCooldown : MonoBehaviour
{
    public Button button; // Kullan�lacak buton
    public float cooldownTime = 5f; // Cooldown s�resi (saniye)
    private bool isCooldown = false; // Cooldown durumu
    [SerializeField] private TMP_Text cooldownText; // Geriye do�ru sayan metin

    void Start()
    {
        button.onClick.AddListener(StartCooldown); // Butona t�kland���nda cooldown ba�lat�l�r
    }

    void Update()
    {
        if (isCooldown)
        {
            button.interactable = false; // Buton etkile�imini devre d��� b�rak
            float remainingTime = cooldownTime - (Time.time % cooldownTime); // Kalan s�reyi hesapla
            cooldownText.text = Mathf.CeilToInt(remainingTime).ToString(); // Metne kalan s�reyi yaz
        }
        else
        {
            button.interactable = true; // Buton etkile�imini etkinle�tir
            cooldownText.text = ""; // Metni bo�alt
        }
    }

    void StartCooldown()
    {
        if (!isCooldown)
        {
            StartCoroutine(Cooldown()); // Cooldown s�recini ba�lat
        }
    }

    IEnumerator Cooldown()
    {
        isCooldown = true;

        yield return new WaitForSeconds(cooldownTime);

        isCooldown = false;
    }
}