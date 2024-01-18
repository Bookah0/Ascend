using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Effect : MonoBehaviour
{
    public List<Type> activateOn;
    public List<GameObject> targets;
    public GameObject activator;
    public int range;
    public Ability usingAbility;

    public enum Type
    {
        beginningOfCombat,
        inCombat,
        outOfCombat,
        onTakingDamage,
        onDealingDamage,
        healing,
        anyWeaponAttack,
        specificWeaponAttack,
        tileWithToken,

        onTargeting,
        onCrit,
        onMiss,
        onHit,
        onDodge,
        onWalking,
        onAttacking,
        onDeath,
        onAttackingTile,

        chaosDamage,
        burnDamage,
        coldDamage,
        mindDamage,
        pierceDamage,
        bluntDamage,
        slashDamage,

        confusion,

        ifHolder,
        ifAlly,
        ifEnemy,
        ifOtherAlly,

        causes,

        toAlly,
        toHolder,
        toEnemy,
    }

    public virtual void Activate(AttackData attack) {
    }

    public virtual List<Enums.StatusEffect> NegatesStatusEffects()
    {
        return null;
    }

    public virtual int StatusEffectDurationChange()
    {
        return 0;
    }
}