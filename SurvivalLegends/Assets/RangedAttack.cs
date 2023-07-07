using System.Collections;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public int damageAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ArcherPlayerBehaviour arcplayerController = other.GetComponent<ArcherPlayerBehaviour>();
            if (arcplayerController != null)
            {
                arcplayerController.PlayerTakeDmg(damageAmount);
            }
        }

        Destroy(gameObject);
    }
}