using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public float baseActionCost;
    public WeaponType type;
    public Handed handed;
    public GameObject wielder;
    public float bleeds = -1;
    public float poisons = -1;
    public float burns = -1;
    public bool throwable = false;
    public bool dualWield = false;
    public List<Ability> attacks = new() { };
    public List<dynamic> effects = new() { };
    public List<dynamic> specialReq = new() { };
    public int[] statRec;

    public virtual void Equip()
    {
        Stats stats = wielder.GetComponent<Stats>();
        CheckStatRequirments(wielder, this);
        CheckSpecialRequirments(this);      
        stats.UpdateStats(wielder, this);
    }

    public virtual void UnEquip()
    {

    }

    public enum Attributes
    {
        throwable,
        bleeds,
        dualWield,
    }

    public enum WeaponType
    {
        bow,
        axe,
        twoHandedAxe,
        dagger
    }

    public enum Handed
    {
        oneHanded,
        offHanded,
        twoHanded,
        mainHanded
    }

    public virtual void Effects() { }
}