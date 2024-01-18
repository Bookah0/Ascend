using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Create Ability")]
public class AbilityData : ScriptableObject
{
    public int cooldownTime;
    public int activeTime;
    [SerializeField] public Range range;
    public int width;
    public bool hasReach;
    public int actionCost;
    protected bool firstTurnActivate = false;
    public Enums.Damage damageType;
    public AttackData.Target typeOfTargets;
    [SerializeField] public Structs.EnumIntStruct<Enums.Attribute>[] attributeReq;


    public struct Range
    {
        private Enums.Attribute attributeType;
        private int numericValue;

        // Constructors
        public Range(Enums.Attribute type)
        {
            attributeType = type;
            numericValue = 0;
        }

        public Range(int value)
        {
            attributeType = 0;
            numericValue = value;
        }
    }
}
