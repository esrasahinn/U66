using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    void Start()
    {
        for (int i = 0; i<shopItemSO.Length; i++)
        {
            shopPanelsGO[i].gameObject.SetActive(true);
        }
        diamondUI.text = diamondScript.diamondAmount.ToString();
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
            if (diamondScript.diamondAmount >= shopItemSO[i].baseCost)
                purchaseBtns[i].interactable= true;
            else
                purchaseBtns[i].interactable= false;
        }
    }

    public void PurchaseItem(int btnNo)
    {
        ShopItemSO currentItem = shopItemSO[btnNo];

        if (diamondScript.diamondAmount >= currentItem.baseCost && !currentItem.isPurchased)
        {
            int spentDiamond = diamondScript.diamondAmount - currentItem.baseCost;
            diamondScript.diamondAmount = spentDiamond; // Update the diamond count

            diamondUI.text = spentDiamond.ToString(); // Update the UI text

            PlayerPrefs.SetInt("DiamondAmount", spentDiamond);

            // Update item status
            currentItem.isPurchased = true;
            currentItem.isEquipped = true;

            // Update button text and functionality
            purchaseBtns[btnNo].GetComponentInChildren<TMP_Text>().text = "Unequip";
            purchaseBtns[btnNo].onClick.RemoveAllListeners();
            purchaseBtns[btnNo].onClick.AddListener(() => UnequipItem(btnNo));

            // Equip the purchased item
            EquipItem(btnNo);
        }
    }

    private void EquipItem(int btnNo)
    {
        ShopItemSO currentItem = shopItemSO[btnNo];

        // Activate the mesh or perform the necessary actions
        // to visually indicate that the item is equipped

        // Disable the meshes of other equipped items
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            if (i != btnNo && shopItemSO[i].isEquipped)
            {
                UnequipItem(i);
            }
        }

        // Update button text and functionality for other items
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            if (i != btnNo)
            {
                Button button = purchaseBtns[i];
                button.GetComponentInChildren<TMP_Text>().text = "Equip";
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => EquipItem(i));
            }
        }
    }

    private void UnequipItem(int btnNo)
    {
        ShopItemSO currentItem = shopItemSO[btnNo];

        // Deactivate the mesh or perform the necessary actions
        // to visually indicate that the item is unequipped

        currentItem.isEquipped = false;

        // Update button text and functionality
        purchaseBtns[btnNo].GetComponentInChildren<TMP_Text>().text = "Equip";
        purchaseBtns[btnNo].onClick.RemoveAllListeners();
        purchaseBtns[btnNo].onClick.AddListener(() => EquipItem(btnNo));
    }


}