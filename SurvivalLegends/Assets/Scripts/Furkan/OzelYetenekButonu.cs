using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OzelYetenekButonu : MonoBehaviour
{
    private MenzileGirenDusmanaAtesVeDonme menzileGirenDusmanaAtesVeDonme;

    private void Start()
    {
        // MenzileGirenDusmanaAtesVeDonme scriptine eriþim saðla
        menzileGirenDusmanaAtesVeDonme = FindObjectOfType<MenzileGirenDusmanaAtesVeDonme>();

        // Butonun týklama olayýný dinle
        Button button = GetComponent<Button>();
        button.onClick.AddListener(AktifEtOzelYetenek);
    }

    private void AktifEtOzelYetenek()
    {
        // Özel yeteneði aktifleþtir
        if (menzileGirenDusmanaAtesVeDonme != null)
        {
            menzileGirenDusmanaAtesVeDonme.AktifEtOzelYetenek();
        }
    }
}