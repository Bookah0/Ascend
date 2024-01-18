using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EquipmentEffect : Effect
{
    public virtual void OnEquip(GameObject wearer) { }

    public virtual void OnUnequip(GameObject wearer) { }
}