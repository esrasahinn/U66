using UnityEngine;
using UnityEngine.UI;

public class ASAbilityArcher : MonoBehaviour
{
    public Button abilityButton;
    public ArcherMenzileGirenDusmanaAtesVeDonme archerScript;
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
            archerScript.AddAtesHizi(boostAmount);
            isAbilityReady = false;
            Invoke("ResetAbility", abilityDuration);
            Invoke("ResetCooldown", cooldownDuration);
        }
    }

    private void ResetAbility()
    {
        archerScript.AddAtesHizi(-boostAmount);
    }

    private void ResetCooldown()
    {
        isAbilityReady = true;
    }
}