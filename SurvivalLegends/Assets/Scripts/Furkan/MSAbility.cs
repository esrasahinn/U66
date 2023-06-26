using UnityEngine;
using UnityEngine.UI;

public class MSAbility : MonoBehaviour
{
    public Button abilityButton;
    public Player player;
    public Mermi mermi;
    public int hasarAmount = 10;
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
            mermi.AddHasar(hasarAmount);
            isAbilityReady = false;
            Invoke("ResetAbility", abilityDuration);
            Invoke("ResetCooldown", cooldownDuration);
        }
    }

    private void ResetAbility()
    {
        mermi.AddHasar(-hasarAmount);
    }

    private void ResetCooldown()
    {
        isAbilityReady = true;
    }
}