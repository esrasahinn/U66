using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasarVerenKup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dusman"))
        {
            EnemyAI enemyAI = other.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.HasarAl(50f);
            }

            EnemyController enemyController = other.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.TakeDamage(50);
            }

            RangedEnemyController RenemyController = other.GetComponent<RangedEnemyController>();
            if (RenemyController != null)
            {
                RenemyController.TakeDamage(50);
            }
        }
    }
}
