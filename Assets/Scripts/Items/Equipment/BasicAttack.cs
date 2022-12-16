using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : Ability
{

    public override void Activate(GameObject parent)
    {
        Attack attack = parent.GetComponent<Attack>();
        attack.CalculateHit();
        attack.DealDamage(target);
    }
}