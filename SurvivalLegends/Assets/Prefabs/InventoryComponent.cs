using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] Weapon[] initWeaponsPrefabs;

    [SerializeField] Transform defaultWeaponSlot;
    [SerializeField] Transform[] weaponSlots;

    private void Start()
    {
        InitializeWeapons();
    }

    private void InitializeWeapons()
    {
        foreach (Weapon weapon in initWeaponsPrefabs)
        {
            Transform weaponSlot = defaultWeaponSlot;
            foreach(Transform slot in weaponSlots)
            {
                if(slot.gameObject.tag == weapon.GetAttachSlotTag())
                {
                    weaponSlot = slot;
                }
            }
            Weapon newWeapon = Instantiate(weapon, weaponSlot);
        }
    }
}
