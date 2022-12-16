using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneChop : Ability
{
    public GameObject target;


    public override void Learn(GameObject parent)
    {
        base.Learn(parent, 2, 0, 2);
    }

    public override void Activate(GameObject parent)
    {
        Player player = parent.GetComponent<Player>();
        firstTurnActivate = false;
        RestoreDefults();
        EquippedItems equippedItems = parent.GetComponent<EquippedItems>();
        Weapon mainHand = equippedItems.mainHand.GetComponent<Weapon>();
        Tile tile = target.GetComponent<Tile>();
        if (mainHand.type == Weapon.WeaponType.axe)
        {
            if (tile.HasToken(Tile.Token.tree) && mainHand.handed == Weapon.Handed.twoHanded)
            {
                tile.FellTree();
            }
            else if (tile.blockage == Tile.Blockage.wood)
            {
                tile.DestroyBlockage(Tile.Blockage.wood);
                if (firstTurnActivate)
                {
                    actionCost -= 2;
                    cooldownTime -= 2;
                }
            }
            else if (tile.blockage == Tile.Blockage.ice)
            {
                tile.DestroyBlockage(Tile.Blockage.ice);
                if (firstTurnActivate)
                {
                    actionCost -= 2;
                    cooldownTime -= 2;
                }
            }
            else if (tile.blockage == Tile.Blockage.stone && player.HasProficiency("Axes")){
                tile.DestroyBlockage(Tile.Blockage.stone);
            }
            if (proficiencies.Contains("Conjure Force")) {
                tile.DestroyConnectedBlockage();
            }
            cooldownTime += 3;
            player.actionPoints -= mainHand.baseActionCost;

        }
        else
        {
            print("not wielding an axe");
        }
    }
}
