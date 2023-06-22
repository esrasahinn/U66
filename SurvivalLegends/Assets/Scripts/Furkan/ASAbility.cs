using UnityEngine;
using UnityEngine.UI;

public class ASAbility : MonoBehaviour
{
    public Button abilityButton;
    public MenzileGirenDusmanaAtesVeDonme MenzileGirenDusmanaAtesVeDonme;
    public float boostAmount = 10f;
    public float cooldownDuration = 2f;
    public float abilityDuration = 2f;

    private bool isAbilityReady = true;

    void Start()
    {
        abilityButton.onClick.AddListener(UseAbility);
    }

    void UseAbility()
    {
        if (isAbilityReady)
        {
            MenzileGirenDusmanaAtesVeDonme.AddAtesHizi(boostAmount);
            isAbilityReady = false;
            Invoke("ResetAbility", abilityDuration);
            Invoke("ResetCooldown", cooldownDuration);
        }
    }

    private void ResetAbility()
    {
        MenzileGirenDusmanaAtesVeDonme.AddAtesHizi(-boostAmount);
    }

    private void ResetCooldown()
    {
        isAbilityReady = true;
    }
}