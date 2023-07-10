using UnityEngine;

public class Buton2 : MonoBehaviour
{
    public float hasarAlmamaSure = 10f; // Hasar almama süresi (saniye)
    private bool hasarAlmamaAktif = false; // Hasar almama durumu
    private float hasarAlmamaSureKalan = 0f; // Geriye kalan hasar almama süresi
    private expController controller;
    private ArcherPlayerBehaviour arcPlayerBehaviour;
    private PlayerBehaviour PlayerBehaviour;


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
        }

    }

    public void ButonTiklamaRifle()
    {
        if (!hasarAlmamaAktif)
        {
            hasarAlmamaAktif = true;
            controller.HidePopup();
            controller.ResumeGame();
            hasarAlmamaSureKalan = hasarAlmamaSure;
            Debug.Log("Hasar almama süresi baþladý.");

            //arcPlayerBehaviour.ActivateImmunity(hasarAlmamaSure);
            PlayerBehaviour.ActivateImmunity(hasarAlmamaSure);
        }
    }
}
