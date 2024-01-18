using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEquipment", menuName = "Create Equipment")]
public class EquipmentData : ItemData
{
    public bool isBuffer;
    public Slot slot;
    public Material material;
    [SerializeField] public Structs.EnumIntStruct<Enums.Attribute>[] attributeReq;
    [SerializeField] public Structs.EnumIntStruct<Enums.Damage>[] damageResistances;
    [SerializeField] public Structs.EnumIntStruct<Enums.Damage>[] grazeResistances;
    [SerializeField] public Structs.EnumIntStruct<Enums.Damage>[] critResistances;
    [SerializeField] public Structs.EnumIntStruct<Enums.Condition>[] conditionResistances;


    // Enums
    public enum Slot
    {
        head,
        feet,
        chest,
        hands,
        finger,
        neck,
        legs,
        trinket
    }

    public enum Material
    {
        none,
        cloth,
        leather,
        mail,
        metal,
        ethereal
    }
}
