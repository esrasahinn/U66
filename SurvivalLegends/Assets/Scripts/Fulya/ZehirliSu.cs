using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZehirliSu : MonoBehaviour
{
    public float fallSpeed = 2.0f; // D��me h�z�
    private Rigidbody rb;
    private int dusmanHasarMiktari;
    private bool hasHitEnemy = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Kinematic modunu etkinle�tir
    }

    public void Atesle(int hasarMiktari)
    {
        dusmanHasarMiktari = hasarMiktari;
        rb.isKinematic = false; // Kinematic modunu devre d��� b�rak
        rb.velocity = Vector3.down * fallSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dusman") && !hasHitEnemy)
        {
            EnemyAI dusman = collision.gameObject.GetComponent<EnemyAI>();
            dusman.HasarAl(dusmanHasarMiktari); // D��mana hasar ver
            hasHitEnemy = true;
            Destroy(gameObject); // Zehirli suyu yok et
        }
    }
}
