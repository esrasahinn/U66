using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrozenButton : MonoBehaviour
{
    public ArcherMenzileGirenDusmanaAtesVeDonme archerScript; // ArcherMenzileGirenDusmanaAtesVeDonme script referans�
    AudioSource audiosource;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        audiosource = GetComponent<AudioSource>();
        if (button != null)
        {
            button.onClick.AddListener(AtesEt); // Butona t�klama olay�na AtesEt metodunu ekle
        }
    }

    private void AtesEt()
    {
        archerScript.AtisYap(); // ArcherMenzileGirenDusmanaAtesVeDonme scriptindeki AtesYap metodunu �a��r
        audiosource.Play();
    }
}