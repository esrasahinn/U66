using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegen : Ability
{
    public override void ActivateAbility()
    {
        if(!CommitAbility()) return;
    }

}
