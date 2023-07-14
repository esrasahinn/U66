using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZehirliSu : MonoBehaviour
{
    public GameObject silindirPrefab; // Silindir prefab nesnesi
    public float fallSpeed = 2.0f; // Düþme hýzý
    public int dusmanHasarMiktari = 10; // Düþmana verilecek hasar miktarý
    private Rigidbody rb;
    private bool hasHitEnemy = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Kinematic modunu etkinleþtir
    }

    public void Atesle(int hasarMiktari, RangedEnemyController dusman)
    {
        dusmanHasarMiktari = hasarMiktari;
        rb.isKinematic = false; // Kinematic modunu devre dýþý býrak
        rb.velocity = Vector3.down * fallSpeed;

        // Zehirli suyu düþmana at
        transform.SetParent(dusman.transform);
    }

    public void Atesle(int hasarMiktari, EnemyController dusman)
    {
        dusmanHasarMiktari = hasarMiktari;
        rb.isKinematic = false; // Kinematic modunu devre dýþý býrak
        rb.velocity = Vector3.down * fallSpeed;

        // Zehirli suyu düþmana at
        transform.SetParent(dusman.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dusman") && !hasHitEnemy)
        {
            hasHitEnemy = true;

            if (other.GetComponent<RangedEnemyController>() != null)
            {
                RangedEnemyController dusman = other.GetComponent<RangedEnemyController>();
                dusman.TakeDamage(dusmanHasarMiktari); // Düþmana hasar ver
            }
            else if (other.GetComponent<EnemyController>() != null)
            {
                EnemyController dusman = other.GetComponent<EnemyController>();
                dusman.TakeDamage(dusmanHasarMiktari); // Düþmana hasar ver
            }

            // Silindir prefabini düþmanýn yerine yerleþtir
            GameObject silindir = Instantiate(silindirPrefab, other.transform.position, other.transform.rotation);

            Destroy(gameObject); // Zehirli suyu yok et

            // Silindir prefabini belirli bir süre sonra yok et
            Destroy(silindir, 5f);
        }
    }
}

