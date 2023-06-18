using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi : MonoBehaviour
{
    private Transform hedef;
    private float hiz;
    //private float damage;

    public void HedefBelirle(Transform hedef)
    {
        this.hedef = hedef;
    }

    public void HizAyarla(float hiz)
    {
        this.hiz = hiz;
    }

  // public void SetDamage(float damage)
  // {
  //     this.damage = damage;
  // }

    private void Update()
    {
        if (hedef == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 hedefYonu = hedef.position - transform.position;
        float hareketMesafesi = hiz * Time.deltaTime;

        if (hedefYonu.magnitude <= hareketMesafesi)
        {
            HedefiVur();
            return;
        }

        transform.Translate(hedefYonu.normalized * hareketMesafesi, Space.World);
    }

    private void HedefiVur()
    {
        // Burada hedefe hasar uygulama veya baþka bir iþlem yapabilirsiniz
        Destroy(gameObject);
    }

    //public void DamageGameObject(GameObject objToDamage, float amt)
    //{
    //    HealthComponent healthComp = objToDamage.GetComponent<HealthComponent>();
    //    if (healthComp != null)
    //    {
    //        healthComp.changeHealth(-amt);
    //    }
    //}
}