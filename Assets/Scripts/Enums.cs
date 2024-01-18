using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    public enum Rarity
    {
        common,
        uncommon,
        rare,
        legendary
    }

    public enum Attribute
    {
        Physical,
        Mental,
        Agility
    }

    public enum Damage
    {
        Blunt,
        Slash,
        Pierce,
        Burn,
        Cold,
        Mind,
        Chaos,
        Void,
        Shock
    }

    public enum StatusEffect
    {
        Crippled,
        Stunned,
        Frozen,
        Immobilized,
        Dazed,
        FrozenFeet,
        Confused,
        Feared,
        Muted,
        Blinded
    }

    public enum Condition
    {
        Burning,
        Poison,
        Bleeding,
        Corrosion,
        Injury
    }
}
