using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Create Character")]
public class CharacterData : ScriptableObject
{
    public enum Race { Human, Voidling, Beast }
    public enum Region { TheDevoidLands, TheBitingShores, Pieniar, BalgonForest }
    public enum Faction { Void, Velorian, Galligan, Pieniar, Valndor }

    public List<Race> types;
    public List<Region> regions;
    public List<Faction> factions;
    public int expOnKill;

    [SerializeField] public Structs.EnumIntStruct<Enums.Attribute>[] attributes;
    [SerializeField] public Structs.EnumIntStruct<Enums.Damage>[] damageResistances;
    [SerializeField] public Structs.EnumIntStruct<Enums.Damage>[] grazeResistances;
    [SerializeField] public Structs.EnumIntStruct<Enums.Damage>[] critResistances;
    [SerializeField] public Structs.EnumIntStruct<Enums.Condition>[] conditionResistances;
}
