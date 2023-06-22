using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    public Button abilityButton;
    public Player player;
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
            player.AddMoveSpeed(boostAmount);
            isAbilityReady = false;
            Invoke("ResetAbility", abilityDuration);
            Invoke("ResetCooldown", cooldownDuration);
        }
    }

    private void ResetAbility()
    {
        player.AddMoveSpeed(-boostAmount);
    }

    private void ResetCooldown()
    {
        isAbilityReady = true;
    }
}