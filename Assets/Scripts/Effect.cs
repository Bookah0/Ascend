using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public enum Type
    {
        inCombat,
        outOfCombat,
        onDamage,
        healing,

        onCrit,
        onMiss,
        onHit,
        onDodge,
        onWalking,
        onAttacking,
        onDeath,

        chaosDamage,
        burnDamage,
        coldDamage,
        mindDamage,
        pierceDamage,
        bluntDamage,
        slashDamage
    }

    public enum Target
    {
        enemy,
        player,
        any,
        all,
        ally,
        tile
    }
}