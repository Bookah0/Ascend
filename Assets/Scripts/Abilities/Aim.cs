using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : Ability{
    
    public override void Learn(GameObject parent)
    {
        Learn(parent, 0, 2, 1);
    }

    public override void Activate(List<GameObject> targets)
    {
        AttackData attackData = new AttackData();
        Player player = gameObject.GetComponent<Player>();
        Stats stats = gameObject.GetComponent<Stats>();
        WeaponData mainHand = gameObject.GetComponent<Inventory>().GetEquippedWeapon(WeaponData.Handed.mainHanded, 0);

        RestoreDefaults();

        if (mainHand.type == WeaponData.WeaponType.bow)
        {
            attackData.graceConversion += 3;
            attackData.dodgeChance = -1;
            attackData.CalculateHit();
            if (player.HasProficiency("Archery"))
            {
                attackData.critChance += 3;
            }
            if (attackData.isMiss)
            {
                ChangeCooldown(-1);
                if (player.HasProficiency("Hand - eye coordination"))
                {
                    attackData.isMiss = false;
                }
            }
            if (!attackData.isMiss)
            {
                ChangeCooldown(2);
                if (attackData.isCrit)
                {
                    ChangeCooldown(-1);
                }
                stats.actionPoints -= mainHand.baseActionCost;
                attackData.DealDamage(1, 2, Enums.Damage.Pierce);
            }
        } 
        else
        {
            Debug.Log("not wielding a bow");
        }
    }
}
