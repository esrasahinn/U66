using UnityEngine;
using UnityEngine.UI;

public class Buton2 : MonoBehaviour
{
    public float hasarAlmamaSure = 10f; // Hasar almama süresi (saniye)
    private bool hasarAlmamaAktif = false; // Hasar almama durumu
    private float hasarAlmamaSureKalan = 0f; // Geriye kalan hasar almama süresi
    private expController controller;
    private ArcherPlayerBehaviour arcPlayerBehaviour;
    private PlayerBehaviour PlayerBehaviour;
    public Image buton2;
    public Text countdownText; // UI metin öðesi

    private void Awake()
    {
        controller = FindObjectOfType<expController>();
        arcPlayerBehaviour = FindObjectOfType<ArcherPlayerBehaviour>();
        PlayerBehaviour = FindObjectOfType<PlayerBehaviour>();
    }

    private void Update()
    {
        if (hasarAlmamaAktif)
        {
            if (hasarAlmamaSureKalan > 0f)
            {
                hasarAlmamaSureKalan -= Time.deltaTime;

                if (hasarAlmamaSureKalan <= 0f)
                {
                    hasarAlmamaAktif = false;
                    arcPlayerBehaviour.DeactivateImmunity();
                    // PlayerBehaviour.DeactivateImmunity();
                    Debug.Log("Hasar alma süresi doldu.");
                }
                if (hasarAlmamaSureKalan <= 0f)
                {
                    hasarAlmamaAktif = false;
                    PlayerBehaviour.DeactivateImmunity();
                    Debug.Log("Hasar alma süresi doldu.");
                }
            }
        }
    }

    public void ButonTiklama()
    {
        if (!hasarAlmamaAktif)
        {
            hasarAlmamaAktif = true;
            controller.HidePopup();
            controller.ResumeGame();
            hasarAlmamaSureKalan = hasarAlmamaSure;
            Debug.Log("Hasar almama süresi baþladý.");

            arcPlayerBehaviour.ActivateImmunity(hasarAlmamaSure);
            //PlayerBehaviour.ActivateImmunity(hasarAlmamaSure);

            countdownText.text = "10"; // Metin öðesini güncelle
            countdownText.gameObject.SetActive(true); // Metin öðesini etkinleþtir
            buton2.gameObject.SetActive(true); // Resmi etkinleþtir

            InvokeRepeating(nameof(UpdateCountdown), 1f, 1f); // Saniyede bir geri sayýmý güncelle
        }
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
            buton2.gameObject.SetActive(false); // Resmi devre dýþý býrak
            Debug.Log("Geri sayým tamamlandý.");
        }
    }
}
