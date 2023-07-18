using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameGemUI : MonoBehaviour
{
    public ShopManager shopManager;
    public TMP_Text gemCountText;

    private void Start()
    {
    }

    private void Update()
    {
        gemCountText.text = shopManager.diamondScript.diamondAmount.ToString();
    }
}