// using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Rifle : Weapon
{
    [SerializeField] AimComponent aimComp;
    [SerializeField] float Damage = 5f;

    public override void Attack()
    {
        GameObject target = aimComp.GetAimTarget();
        DamageGameObject(target, Damage);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
