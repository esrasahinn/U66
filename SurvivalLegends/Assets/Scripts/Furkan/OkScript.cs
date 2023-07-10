using UnityEngine;

public class OkScript : MonoBehaviour
{
    public int damageAmount = 10; // Hasar miktarý
    public int DamageAmount { get { return damageAmount; } set { damageAmount = value; } }

    private bool hitEnemy = false; // Düþmana çarptý mý kontrolü için bool deðiþken

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dusman"))
        {
            Debug.Log("Ok, düþmana çarptý!"); // Kontrol için bir log mesajý ekle
            EnemyController dusmanHealth = other.gameObject.GetComponent<EnemyController>();
            if (dusmanHealth != null)
            {
                dusmanHealth.TakeDamage(damageAmount); // Hasarý ver
            }

            RangedEnemyController RdusmanHealth = other.gameObject.GetComponent<RangedEnemyController>();
            if (RdusmanHealth != null)
            {
                RdusmanHealth.TakeDamage(damageAmount); // Hasarý ver
            }

            hitEnemy = true; // Düþmana çarptý olarak iþaretle
            Destroy(gameObject); // Oku hemen yok et
        }
    }

    private void Update()
    {
        if (!hitEnemy)
        {
            // Düþmana çarpmadýysa zamanlayýcýyý baþlat
            StartCoroutine(DestroyAfterDelay(2f));
        }
    }

    private System.Collections.IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
