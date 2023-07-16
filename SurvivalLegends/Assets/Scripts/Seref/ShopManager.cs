using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public DiamonCount diamondScript;
    public TMP_Text diamondUI;
    public ShopItemSO[] shopItemSO;
    public ShopTemplate[] shopPanels;
    public GameObject[] shopPanelsGO;
    public Button[] purchaseBtns;
    public Button[] equipBtns;
    public Button[] unequipBtns;
    public CharachterSelectShop chrctrSlct;
    public SkinCheck skinCheck;
   

    private const string DiamondAmountKey = "DiamondAmount";

    void Start()
    {

        diamondScript.diamondAmount = PlayerPrefs.GetInt("DiamondAmount", 10);
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            {
                shopPanelsGO[i].gameObject.SetActive(true);
            }
        }

        LoadDiamondCount();
        diamondUI.text = diamondScript.diamondAmount.ToString();
        LoadPanels();
        CheckPurchasable();
        CheckEquipped();
        //skinCheck.CheckEquipped();
    }

    void Update()
    {
        LoadPanels();
    }

    public void LoadPanels()
    {
        int selectedCharacter = chrctrSlct.SelectedCharacter;

        for (int i = 0; i < shopItemSO.Length; i++)
        {
            bool isActive = (shopItemSO[i].characterIndex == selectedCharacter);

            shopPanelsGO[i].SetActive(isActive);
            shopPanels[i].costTxt.text = shopItemSO[i].baseCost.ToString();
        }
    }


    public void CheckPurchasable()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            if (diamondScript.diamondAmount >= shopItemSO[i].baseCost && !shopItemSO[i].IsPurchased)
                purchaseBtns[i].interactable = true;
            else
                purchaseBtns[i].interactable = false;
        }
    }

    public void PurchaseItem(int btnNo)
    {
        ShopItemSO currentItem = shopItemSO[btnNo];

        if (diamondScript.diamondAmount >= currentItem.baseCost)
        {
            diamondScript.diamondAmount = diamondScript.diamondAmount - currentItem.baseCost;

            SaveDiamondCount();
            diamondUI.text = diamondScript.diamondAmount.ToString();
            currentItem.IsPurchased = true;

            purchaseBtns[btnNo].gameObject.SetActive(false);
            equipBtns[btnNo].gameObject.SetActive(true);

            shopPanelsGO[btnNo].SetActive(true);

            CheckPurchasable();
            CheckEquipped();
        }
    }

    public void CheckEquipped()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            bool isPurchased = shopItemSO[i].IsPurchased;
            bool isEquipped = shopItemSO[i].IsEquipped;

            if (isPurchased && isEquipped)
            {
                purchaseBtns[i].gameObject.SetActive(false);
                equipBtns[i].gameObject.SetActive(false);
                unequipBtns[i].gameObject.SetActive(true);
                shopPanelsGO[i].SetActive(true);
            }
            else if (isPurchased)
            {
                purchaseBtns[i].gameObject.SetActive(false);
                equipBtns[i].gameObject.SetActive(true);
                unequipBtns[i].gameObject.SetActive(false);
                shopPanelsGO[i].SetActive(true);
            }
            else
            {
                purchaseBtns[i].gameObject.SetActive(true);
                equipBtns[i].gameObject.SetActive(false);
                unequipBtns[i].gameObject.SetActive(false);
                shopPanelsGO[i].SetActive(true);
            }
        }
    }


    public void EquipItem(int btnNo)
    {
        ShopItemSO currentItem = shopItemSO[btnNo];

        if (!currentItem.IsEquipped)
        {
            currentItem.IsEquipped = true;

            purchaseBtns[btnNo].gameObject.SetActive(false);
            equipBtns[btnNo].gameObject.SetActive(false);
            unequipBtns[btnNo].gameObject.SetActive(true);
            shopPanelsGO[btnNo].SetActive(true);

            CheckEquipped();
        }
    }

    public void UnequipItem(int btnNo)
    {
        ShopItemSO currentItem = shopItemSO[btnNo];

        if (currentItem.IsEquipped)
        {
            currentItem.IsEquipped = false;

            purchaseBtns[btnNo].gameObject.SetActive(false);
            equipBtns[btnNo].gameObject.SetActive(true);
            unequipBtns[btnNo].gameObject.SetActive(false);

            shopPanelsGO[btnNo].SetActive(false);


            CheckEquipped();
        }
    }


    public void AdDia()
    {
        diamondScript.diamondAmount += 5;
        diamondUI.text = diamondScript.diamondAmount.ToString();
        SaveDiamondCount();
        CheckPurchasable();
    }

    private void LoadDiamondCount()
    {
        if (PlayerPrefs.HasKey(DiamondAmountKey))
        {
            diamondScript.diamondAmount = PlayerPrefs.GetInt(DiamondAmountKey);
        }
        else
        {
            diamondScript.diamondAmount = 0;
            SaveDiamondCount();
        }
    }

    public void SaveDiamondCount()
    {
        PlayerPrefs.SetInt(DiamondAmountKey, diamondScript.diamondAmount);
    }
}
