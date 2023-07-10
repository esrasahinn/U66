using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi : MonoBehaviour
{
    private Transform hedef;
    private float hiz;
    [SerializeField] public int hasar = 20;
    [SerializeField] public int minHasar = 5;
    [SerializeField] public int maxHasar = 50;

    internal void AddHasar(int hasarAmt)
    {
        hasar += hasarAmt;
        hasar = Mathf.Clamp(hasar, minHasar, maxHasar);
    }


    public void HedefBelirle(Transform hedef)
    {
        this.hedef = hedef;
    }

    public void HizAyarla(float hiz)
    {
        this.hiz = hiz;
    }

    public void HasarAyarla(int hasar)
    {
        this.hasar = hasar;
    }

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
        Collider[] hedefColliders = Physics.OverlapSphere(transform.position, 1f);

        foreach (Collider collider in hedefColliders)
        {
            EnemyController enemyAI = collider.GetComponent<EnemyController>();
            if (enemyAI != null)
            {
                enemyAI.TakeDamage(hasar);
            }

            RangedEnemyController RenemyAI = collider.GetComponent<RangedEnemyController>();
            if (RenemyAI != null)
            {
                RenemyAI.TakeDamage(hasar);
            }
        }

        Destroy(gameObject);
    }
}
