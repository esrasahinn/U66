using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public delegate void OnHealthChange(float health, float delta, float maxHealth);
    public delegate void OnTakeDamage(float health, float delta, float maxHealt);
    public delegate void OnHealthEmpty();

    [SerializeField] float health = 100;
    [SerializeField] float maxhealth = 100;

    public event OnHealthChange onHealthChange;
    public event OnTakeDamage onTakeDamage;
    public event OnHealthEmpty onHealthEmpty;

    public void changeHealth (float amt)
    {
        if(amt == 0)
        {
            return;
        }

        health += amt;
        if (amt < 0)
        {
            onTakeDamage?.Invoke(health, amt, maxhealth);
        }

        onHealthChange?.Invoke(health, amt, maxhealth);

        if(health < 0)
        {
            health = 0;
            onHealthEmpty?.Invoke();
        }

        Debug.Log($"{gameObject.name}, taking damage: {amt}, healt is now: {health}");
    }
   
}
