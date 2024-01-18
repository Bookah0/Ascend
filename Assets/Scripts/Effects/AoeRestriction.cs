using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeRestriction : Effect
{
    // Start is called before the first frame update
    void Start()
    {
        activateOn = new List<Type> { Type.onTargeting, Type.onDealingDamage };
    }

   public override void Activate(AttackData attack)
    {
        if(attack.targets.Count > 1 || attack.TargetsContainEnemy())
        {
            attack.validTargets = false;
        }
    }
}
