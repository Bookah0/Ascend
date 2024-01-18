using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBush : Effect
{
    public override void Activate(AttackData attack)
    {
        if(attack.targets[0].GetComponent<Tile>() != null)
        {
            var tScript = targets[0].GetComponent<Tile>();
            if (tScript.HasToken(Token.Type.bush))
            {
                tScript.RemoveToken(Token.Type.bush);
            }
        }
    }
}
