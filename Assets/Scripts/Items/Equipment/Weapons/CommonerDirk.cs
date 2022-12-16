using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonerDirk : Weapon
{
    

    public override void Equip()
    {
        base.Equip();
        Stats stats = wielder.GetComponent<Stats>();
        if (stats.agility >= 3)
        {
            stats.critChance += 1;
        }
    }

    public override void UnEquip()
    {
        base.UnEquip();
        Stats stats = wielder.GetComponent<Stats>();
        if (stats.agility >= 3)
        {
            stats.critChance -= 1;
        }
    }
}