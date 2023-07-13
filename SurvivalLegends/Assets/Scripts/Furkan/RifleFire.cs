using UnityEngine;
using UnityEngine.UI;

public class RifleFire : MonoBehaviour
{
    public MenzileGirenDusmanaAtesVeDonme rifleScript;
    public Button button;
    AudioSource audiosource;

    private void Start()
    {
        button.onClick.AddListener(Aktiflestir);
        audiosource = GetComponent<AudioSource>();
    }

    private void Aktiflestir()
    {
        rifleScript.AktiflestirOzelYetenek();
         audiosource.Play();
    }
}