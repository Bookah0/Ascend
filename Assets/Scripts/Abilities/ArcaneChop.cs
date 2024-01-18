using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneChop : Ability
{
    public override void Learn(GameObject parent)
    {
        base.Learn(parent, 2, 0, 2);
    }

    public override void Activate(List<GameObject> targets)
    {
        var attackData = new AttackData();
        if (targets[0].GetComponent<Tile>() != null)
        {
            Player player = gameObject.GetComponent<Player>();
            RestoreDefaults();
            WeaponData mainHand = gameObject.GetComponent<Inventory>().GetEquippedWeapon(WeaponData.Handed.mainHanded, 1);
            Tile tile = targets[0].GetComponent<Tile>();
            if (mainHand.type == WeaponData.WeaponType.axe)
            {

            }
            else
            {
                Debug.Log("not wielding an axe");
            }
        }
        else
        {
            attackData.validTargets = false;
        }
    }
}
