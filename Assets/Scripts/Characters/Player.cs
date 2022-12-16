using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Ability> abilities = new List<Ability> { };
    public List<Proficiency> proficiencies = new List<Proficiency> { };

    public void LearnAbility(Ability ability)
    {
        abilities.Add(ability);
    }

    public bool HasProficiency(string name)
    {
        foreach (Proficiency p in proficiencies){
            if (p.name.Equals(name))
            {
                return true;
            }
        }
        return false;
    }

    public bool PlayerChoice(string q) {
        return true;
    }
}
