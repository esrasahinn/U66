using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/SpeedBoost")] //scrpiti prefab gibi göstermek için.

public class SpeedBoostAbility : Ability
{
    [SerializeField] float boostAmt = 20f;
    [SerializeField] float boostDuration = 2f;


    MenzileGirenDusmanaAtesVeDonme MenzileGirenDusmanaAtesVeDonme; //Player scripten deðiþken çekmek için yazýldý

    public override void ActivateAbility()
    {
        if (!CommitAbility()) return;
        MenzileGirenDusmanaAtesVeDonme = AbilityComp.GetComponent<MenzileGirenDusmanaAtesVeDonme>();
        MenzileGirenDusmanaAtesVeDonme.AddAtesHizi(boostAmt);
        AbilityComp.StartCoroutine(RestSpeed());
    }

    IEnumerator RestSpeed()
    {
        yield return new WaitForSeconds(boostDuration);
        MenzileGirenDusmanaAtesVeDonme.AddAtesHizi(-boostAmt);
    }
}