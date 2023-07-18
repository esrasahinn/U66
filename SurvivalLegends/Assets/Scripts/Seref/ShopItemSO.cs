using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scriptable Objects/New Shop Item", order = 1)]
public class ShopItemSO : ScriptableObject
{
    public int baseCost;
    public GameObject baseModel;
    public GameObject skinModel;
    public string itemName;
    public bool IsPurchased;
    public bool IsEquipped;
    public int characterIndex;


}
