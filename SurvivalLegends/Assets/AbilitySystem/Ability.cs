using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [SerializeField] Sprite AbilityIcon;
    [SerializeField] float staminaCost = 10;
    [SerializeField] float cooldownDuration = 2f;

    public AbilityComponent AbilityComp
    {
        get { return abilityComponent; }
        private set { abilityComponent = value; }
    }

    AbilityComponent abilityComponent;

    bool abilityOnCooldown = false;

    public delegate void OnCooldownStarted();
    public OnCooldownStarted onCooldownStarted;

    internal Sprite GetAbilityIcon()
    {
        return AbilityIcon;
    }


    internal void InitAbility(AbilityComponent abilityComponent)
    {
        this.abilityComponent = abilityComponent;
    }

    public abstract void ActivateAbility();

    protected bool CommitAbility()
    {
        if (abilityOnCooldown) return false;

        if (abilityComponent == null || !abilityComponent.TryConsumeStamina(staminaCost))
            return false;

        return true;
    }

    void StartAbilityCooldown()
    {
        abilityComponent.StartCoroutine(CooldownCoroutine());
    }

    IEnumerator CooldownCoroutine()
    {
        abilityOnCooldown = true;
        onCooldownStarted.Invoke();
        yield return new WaitForSeconds(cooldownDuration);
        abilityOnCooldown= false;
    }

    internal float GetCooldownDuration()
    {
        return cooldownDuration;
    }
}
