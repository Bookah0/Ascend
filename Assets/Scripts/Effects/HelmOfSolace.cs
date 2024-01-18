using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmOfSolace : EquipmentEffect
{
    public override void OnEquip(GameObject wearer)
    {
        AttackData.OnTargeting += OnDirectEnemyTargetHandler;
    }

    public override void OnUnequip(GameObject wearer)
    {
        AttackData.OnTargeting -= OnDirectEnemyTargetHandler;
    }

    private void OnDirectEnemyTargetHandler(GameObject primaryTarget, AttackData attackData)
    {
        if (!primaryTarget.GetComponent<Character>().IsAlly())
        {
            attackData.validTargets = false;
            Debug.Log("Helm of Solace prevents targeting enemies directly");
        }
    }
}
