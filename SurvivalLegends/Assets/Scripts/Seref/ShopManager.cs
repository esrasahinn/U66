using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public CollectCoin coinScript;
    public TMP_Text coinUI;
    public ShopItemSO[] shopItemSO;
    public ShopTemplate[] shopPanels;
    public GameObject[] shopPanelsGO;
    public Button[] purchaseBtns;
    void Start()
    {
        for (int i = 0; i<shopItemSO.Length; i++)
        {
            shopPanelsGO[i].gameObject.SetActive(true);
        }
        coinUI.text = "Coins: " + coinScript.coinAmount.ToString();
        LoadPanels();
        CheckPurchasable();
    }

    void Update()
    {

    }

 
   public void LoadPanels()
    {
        for (int i=0; i<shopItemSO.Length; i++)
        {
            shopPanels[i].costTxt.text = shopItemSO[i].baseCost.ToString();
        }
    }

    public void CheckPurchasable()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            if (coinScript.coinAmount >= shopItemSO[i].baseCost)
                purchaseBtns[i].interactable= true;
            else
                purchaseBtns[i].interactable= false;
        }
    }

    public void PurchaseItem(int btnNo)
    {
        if (coinScript.coinAmount >= shopItemSO[btnNo].baseCost)
        {
            coinScript.coinAmount = coinScript.coinAmount - shopItemSO[btnNo].baseCost;       
            coinUI.text = "Coins: " + coinScript.coinAmount.ToString();
            PlayerPrefs.SetInt("CoinAmount", coinScript.coinAmount);
            CheckPurchasable();
        }
    }
}