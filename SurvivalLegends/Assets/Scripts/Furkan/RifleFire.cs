using UnityEngine;
using UnityEngine.UI;

public class RifleFire : MonoBehaviour
{
    public MenzileGirenDusmanaAtesVeDonme rifleScript;
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(Aktiflestir);
    }

    private void Aktiflestir()
    {
        rifleScript.AktiflestirOzelYetenek();
    }
}