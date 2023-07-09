using UnityEngine;

public class OkScript : MonoBehaviour
{
    public int damageAmount = 10; // Hasar miktar�
    public int DamageAmount { get { return damageAmount; } set { damageAmount = value; } }

    private bool hitEnemy = false; // D��mana �arpt� m� kontrol� i�in bool de�i�ken

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dusman"))
        {
            Debug.Log("Ok, d��mana �arpt�!"); // Kontrol i�in bir log mesaj� ekle
            EnemyController dusmanHealth = other.gameObject.GetComponent<EnemyController>();
            if (dusmanHealth != null)
            {
                dusmanHealth.TakeDamage(damageAmount); // Hasar� ver
            }

            RangedEnemyController RdusmanHealth = other.gameObject.GetComponent<RangedEnemyController>();
            if (RdusmanHealth != null)
            {
                RdusmanHealth.TakeDamage(damageAmount); // Hasar� ver
            }

            hitEnemy = true; // D��mana �arpt� olarak i�aretle
            Destroy(gameObject); // Oku hemen yok et
        }
    }

    private void Update()
    {
        if (!hitEnemy)
        {
            // D��mana �arpmad�ysa zamanlay�c�y� ba�lat
            StartCoroutine(DestroyAfterDelay(2f));
        }
    }

    private System.Collections.IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
