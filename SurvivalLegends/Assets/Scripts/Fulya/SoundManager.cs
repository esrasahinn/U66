using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private bool isMuted;
    public Button muteButton;

    void Start()
    {
        // Baþlangýçta seslerin açýk olduðunu varsayalým
        isMuted = false;
        muteButton.onClick.AddListener(ToggleSound);
    }

    void ToggleSound()
    {
        // Sesi aç veya kapat
        isMuted = !isMuted;

        // Tüm ses kaynaklarýný al
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        // Tüm ses kaynaklarýnýn durumunu güncelle
        foreach (AudioSource source in audioSources)
        {
            source.mute = isMuted;
        }

        // Butonun metnini güncelle
        Text buttonText = muteButton.GetComponentInChildren<Text>();
        buttonText.text = isMuted ? "Sesi Aç" : "Sesi Kapat";
    }
}