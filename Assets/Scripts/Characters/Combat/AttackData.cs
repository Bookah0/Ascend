using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData : ScriptableObject
{
    public enum Target
    {
        enemy,
        allEnemies,
        player,
        character,
        allCharacters,
        otherCharacters,
        ally,
        tile,
        allAllies,
        otherAllies
    }

    public GameObject user;
    public GameObject bf;
    public List<GameObject> targets;
    public List<GameObject> targetPositions;
    public Target typesAffected;

    public float graceConversion;
    public float dodgeChance;
    public float critChance;

    public bool isMiss;
    public bool isCrit;
    public bool isGraze;
    public bool hasReach;
    public int lowDamage;
    public int highDamage;
    public int range;
    public int actionCost;
    public Enums.Damage damageType;
    public int cooldown;

    public int successRange;
    public bool validTargets = true;

    // Events
    public delegate void OnTargetingEvent(GameObject primaryTarget, AttackData attackData);
    public static event OnTargetingEvent OnTargeting;
    
    public AttackData() { }

    public Dictionary<GameObject, bool> CalculateHit() {
        var stats = user.GetComponent<Stats>();
        var result = new Dictionary<GameObject, bool>();
        foreach (GameObject target in targetPositions){
            switch (typesAffected)
            {
                case Target.enemy:
                    var enemyStats = target.GetComponent<Stats>();
                    var hitRoll = Random.Range(1, 20);
                    hitRoll -= enemyStats.hitChance;
                    hitRoll += stats.hitChance;
                    hitRoll -= enemyStats.dodgeChance;
                    hitRoll += stats.dodgeChance;
                    hitRoll -= (enemyStats.level - stats.level);
                    if(hitRoll >= successRange){
                        result.Add(target, true);
                    } else{
                        result.Add(target, false);
                    }
                    break;
                case Target.player:
                    result.Add(target, true);
                    break;
                case Target.tile:
                    result.Add(target, true);
                    break;
            }
        }
        return result;
    }

    public Dictionary<GameObject, int> CalculateDamage(int low, int high, Enums.Damage dmgType) {
        var damageDictionary = new Dictionary<GameObject, int>();
        foreach (GameObject target in targets)
        {
            var bfScript = bf.GetComponent<Battlefield>();
            var targetStats = target.GetComponent<Stats>();
            var attacker = user.GetComponent<Stats>();
            var damage = Random.Range(low, high);
            if (damage <= low)
            {
                isGraze = true;
                damage -= targetStats.grazeResistances[dmgType];
            }
            //damage += attacker.DmgChange(dmgType, isGraze);
            damage += bfScript.GlobalDmgChange(dmgType, isGraze);
            damage -= targetStats.dmgResistances[dmgType];
            if (damage < 0)
            {
                damage = 0;
            }
            damageDictionary.Add(target, damage);
        }
        return damageDictionary;
    }

    public void DealDamage(int low, int high, Enums.Damage dmgType)
    {
        var damage = CalculateDamage(low, high, dmgType);
        foreach (GameObject target in targets)
        {
            var targetStats = target.GetComponent<Stats>();
            targetStats.health -= damage[target];
            if (target.GetComponent<Stats>().health <= 0)
            {
                target.GetComponent<Enemy>().TriggerDeath();
            }
            Debug.Log("damage dealt");
        }
    }

    public bool TargetsContainEnemy()
    {
        foreach (GameObject target in targets)
        {
            if (target.GetComponent<Enemy>() != null)
            {
                return true;
            }
        }
        return false;
    }
}
