using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public GameObject silindirPrefab; // Silindir prefab nesnesi
    public float fallSpeed = 2.0f; // D��me h�z�
    public int dusmanHasarMiktari = 10; // D��mana verilecek hasar miktar�
    private Rigidbody rb;
    private bool hasHitEnemy = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Kinematic modunu etkinle�tir
    }

    public void Atesle(int hasarMiktari, EnemyAI dusman)
    {
        dusmanHasarMiktari = hasarMiktari;
        rb.isKinematic = false; // Kinematic modunu devre d��� b�rak
        rb.velocity = Vector3.down * fallSpeed;

        // Meteoru d�smana at
        transform.SetParent(dusman.transform);
    }

    public void Atesle(int hasarMiktari, EnemyController dusman)
    {
        dusmanHasarMiktari = hasarMiktari;
        rb.isKinematic = false; // Kinematic modunu devre d��� b�rak
        rb.velocity = Vector3.down * fallSpeed;

        // Meteoru d��mana at
        transform.SetParent(dusman.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dusman") && !hasHitEnemy)
        {
            hasHitEnemy = true;

            if (other.GetComponent<EnemyAI>() != null)
            {
                EnemyAI dusman = other.GetComponent<EnemyAI>();
                dusman.HasarAl(dusmanHasarMiktari); // D��mana hasar ver
            }
            else if (other.GetComponent<EnemyController>() != null)
            {
                EnemyController dusman = other.GetComponent<EnemyController>();
                dusman.TakeDamage(dusmanHasarMiktari); // D��mana hasar ver
            }

            // Silindir prefabini d��man�n yerine yerle�tir
            Instantiate(silindirPrefab, other.transform.position, other.transform.rotation);

            Destroy(gameObject); // Meteoru yok et
        }
    }
}

