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
    public TMP_Text priceText; // Reference to the text component showing the price

    private int selectedItemIndex = -1; // Index of the currently selected item

    void Start()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
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
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanels[i].itemCost.text = shopItemSO[i].baseCost.ToString();
        }
    }

    public void CheckPurchasable()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            if (coinScript.coinAmount >= shopItemSO[i].baseCost)
                purchaseBtns[i].interactable = true;
            else
                purchaseBtns[i].interactable = false;
        }
    }

    public void ShowPrice(int itemIndex)
    {
        selectedItemIndex = itemIndex;
        priceText.text = "Price: " + shopItemSO[itemIndex].baseCost.ToString();
    }

    public void PurchaseItem()
    {
        if (selectedItemIndex != -1 && coinScript.coinAmount >= shopItemSO[selectedItemIndex].baseCost)
        {
            coinScript.coinAmount -= shopItemSO[selectedItemIndex].baseCost;
            coinUI.text = "Coins: " + coinScript.coinAmount.ToString();
            PlayerPrefs.SetInt("CoinAmount", coinScript.coinAmount);
            CheckPurchasable();
        }
    }
}
