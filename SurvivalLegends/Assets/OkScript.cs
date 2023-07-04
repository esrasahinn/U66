using UnityEngine;

public class OkScript : MonoBehaviour
{
    public int damageAmount = 10; // Hasar miktar�

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dusman"))
        {
            // E�er �arp��an obje "Dusman" tagine sahipse, ona hasar ver
            EnemyAI enemyAI = other.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.HasarAl(damageAmount);
            }

            // Oku yok et
            Destroy(gameObject);
        }
    }
}