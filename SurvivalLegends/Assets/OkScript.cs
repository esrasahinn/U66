using UnityEngine;

public class OkScript : MonoBehaviour
{
    public int damageAmount = 10; // Hasar miktarý

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dusman"))
        {
            EnemyAI dusmanHealth = collision.gameObject.GetComponent<EnemyAI>();
            if (dusmanHealth != null)
            {
                dusmanHealth.HasarAl(damageAmount); // Hasarý ver
            }

            Destroy(gameObject); // Oku yok et
        }
    }
}