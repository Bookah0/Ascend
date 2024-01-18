using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    public Enums.Rarity rarity;
    public int weight = 0;
    public int value = 0;
    public string flavorText = "";
}
