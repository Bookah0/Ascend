using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public PlayerData data;

    private void Start()
    {
        var stats = gameObject.AddComponent<Stats>();
        stats.TransferData(data);
        EquipAbility(typeof(ArcanePull));
    }

    public void EquipAbility(Type abilityType)
    {
        Ability ability = gameObject.AddComponent(abilityType) as Ability;
        abilitySlots[0] = ability;
    }

    private List<Ability> learnedAbilities = new() { };

    public void LearnAbility(Ability ability)
    {
        learnedAbilities.Add(ability);
    }

    public bool PlayerChoice(string q) {
        return true;
    }

    public List<Ability> GetLearnedAbilities()
    {
        return learnedAbilities;
    }
}
