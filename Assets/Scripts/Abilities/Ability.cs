using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public new string name;
    public float cooldownTime;
    public float activeTime;
    public float range;
    public List<string> proficiencies;
    public float actionCost;
    public bool firstTurnActivate = false;
    public List<Dice> damageDice;
    public float damageMod;
    public DamageType damageType;
    public GameObject target;

    public enum DamageType
    {
        pierce
    }

    public enum Dice
    {
        D6
    }

    public virtual void Activate(GameObject parent) { }

    public void Learn(GameObject parent, float str, float agil, float ment)
    {
        Stats stats = parent.GetComponent<Stats>();
        Player player = parent.GetComponent<Player>();
        if (stats.mental >= ment && stats.strength >= str && stats.agility >= agil)
        {
            player.LearnAbility(this);
        }
    }
    public virtual void Learn(GameObject parent) { }


    public void RestoreDefults() { }
}
