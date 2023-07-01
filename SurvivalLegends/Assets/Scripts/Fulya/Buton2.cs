using UnityEngine;

public class Buton2 : MonoBehaviour
{
    public float hasarAlmamaSure = 30f; // Hasar almama s�resi (saniye)
    private bool hasarAlmamaAktif = false; // Hasar almama durumu
    private expController controller;


    private void Awake()
    {
        controller = FindObjectOfType<expController>();
    }

    private void Update()
    {
        if (hasarAlmamaAktif)
        {
            // Hasar almama s�resini azalt
            hasarAlmamaSure -= Time.deltaTime;

            if (hasarAlmamaSure <= 0f)
            {
                hasarAlmamaAktif = false;
                Debug.Log("Hasar alma s�resi doldu.");
            }
        }
    }

    public void ButonTiklama()
    {
        if (!hasarAlmamaAktif)
        {
            hasarAlmamaAktif = true;
            controller.HidePopup();
            hasarAlmamaSure = 30f;
            Debug.Log("Hasar almama s�resi ba�lad�.");
        }
    }
}