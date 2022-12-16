using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcanePull : Ability
{

    public override void Learn(GameObject parent)
    {
        Player player = parent.GetComponent<Player>();
        if (player.mental >= 1)
        {
            player.LearnAbility(this);
        }
    }

    public override void Activate(GameObject parent)
    {
        Player player = parent.GetComponent<Player>();
        float attribute = player.mental;
        if(player.HasProficiency("Intimidation") && player.PlayerChoice("str instead of ment")){
            attribute = player.strength;
        }
        RestoreDefults();
        List<GameObject> targets = player.position.CharactersInAoe(attribute);
        
        foreach (GameObject target in targets){
            Checks checks = target.GetComponent<Checks>();
            if (checks.Check(Checks.Type.pull, parent, target) == true)
            {
                Movement movement = target.GetComponent<Movement>();
                if (proficiencies.Contains("Conjure Force") && player.PlayerChoice("pull 2 instead of 1"))
                {
                    movement.Pull(2, player.position);
                }
                else
                {
                    movement.Pull(1, player.position);
                }
            }
        }
        cooldownTime += 4;
        player.actionPoints -= 5-attribute;
    }
}
