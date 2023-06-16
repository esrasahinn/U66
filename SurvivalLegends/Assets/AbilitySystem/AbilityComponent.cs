using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityComponent : MonoBehaviour
{
    [SerializeField] Ability[] InitialAbilites;

    public delegate void OnNewAbilityAdded(Ability newAbility);
    public delegate void OnStaminaChange(float newAmount, float maxAmount);

    private List<Ability> abilities = new List<Ability>();

    public event OnNewAbilityAdded onNewAbilityAdded;
    public event OnStaminaChange onStaminaChange;

    [SerializeField] float stamina = 200f;
    [SerializeField] float maxStamina = 200f;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Ability ability in InitialAbilites)
        {
            GiveAbility(ability);
        }

    }
    void GiveAbility(Ability ability)
    {
        Ability newAbility = Instantiate(ability);
        newAbility.InitAbility(this);
        abilities.Add(newAbility);
        onNewAbilityAdded?.Invoke(newAbility);

    }


    public void ActiveAbility(Ability abilityToActive)
    {
        if(abilities.Contains(abilityToActive))
        {
            abilityToActive.ActivateAbility();
        }
    }

    float GetStamina()
    {
        return stamina;
    }

    public bool TryConsumeStamina(float staminaToConsume)
    {
        if (stamina <= staminaToConsume) return false;

        stamina -= staminaToConsume;
        onStaminaChange?.Invoke(stamina, maxStamina);
        return true;
    }
}
