using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaInventoryComponent : MonoBehaviour, IPurchaseListener
{
    [SerializeField] Weapon[] initWeaponsPrefabs;
    [SerializeField] Transform[] defaultWeaponSlots;
    [SerializeField] Transform[] weaponSlots;
    [SerializeField] private Animator animator;

    List<Weapon> weapons;
    int currentWeaponIndex = -1;
    bool isAnimationPlaying = false;
    public bool isInventoryActive = false;
    public bool canUseAbility = true;

    private void Start()
    {
        InitializeWeapons();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isAnimationPlaying)
            {
                ToggleInventory();
            }
        }
    }

    private void InitializeWeapons()
    {
        weapons = new List<Weapon>();
        int slotIndex = 0; // Kullanýlacak baþlangýç silahý slotunun dizideki indeksi

        foreach (Weapon weapon in initWeaponsPrefabs)
        {
            GiveNewWeapon(weapon, defaultWeaponSlots[slotIndex]);

            slotIndex++; // Slot indeksini bir sonraki slot için artýrýn

            if (slotIndex >= defaultWeaponSlots.Length)
            {
                slotIndex = 0; // Eðer dizideki slotlarýn sonuna gelindi ise, baþa dönün
            }
        }

        // Baþlangýçta silahlarý devre dýþý býrak
        foreach (Weapon weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }
    }

    private void GiveNewWeapon(Weapon weapon, Transform weaponSlot)
    {
        GameObject weaponObject = Instantiate(weapon.gameObject, weaponSlot);
        Weapon newWeapon = weaponObject.GetComponent<Weapon>();

        // Gerekli inizializasyonlarý yap
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

        Transform nextWeaponSlot = GetNextAvailableWeaponSlot();
        if (nextWeaponSlot != null)
        {
            GiveNewWeapon(itemAsWeapon, nextWeaponSlot);
            return true;
        }

        return false;
    }

    public void ActivateAbility()
    {
        if (!isInventoryActive && canUseAbility)
        {
            ToggleInventory();
            StartCoroutine(DisableAbilityAfterDelay(1f)); // Yetenek kullanýmýndan sonra 1 saniye beklet
        }
    }

    public void ToggleInventory()
    {
        isInventoryActive = !isInventoryActive;

        if (isInventoryActive)
        {
            currentWeaponIndex = 0; // Ýlk silahý seç
            ActivateInventory(); // Silahý etkinleþtir
            animator.SetBool("Spine", true); // "spine" animasyon parametresini true olarak ayarla
            isAnimationPlaying = true;
            StartCoroutine(StopAnimationAfterDelay(1.5f));
        }
        else
        {
            DeactivateInventory();
        }
    }

    private IEnumerator StopAnimationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (isInventoryActive)
        {
            animator.SetBool("Spine", false); // "spine" animasyon parametresini false olarak ayarla
            isAnimationPlaying = false;
            DeactivateInventory(); // Silahý devre dýþý býrak
        }
    }

    private IEnumerator DisableAbilityAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        canUseAbility = false;
        yield return new WaitForSeconds(delay);
        canUseAbility = true;
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

        // Ýkinci silahý etkinleþtir
        int secondWeaponIndex = currentWeaponIndex + 1;
        if (secondWeaponIndex >= 0 && secondWeaponIndex < weapons.Count)
        {
            weapons[secondWeaponIndex].gameObject.SetActive(true);
        }
    }

    private void DeactivateInventory()
    {
        if (currentWeaponIndex >= 0 && currentWeaponIndex < weapons.Count)
        {
            weapons[currentWeaponIndex].gameObject.SetActive(false);
        }

        // Ýkinci silahý devre dýþý býrak
        int secondWeaponIndex = currentWeaponIndex + 1;
        if (secondWeaponIndex >= 0 && secondWeaponIndex < weapons.Count)
        {
            weapons[secondWeaponIndex].gameObject.SetActive(false);
        }

        animator.SetBool("Spine", false); // "spine" animasyon parametresini false olarak ayarla
        isInventoryActive = false;
    }

    private Transform GetNextAvailableWeaponSlot()
    {
        int nextSlotIndex = currentWeaponIndex + 1;
        if (nextSlotIndex >= 0 && nextSlotIndex < defaultWeaponSlots.Length)
        {
            return defaultWeaponSlots[nextSlotIndex];
        }

        return null;
    }
}