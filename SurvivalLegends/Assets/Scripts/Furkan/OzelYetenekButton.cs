using UnityEngine;
using UnityEngine.UI;

public class OzelYetenekButton : MonoBehaviour
{
    public ArcherMenzileGirenDusmanaAtesVeDonme archerScript;
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(Aktiflestir);
    }

    private void Aktiflestir()
    {
        archerScript.AktiflestirOzelYetenek();
    }
}