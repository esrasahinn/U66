using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinCheck : MonoBehaviour
{
    public ShopManager shopMngr;
    public ShopItemSO[] shopItemSO;
    public MeshActivation meshActv;
    void Start()
    {
        CheckEquipped();
    }

    void CheckEquipped()
    {
        if (shopItemSO[0].IsEquipped) { meshActv.ActivateMesh();}
    }
        
        void Update()
    {
        
    }
}
