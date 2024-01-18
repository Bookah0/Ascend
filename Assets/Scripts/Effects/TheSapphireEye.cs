using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TheSapphireEye : EquipmentEffect
{
    public override void OnEquip(GameObject wearer)
    {
        Checks.OnCheck += IncreaseMentCheck;
    }

    public override void OnUnequip(GameObject wearer)
    {
        Checks.OnCheck += IncreaseMentCheck;
    }

    private int IncreaseMentCheck(Checks.Type type, GameObject source, GameObject target, Enums.Attribute checkAttribute)
    {
        if(checkAttribute == Enums.Attribute.Mental)
        {
            return 1;
        }
        return 0;
    }

}
