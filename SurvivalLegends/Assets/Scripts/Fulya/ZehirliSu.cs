using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZehirliSu : MonoBehaviour
{
    public float fallSpeed = 2.0f; // Düþme hýzý
    private Rigidbody rb;
    private int dusmanHasarMiktari;
    private bool hasHitEnemy = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Kinematic modunu etkinleþtir
    }

    public void Atesle(int hasarMiktari, EnemyAI dusman)
    {
        dusmanHasarMiktari = hasarMiktari;
        rb.isKinematic = false; // Kinematic modunu devre dýþý býrak
        rb.velocity = Vector3.down * fallSpeed;

        // Zehirli suyu düþmana at
        dusman.HasarAl(dusmanHasarMiktari);
    }

    public void Atesle(int hasarMiktari, EnemyController dusman)
    {
        dusmanHasarMiktari = hasarMiktari;
        rb.isKinematic = false; // Kinematic modunu devre dýþý býrak
        rb.velocity = Vector3.down * fallSpeed;

        // Zehirli suyu düþmana at
        dusman.TakeDamage(dusmanHasarMiktari);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dusman") && !hasHitEnemy)
        {
            hasHitEnemy = true;
            Destroy(gameObject); // Zehirli suyu yok et
        }
    }
}
