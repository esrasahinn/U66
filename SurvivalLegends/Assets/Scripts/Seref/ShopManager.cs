using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public CollectCoin coinScript;
    public TMP_Text coinUI;
    public ShopItemSO[] shopItemSO;
    public ShopTemplate[] shopPanels;
    public GameObject[] shopPanelsGO;
    public Button purchaseBtn;
    public Button equipBtn;
    public Button unequipBtn;
    public TMP_Text priceText;
    private int selectedItemIndex = -1;

    void Start()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
        }
        coinUI.text = "Coins: " + coinScript.coinAmount.ToString();
        LoadPanels();
        LoadEquippedItems();
        CheckPurchasable();
    }

    void Update()
    {

    }

    public void LoadPanels()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            int index = i;
            shopPanels[i].itemCost.text = shopItemSO[i].baseCost.ToString();
            shopPanels[i].itemButton.onClick.AddListener(() => ShowPrice(index));

            if (IsItemPurchased(index))
            {
                shopPanels[i].equipButton.onClick.RemoveAllListeners();
                shopPanels[i].unequipButton.onClick.RemoveAllListeners();

                shopPanels[i].equipButton.onClick.AddListener(() => EquipItem(index));
                shopPanels[i].unequipButton.onClick.AddListener(() => UnequipItem(index));

                shopPanels[i].equipButton.gameObject.SetActive(true);
                shopPanels[i].unequipButton.gameObject.SetActive(false);

                if (IsItemEquipped(index))
                {
                    shopPanels[i].itemObject.SetActive(true);
                    shopPanels[i].equipButton.gameObject.SetActive(false);
                    shopPanels[i].unequipButton.gameObject.SetActive(true);
                }
                else
                {
                    shopPanels[i].itemObject.SetActive(false);
                    shopPanels[i].equipButton.gameObject.SetActive(true);
                    shopPanels[i].unequipButton.gameObject.SetActive(false);
                }
            }
            else
            {
                shopPanels[i].equipButton.gameObject.SetActive(false);
                shopPanels[i].unequipButton.gameObject.SetActive(false);
            }
        }
    }



    public void CheckPurchasable()
    {
        if (selectedItemIndex != -1 && coinScript.coinAmount >= shopItemSO[selectedItemIndex].baseCost)
            purchaseBtn.interactable = true;
        else
            purchaseBtn.interactable = false;
    }

    public void ShowPrice(int itemIndex)
    {
        selectedItemIndex = itemIndex;
        if (selectedItemIndex != -1)
        {
            priceText.text = "Price: " + shopItemSO[itemIndex].baseCost.ToString();

            if (IsItemPurchased(itemIndex))
            {
                purchaseBtn.gameObject.SetActive(false);

                if (IsItemEquipped(itemIndex))
                {
                    equipBtn.gameObject.SetActive(false);
                    unequipBtn.gameObject.SetActive(true);
                }
                else
                {
                    equipBtn.gameObject.SetActive(true);
                    unequipBtn.gameObject.SetActive(false);
                }
            }
            else
            {
                purchaseBtn.gameObject.SetActive(true);
                equipBtn.gameObject.SetActive(false);
                unequipBtn.gameObject.SetActive(false);
            }
        }
        else
        {
            purchaseBtn.gameObject.SetActive(false);
            equipBtn.gameObject.SetActive(false);
            unequipBtn.gameObject.SetActive(false);
        }
        CheckPurchasable();
    }





    public void PurchaseItem()
    {
        if (selectedItemIndex != -1 && coinScript.coinAmount >= shopItemSO[selectedItemIndex].baseCost)
        {
            coinScript.coinAmount -= shopItemSO[selectedItemIndex].baseCost;
            coinUI.text = "Coins: " + coinScript.coinAmount.ToString();
            PlayerPrefs.SetInt("CoinAmount", coinScript.coinAmount);

            MarkItemAsPurchased(selectedItemIndex);

            shopPanels[selectedItemIndex].equipButton.gameObject.SetActive(true);
            shopPanels[selectedItemIndex].unequipButton.gameObject.SetActive(false);
            purchaseBtn.gameObject.SetActive(false);

            if (IsItemEquipped(selectedItemIndex))
            {
                ShowUnequipButton(selectedItemIndex);
            }

            CheckPurchasable();
        }
    }


    //public void LoadEquippedItems()
    //{
    //    for (int i = 0; i < shopItemSO.Length; i++)
    //    {
    //        if (IsItemEquipped(i))
    //        {
    //            shopPanels[i].itemObject.SetActive(true);
    //            shopPanels[i].equipButton.gameObject.SetActive(false);
    //            shopPanels[i].unequipButton.gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            shopPanels[i].itemObject.SetActive(false);
    //            shopPanels[i].equipButton.gameObject.SetActive(true);
    //            shopPanels[i].unequipButton.gameObject.SetActive(false);
    //        }
    //    }
    //}


    public void EquipItem(int itemIndex)
    {
        // Unequip all items first
        UnequipAllItems();

        // Equip the selected item
        shopPanels[itemIndex].itemObject.SetActive(true);
        shopPanels[itemIndex].equipButton.gameObject.SetActive(false);
        shopPanels[itemIndex].unequipButton.gameObject.SetActive(true);
        MarkItemAsEquipped(itemIndex);
    }

    public void UnequipItem(int itemIndex)
    {
        shopPanels[itemIndex].itemObject.SetActive(false);
        shopPanels[itemIndex].equipButton.gameObject.SetActive(true);
        shopPanels[itemIndex].unequipButton.gameObject.SetActive(false);
        MarkItemAsUnequipped(itemIndex);
    }

    private void UnequipAllItems()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            if (IsItemEquipped(i))
            {
                shopPanels[i].itemObject.SetActive(false);
                shopPanels[i].equipButton.gameObject.SetActive(true);
                shopPanels[i].unequipButton.gameObject.SetActive(false);
                MarkItemAsUnequipped(i);
            }
        }
    }

    public void LoadEquippedItems()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            if (IsItemEquipped(i))
            {
                shopPanels[i].itemObject.SetActive(true);
                shopPanels[i].equipButton.gameObject.SetActive(false);
                shopPanels[i].unequipButton.gameObject.SetActive(true);
            }
            else
            {
                shopPanels[i].itemObject.SetActive(false);
                shopPanels[i].equipButton.gameObject.SetActive(true);
                shopPanels[i].unequipButton.gameObject.SetActive(false);
            }
        }
    }

    private bool IsItemEquipped(int itemIndex)
    {
        return PlayerPrefs.GetInt("ItemEquipped_" + itemIndex, 0) == 1;
    }


    private void ShowUnequipButton(int itemIndex)
    {
        shopPanels[itemIndex].equipButton.gameObject.SetActive(false);
        shopPanels[itemIndex].unequipButton.gameObject.SetActive(true);
    }

    private bool IsItemPurchased(int itemIndex)
    {
        return PlayerPrefs.GetInt("Item_" + itemIndex, 0) == 1;
    }

    private void MarkItemAsPurchased(int itemIndex)
    {
        PlayerPrefs.SetInt("Item_" + itemIndex, 1);
    }

    private void MarkItemAsEquipped(int itemIndex)
    {
        PlayerPrefs.SetInt("ItemEquipped_" + itemIndex, 1);
    }

    private void MarkItemAsUnequipped(int itemIndex)
    {
        PlayerPrefs.SetInt("ItemEquipped_" + itemIndex, 0);
    }

}
