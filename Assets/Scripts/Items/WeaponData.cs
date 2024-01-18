using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Create Weapon")]
public class WeaponData : ItemData
{
 
    public WeaponType type;
    public Handed handed;

    public bool throwable = false;
    public bool dualWield = false;
    public bool hasReach;

    public int baseActionCost;
    public int dualWieldPenalty;

    public string toImplement = "";

    [SerializeField] public Structs.EnumIntStruct<Enums.Attribute>[] attributeReq;
    [SerializeField] public Structs.EnumIntStruct<Enums.Attribute>[] dualWieldReq;

    public List<int> bleedsAt;
    public List<Enums.Damage> damageTypes;

    public enum WeaponType
    {
        bow,
        axe,
        twoHandedAxe,
        dagger,
        mace,
        polearm,
        staff,
        sword
    }

    public enum Handed
    {
        oneHanded,
        offHanded,
        twoHanded,
        mainHanded
    }

}
