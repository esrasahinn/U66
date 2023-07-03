using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZehirliSu : MonoBehaviour
{
    public float fallSpeed = 2.0f; // Düþme hýzý
    public int dusmanHasarMiktari = 10; // Düþmana verilecek hasar miktarý
    private Rigidbody rb;
    private bool hasHitEnemy = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Kinematic modunu etkinleþtir
    }

    private void Start()
    {
        rb.velocity = Vector3.down * fallSpeed; // Aþaðý yönde düþme hýzýný ayarla
    }

    public void At(EnemyAI dusman)
    {
        // Zehirli suyu düþmana fýrlat
        transform.SetParent(dusman.transform);
        rb.isKinematic = false; // Kinematic modunu devre dýþý býrak
        rb.velocity = Vector3.down * fallSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dusman") && !hasHitEnemy)
        {
            EnemyAI dusman = collision.gameObject.GetComponent<EnemyAI>();
            dusman.HasarAl(dusmanHasarMiktari); // Düþmana hasar ver
            hasHitEnemy = true;
            Destroy(gameObject); // Zehirli suyu yok et
        }
    }
}