using UnityEngine;
using UnityEngine.UI;

public class OzelYetenekButton : MonoBehaviour
{
    public ArcherMenzileGirenDusmanaAtesVeDonme archerScript;
    public Button button;
    AudioSource audiosource;
    private void Start()
    {
        button.onClick.AddListener(Aktiflestir);
        audiosource = GetComponent<AudioSource>();
    }

    private void Aktiflestir()
    {
        archerScript.AktiflestirOzelYetenek();
        audiosource.Play();
    }
}