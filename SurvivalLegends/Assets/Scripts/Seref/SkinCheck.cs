using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinCheck : MonoBehaviour
{
    public ShopManager shopMngr;
    public ShopItemSO[] shopItemSO;
    public MeshActivation archerSkin;
    public MeshNinja ninjaSkin;
    public MeshMarksman marksmanSkin;
    void Start()
    {
        CheckEquipped();
    }

    public void CheckEquipped()
    {
        if (shopItemSO[0].IsEquipped) { archerSkin.ActivateMesh(); }
        if (shopItemSO[1].IsEquipped) { ninjaSkin.ActivateMesh(); }
        if (shopItemSO[2].IsEquipped) { marksmanSkin.ActivateMesh(); }
    }
}
