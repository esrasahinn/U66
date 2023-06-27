using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NinjaInventoryComponent : MonoBehaviour, IPurchaseListener
{
    [SerializeField] Weapon[] initWeaponsPrefabs;

    [SerializeField] Transform defaultWeaponSlot;
    [SerializeField] Transform[] weaponSlots;
    private bool isInventoryActive = false;
    [SerializeField] private Animator animator;

    List<Weapon> weapons;
    int currentWeaponIndex = -1;

    private void Start()
    {
        InitializeWeapons();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
        }
    }

    private void InitializeWeapons()
    {
        weapons = new List<Weapon>();
        foreach (Weapon weapon in initWeaponsPrefabs)
        {
            GiveNewWeapon(weapon);
        }

        // Baþlangýçta silahlarý devre dýþý býrak
        foreach (Weapon weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }
    }

    private void GiveNewWeapon(Weapon weapon)
    {
        Transform weaponSlot = defaultWeaponSlot;
        foreach (Transform slot in weaponSlots)
        {
            if (slot.gameObject.tag == weapon.GetAttachSlotTag())
            {
                weaponSlot = slot;
            }
        }
        Weapon newWeapon = Instantiate(weapon, weaponSlot);
        newWeapon.Init(gameObject);
        weapons.Add(newWeapon);
    }

    internal Weapon GetActiveWeapon()
    {
        return weapons[currentWeaponIndex];
    }




    public bool HandlePurchase(Object newPurchase)
    {
        GameObject itemAsGameObject = newPurchase as GameObject;
        if (itemAsGameObject != null) return false;

        Weapon itemAsWeapon = itemAsGameObject.GetComponent<Weapon>();
        if (itemAsWeapon == null) return false;

        GiveNewWeapon(itemAsWeapon);
        return true;
    }
    private void ToggleInventory()
    {
        isInventoryActive = !isInventoryActive;

        if (isInventoryActive)
        {
            currentWeaponIndex = 0; // Ýlk silahý seç
            ActivateInventory();
        }
        else
        {
            currentWeaponIndex = -1; // Silahý devre dýþý býrak
            DeactivateInventory();
        }
    }
    private void ActivateInventory()
    {
        // Önceki silahý devre dýþý býrak
        if (currentWeaponIndex >= 0 && currentWeaponIndex < weapons.Count)
        {
            weapons[currentWeaponIndex].gameObject.SetActive(false);
        }

        // Yeni silahý etkinleþtir
        if (currentWeaponIndex >= 0 && currentWeaponIndex < weapons.Count)
        {
            weapons[currentWeaponIndex].gameObject.SetActive(true);
            animator.SetBool("Spine", true); // "spine" animasyon parametresini true olarak ayarla
        }
    }
    private void DeactivateInventory()
    {
        if (currentWeaponIndex >= 0 && currentWeaponIndex < weapons.Count)
        {
            weapons[currentWeaponIndex].gameObject.SetActive(false);
        }

        animator.SetBool("Spine", false); // "spine" animasyon parametresini false olarak ayarla
    }
}