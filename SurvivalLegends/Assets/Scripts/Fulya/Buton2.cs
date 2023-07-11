using UnityEngine;
using UnityEngine.UI;

public class Buton2 : MonoBehaviour
{
    public float hasarAlmamaSure = 10f; // Hasar almama s�resi (saniye)
    private bool hasarAlmamaAktif = false; // Hasar almama durumu
    private float hasarAlmamaSureKalan = 0f; // Geriye kalan hasar almama s�resi
    private expController controller;
    private ArcherPlayerBehaviour arcPlayerBehaviour;
    private PlayerBehaviour PlayerBehaviour;
    public Image buton2;
    public Text countdownText; // UI metin ��esi

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
                    Debug.Log("Hasar alma s�resi doldu.");
                }
                if (hasarAlmamaSureKalan <= 0f)
                {
                    hasarAlmamaAktif = false;
                    PlayerBehaviour.DeactivateImmunity();
                    Debug.Log("Hasar alma s�resi doldu.");
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
            Debug.Log("Hasar almama s�resi ba�lad�.");

            arcPlayerBehaviour.ActivateImmunity(hasarAlmamaSure);
            //PlayerBehaviour.ActivateImmunity(hasarAlmamaSure);

            countdownText.text = "10"; // Metin ��esini g�ncelle
            countdownText.gameObject.SetActive(true); // Metin ��esini etkinle�tir
            buton2.gameObject.SetActive(true); // Resmi etkinle�tir

            InvokeRepeating(nameof(UpdateCountdown), 1f, 1f); // Saniyede bir geri say�m� g�ncelle
        }
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
            buton2.gameObject.SetActive(false); // Resmi devre d��� b�rak
            Debug.Log("Geri say�m tamamland�.");
        }
    }
}
