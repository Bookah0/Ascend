using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ValnaiCowl : EquipmentEffect
{
    public override void OnEquip(GameObject wearer)
    {
        Stats.OnStatusEffectApplied += OnConfusionAppliedHandler;
    }

    public override void OnUnequip(GameObject wearer)
    {
        Stats.OnStatusEffectApplied -= OnConfusionAppliedHandler;
    }

    private void OnConfusionAppliedHandler(Enums.StatusEffect type, GameObject source, GameObject target, int duration)
    {
        if (source.GetComponent<Character>().IsAlly())
        {
            ChooseNewCharacterForTurn(target);
        }
    }

    private void ChooseNewCharacterForTurn(GameObject target)
    {
        var playerStats = target.GetComponent<Stats>();
        range = playerStats.attributes[Enums.Attribute.Mental];
        //target = GetTarget(range).GetComponent<Stats>();
        // target.CauseStatusEffect(StatusEffect.Type.CONFUSED, 1);
    }
}
