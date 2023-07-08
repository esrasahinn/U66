using UnityEngine;

public class OkScript : MonoBehaviour
{
    public int damageAmount = 10; // Hasar miktar�
    public int DamageAmount { get { return damageAmount; } set { damageAmount = value; } }

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

            Destroy(gameObject); // Oku hemen yok et
        }
        else
        {
            Destroy(gameObject, 3f); // Oku 3 saniye sonra yok et
        }
    }
}