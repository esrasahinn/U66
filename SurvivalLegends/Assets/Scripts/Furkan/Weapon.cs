using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour //diðer sýnýflardan eriþmek için abstract eklendi.
{
    [SerializeField] string AttachSlotTag;
   // [SerializeField] float AttackRateMult = 1f;

    public abstract void Attack();

    public string GetAttachSlotTag()
    {
        return AttachSlotTag;
    }

    public GameObject Owner
    {
        get;
        private set;
    }
    public void Init(GameObject owner)
    {
        Owner = owner;
    }

    public void Equip()
    {
        gameObject.SetActive(true);

    }

    public void UnEquip()
    {
        gameObject.SetActive(false);
    }

    public void DamageGameObject(GameObject objToDamage,float amt)
    {
        HealthComponent healthComp = objToDamage.GetComponent<HealthComponent>();
        if (healthComp != null)
        {
            healthComp.changeHealth(-amt);
        }
    }
}
