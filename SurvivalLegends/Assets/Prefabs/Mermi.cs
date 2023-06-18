using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi : MonoBehaviour
{
    private Transform hedef;
    private float hiz;
    [SerializeField] float hasar = 5f;

    public void HedefBelirle(Transform hedef)
    {
        this.hedef = hedef;
    }

    public void HizAyarla(float hiz)
    {
        this.hiz = hiz;
    }

    public void HasarAyarla(float hasar)
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
            EnemyAI enemyAI = collider.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.HasarAl((int)hasar);
            }
        }

        Destroy(gameObject);
    }
}