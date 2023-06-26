using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/MoveSpeed")] //scrpiti prefab gibi göstermek için.

public class MoveSpeedAbility : Ability
{
    [SerializeField] float boostAmt = 20f;
    [SerializeField] float boostDuration = 2f;


    Player Player ; //Player scripten deðiþken çekmek için yazýldý

    public override void ActivateAbility()
    {
        if (!CommitAbility()) return;
        Player = AbilityComp.GetComponent<Player>();
        Player.AddMoveSpeed(boostAmt);
        AbilityComp.StartCoroutine(RestSpeed());
    }

    IEnumerator RestSpeed()
    {
        yield return new WaitForSeconds(boostDuration);
        Player.AddMoveSpeed(-boostAmt);
    }
}