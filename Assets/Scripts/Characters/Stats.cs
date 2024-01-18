using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * Contains all stats of a certain character, player or enemy. The original script is permanent stats. The addStaticStats is the stat increases from sources like armor 
 * and addCombatStats are temporary stat increases from combat effects (turn based).
 */
public class Stats : MonoBehaviour
{
    public int level;
    public int health;
    public int actionPoints;
    public int maxAP;
    public int maxMovement;
    public int curMovement;
    public int critChance;
    public int hitChance;
    public int dodgeChance;
    
    public List<Enums.StatusEffect> permanentStatusEffects = new();

    public Dictionary<Enums.Damage, int> dmgResistances = new();
    public Dictionary<Enums.Damage, int> grazeResistances = new();
    public Dictionary<Enums.Damage, int> critResistances = new();
    public Dictionary<Enums.Attribute, int> baseAttributes = new();
    public Dictionary<Enums.Attribute, int> attributes = new();
    public Dictionary<Enums.Condition, int> conditions = new();
    public Dictionary<Enums.Condition, int> conditionResistances = new();
    public Dictionary<Enums.Condition, int> conditionDurationCap = new();
    public Dictionary<Enums.StatusEffect, int> statusEffects = new();
    public Dictionary<Enums.StatusEffect, int> statusCheckResistances = new();
    public Dictionary<Enums.StatusEffect, int> statusEffectDurationCap = new();

    private void InitZeroValueDict<T>(Dictionary<T, int> dict)
    {
        dict = Enum.GetValues(typeof(T)).Cast<T>().ToDictionary(key => key, value => 0);
    }

    internal void DecreaseMovement(int val)
    {
        curMovement -= val;
    }

    public void InitDictionaries()
    {
        InitZeroValueDict(attributes);
        InitZeroValueDict(dmgResistances);       
        InitZeroValueDict(grazeResistances);       
        InitZeroValueDict(critResistances);        
        InitZeroValueDict(conditions);
        InitZeroValueDict(conditionResistances);       
        InitZeroValueDict(statusEffects);
        InitZeroValueDict(statusCheckResistances);
        InitZeroValueDict(statusEffectDurationCap);

        conditionDurationCap = new()
        {
            { Enums.Condition.Burning, 2 },
            { Enums.Condition.Poison, 3 },
            { Enums.Condition.Bleeding, 4 },
            { Enums.Condition.Corrosion, 2 }
        };
    }

    public void TransferData(CharacterData data)
    {
        InitDictionaries();
        Structs.TransferDataToDictionary(data.attributes, attributes);
        Structs.TransferDataToDictionary(data.damageResistances, dmgResistances);
        Structs.TransferDataToDictionary(data.grazeResistances, grazeResistances);
        Structs.TransferDataToDictionary(data.critResistances, critResistances);
        Structs.TransferDataToDictionary(data.conditionResistances, conditionResistances);

        health = 10 + level * (attributes[Enums.Attribute.Physical] + 1);
        maxMovement = 2 + (attributes[Enums.Attribute.Agility] % 2);
        maxAP = 4;
    }

    public delegate void AppliedStatusEffect(Enums.StatusEffect statusEffect, GameObject source, GameObject target, int duration);
    public static event AppliedStatusEffect OnStatusEffectApplied;

    public void ApplyStatusEffect(Enums.StatusEffect type, GameObject source, GameObject target, int duration)
    {
        OnStatusEffectApplied(type, source, target, duration);
        statusEffects.Add(type, duration + statusEffects[type]);
        if (statusEffects[type] > statusEffectDurationCap[type])
        {
            statusEffects.Add(type, statusEffectDurationCap[type]);
        }
    }

    public bool MeetsStatRequirments(WeaponData weapon)
    {
        return attributes.All(kvp => Structs.GetValue(kvp.Key, weapon.attributeReq) > kvp.Value);
    }

    public bool MeetsStatRequirments(EquipmentData equipment)
    {
        return attributes.All(kvp => Structs.GetValue(kvp.Key, equipment.attributeReq) > kvp.Value);
    }

    public bool CanDualWield(WeaponData weapon)
    {
        return attributes.All(kvp => Structs.GetValue(kvp.Key, weapon.dualWieldReq) > kvp.Value);
    }

    public delegate int InitiativeRoll(GameObject source);
    public static event InitiativeRoll OnInitiativeRoll;
    public int RollInitiative(GameObject source)
    {
        int rollResult = UnityEngine.Random.Range(1, 21);
        rollResult += attributes[Enums.Attribute.Agility];
        //rollResult += OnInitiativeRoll(source);
        return rollResult;
    }

    public void UpdateStats(GameObject wielder, WeaponData weapon) { }

    public void ApplyPermanentStatusEffect(Enums.StatusEffect type)
    {
        statusEffects.Add(type, int.MaxValue);
    }

    public void CauseCondition(Enums.Condition type, int duration)
    {
        conditions.Add(type, duration + conditions[type]);
        if(conditions[type] > conditionDurationCap[type])
        {
            conditions.Add(type, conditionDurationCap[type]);
        }
    }

    internal void NewTurn()
    {
        curMovement = maxMovement;
        actionPoints = maxAP;
    }


    // Getters & setters
    public int GetAttribute(Enums.Attribute checkAttribute)
    {
        return attributes[checkAttribute];
    }

}
