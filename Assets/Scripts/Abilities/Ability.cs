using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public AbilityData data;
    public List<Effect> effects;
    private int curCooldown;

    public void Initialize(string abilityDataName)
    {
        curCooldown = 0;
        var abilityDataResourcePath = "Abilities/" + abilityDataName;
        AbilityData abilityData = Resources.Load<AbilityData>(abilityDataResourcePath);

        if (abilityData != null)
        {
            data = abilityData;
        }
        else
        {
            Debug.LogError("AbilityData not found. Make sure the path is correct.");
        }
    }

    public virtual void Activate(List<GameObject> targets) { }

    public void Learn(GameObject player, float str, float agil, float ment)
    {
        Stats stats = player.GetComponent<Stats>();
        if (stats.GetAttribute(Enums.Attribute.Mental) >= ment && stats.GetAttribute(Enums.Attribute.Physical) >= str && stats.GetAttribute(Enums.Attribute.Agility) >= agil)
        {
            player.GetComponent<Player>().LearnAbility(this);
        }
        else
        {
            Debug.Log("Doesn't meet attribute requirments");
        }
    }

/*    public List<GameObject> GetTargetTiles()
    {
        List<GameObject> tiles = new() { };
        Tile tileScript = parent.GetComponent<Player>().GetComponent<Tile>();
        switch (targeting)
        {
            case Targeting.single:
                return (tileScript.GetTile());
            case Targeting.aoe:
                return (tileScript.TilesInAoe(range));
            case Targeting.cone:
                return (tileScript.GetTilesInCone(width, range));
            case Targeting.self:
                return (new List<GameObject> { parent });
            case Targeting.global:
                return (tileScript.GetTilesOnBattlefield());
        }
    }
*/
    public virtual void Learn(GameObject parent) { }

    public void RestoreDefaults()
    {
        //firstTurnActivate = false;
    }

    public void ChangeCooldown(int val)
    {
        curCooldown += val;
    }

    public bool IsOnCooldown()
    {
        return curCooldown <= 0;
    }

    /*void Update(){
    switch (state){
        case (AbilityState.active):
            if(activeTime > 0){
                activeTime -= Time.deltaTime;
            } else{
                state = AbilityState.cooldown;
                cooldownTime = ability.cooldownTime;
            }
            break;
        case (AbilityState.cooldown):
            if (cooldownTime > 0){
                cooldownTime -= Time.deltaTime;
            } else{
                state = AbilityState.ready;
            }
            break;
    }
}*/

}
