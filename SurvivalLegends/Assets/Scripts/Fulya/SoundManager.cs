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
        // Ba�lang��ta seslerin a��k oldu�unu varsayal�m
        isMuted = false;
        muteButton.onClick.AddListener(ToggleSound);
    }

    void ToggleSound()
    {
        // Sesi a� veya kapat
        isMuted = !isMuted;

        // T�m ses kaynaklar�n� al
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        // T�m ses kaynaklar�n�n durumunu g�ncelle
        foreach (AudioSource source in audioSources)
        {
            source.mute = isMuted;
        }

        // Butonun metnini g�ncelle
        Text buttonText = muteButton.GetComponentInChildren<Text>();
        buttonText.text = isMuted ? "Sesi A�" : "Sesi Kapat";
    }
}