using UnityEngine;

public class Buton2 : MonoBehaviour
{
    public float hasarAlmamaSure = 30f; // Hasar almama süresi (saniye)
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
            // Hasar almama süresini azalt
            hasarAlmamaSure -= Time.deltaTime;

            if (hasarAlmamaSure <= 0f)
            {
                hasarAlmamaAktif = false;
                Debug.Log("Hasar alma süresi doldu.");
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
            Debug.Log("Hasar almama süresi baþladý.");
        }
    }
}