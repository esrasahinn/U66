using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class olmekicin : MonoBehaviour
{

    UnitHealth unitHealth;

    void Start()
    {
        unitHealth = new UnitHealth(100, 100);
    }

    void Update()
    {
        // Burada sa�l�k de�erini kontrol edebilirsiniz
        if (unitHealth.Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}