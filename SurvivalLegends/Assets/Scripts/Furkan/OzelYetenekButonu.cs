using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OzelYetenekButonu : MonoBehaviour
{
    private MenzileGirenDusmanaAtesVeDonme menzileGirenDusmanaAtesVeDonme;

    private void Start()
    {
        // MenzileGirenDusmanaAtesVeDonme scriptine eri�im sa�la
        menzileGirenDusmanaAtesVeDonme = FindObjectOfType<MenzileGirenDusmanaAtesVeDonme>();

        // Butonun t�klama olay�n� dinle
        Button button = GetComponent<Button>();
        button.onClick.AddListener(AktifEtOzelYetenek);
    }

    private void AktifEtOzelYetenek()
    {
        // �zel yetene�i aktifle�tir
        if (menzileGirenDusmanaAtesVeDonme != null)
        {
            menzileGirenDusmanaAtesVeDonme.AktifEtOzelYetenek();
        }
    }
}