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

        // Ba�lang��ta silahlar� devre d��� b�rak
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
            currentWeaponIndex = 0; // �lk silah� se�
            ActivateInventory();
        }
        else
        {
            currentWeaponIndex = -1; // Silah� devre d��� b�rak
            DeactivateInventory();
        }
    }
    private void ActivateInventory()
    {
        // �nceki silah� devre d��� b�rak
        if (currentWeaponIndex >= 0 && currentWeaponIndex < weapons.Count)
        {
            weapons[currentWeaponIndex].gameObject.SetActive(false);
        }

        // Yeni silah� etkinle�tir
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