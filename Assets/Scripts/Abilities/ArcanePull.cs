using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcanePull : Ability
{

    private void Start()
    {
        Initialize("Arcane Pull");
    }

    public override void Learn(GameObject player)
    {
        Learn(player, 0, 0, 2);
    }

    public override void Activate(List<GameObject> targets)
    {
        //if(IsOnCooldown())
        //{
        //    Debug.Log("Ability is on cooldown");
        //    return;
        //}

        //RestoreDefaults();
        Stats playerStats = gameObject.GetComponent<Stats>();
        Player player = gameObject.GetComponent<Player>();

        int range = IntimidationEffect(playerStats);

        foreach (GameObject target in targets){
            Movement movement = gameObject.GetComponent<Movement>();
            if (player.HasProficiency("Conjure Force")){
                ConjureForceEffect(player, movement, targets);
            }
            else
            {
                movement.Pull(player, 1, targets[0]);
            }
        }
        ChangeCooldown(4);
        playerStats.actionPoints -= 2;
    }

    private int IntimidationEffect(Stats stats)
    {
        int phys = stats.GetAttribute(Enums.Attribute.Physical);
        int ment = stats.GetAttribute(Enums.Attribute.Mental);
        if (ment < phys)
        {
            return phys;
        }
        return ment;
    }

    private void ConjureForceEffect(Player player, Movement movement, List<GameObject> targets)
    {
        if (player.PlayerChoice("pull 2 instead of 1"))
        {
            movement.Pull(player, 2, targets[0]);
        }
        else
        {
            movement.Pull(player, 1, targets[0]);
        }
    }
}
