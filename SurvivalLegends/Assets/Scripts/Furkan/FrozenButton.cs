using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrozenButton : MonoBehaviour
{
    public ArcherMenzileGirenDusmanaAtesVeDonme archerScript; // ArcherMenzileGirenDusmanaAtesVeDonme script referansý

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(AtesEt); // Butona týklama olayýna AtesEt metodunu ekle
        }
    }

    private void AtesEt()
    {
        archerScript.AtisYap(); // ArcherMenzileGirenDusmanaAtesVeDonme scriptindeki AtesYap metodunu çaðýr
    }
}