using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : Ability{
    
    
    public override void Learn(GameObject parent)
    {
        base.Learn(parent, 0, 2, 1);
    }

    public override void Activate(GameObject parent)
    {
        Player player = parent.GetComponent<Player>();
        Stats stats = parent.GetComponent<Stats>();
        EquippedItems equippedItems = parent.GetComponent<EquippedItems>();
        Weapon mainHand = equippedItems.mainHand.GetComponent<Weapon>();

        RestoreDefults();

        if (mainHand.type == Weapon.WeaponType.bow)
        {
            Attack attack = parent.GetComponent<Attack>();
            attack.target = this.target;
            attack.graceConversion += 3;
            attack.dodgeChance = -1;
            attack.CalculateHit();
            if (player.HasProficiency("Archery"))
            {
                attack.critChance += 3;
            }
            if (attack.isMiss)
            {
                cooldownTime -= 1;
                if (player.HasProficiency("Hand - eye coordination"))
                {
                    attack.isMiss = false;
                }
            } if (attack.isCrit){
                cooldownTime -= 1;
            }
            attack.DealDamage(target);
            cooldownTime += 2;
            stats.actionPoints -= mainHand.baseActionCost;

        } else
        {
            print("not wielding a bow");
        }
    }
}
