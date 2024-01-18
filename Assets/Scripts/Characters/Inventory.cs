using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<ItemData, int> inventory = new Dictionary<ItemData, int>{};

    public Dictionary<EquipmentData.Slot, EquipmentData[]> equippedArmor = new Dictionary<EquipmentData.Slot, EquipmentData[]>
    {
        { EquipmentData.Slot.head, null },
        { EquipmentData.Slot.chest, null },
        { EquipmentData.Slot.hands, null },
        { EquipmentData.Slot.legs, null },
        { EquipmentData.Slot.feet, null },
        { EquipmentData.Slot.neck, null },
        { EquipmentData.Slot.finger, new EquipmentData[2] { null, null } },
        { EquipmentData.Slot.trinket, new EquipmentData[2] { null, null } },
    };

    public Dictionary<WeaponData.Handed, WeaponData[]> equippedWeapons = new Dictionary<WeaponData.Handed, WeaponData[]>
    {
        { WeaponData.Handed.mainHanded, new WeaponData[2] { null, null } },
        { WeaponData.Handed.offHanded, new WeaponData[2] { null, null } }
    };


    // Weapons
    public virtual void Equip(WeaponData weapon, WeaponData.Handed slot, int weaponSet)
    {
        Stats stats = transform.parent.gameObject.GetComponent<Stats>();
        if (stats.MeetsStatRequirments(weapon))
        {
            //stats.UpdateStats(wielder, weapon);
            if(slot == WeaponData.Handed.mainHanded && weapon.handed == WeaponData.Handed.offHanded)
            {
                Debug.Log("Can't equip off-hand weapon in main-hand");
                return;
            }
            else if (slot == WeaponData.Handed.offHanded && weapon.handed == WeaponData.Handed.mainHanded)
            {
                Debug.Log("Can't equip main-hand weapon in off-hand");
                return;
            }
            else if (slot == WeaponData.Handed.offHanded && weapon.dualWield && !stats.CanDualWield(weapon))
            {
                Debug.Log("Can't dual-wield");
                return;
            }
            else
            {
                if (weapon.handed == WeaponData.Handed.twoHanded)
                {
                    UnEquip(WeaponData.Handed.mainHanded, weaponSet);
                    UnEquip(WeaponData.Handed.offHanded, weaponSet);
                    equippedWeapons[WeaponData.Handed.mainHanded][weaponSet] = weapon;
                    equippedWeapons[WeaponData.Handed.offHanded][weaponSet] = weapon;
                }
                else
                {
                    if (equippedWeapons[slot][weaponSet].handed == WeaponData.Handed.twoHanded)
                    {
                        UnEquip(WeaponData.Handed.mainHanded, weaponSet);
                        UnEquip(WeaponData.Handed.offHanded, weaponSet);
                    }
                    else
                    {
                        UnEquip(slot, weaponSet);
                    }
                    equippedWeapons[slot][weaponSet] = weapon;
                }
            }
        }
        else
        {
            Debug.Log("Doesn't meet the attribute requierments");
            return;
        }
    }

    public virtual void UnEquip(WeaponData.Handed slot, int weaponSet)
    {
        if (equippedWeapons[slot][weaponSet] != null)
        {
            inventory.Add(GetEquippedWeapon(slot, weaponSet), 1);
            equippedWeapons[slot][weaponSet] = null;
        }
        else
        {
            Debug.Log("Nothing to unequip");
        }
    }

    public WeaponData GetEquippedWeapon(WeaponData.Handed slot, int weaponSet)
    {
        return equippedWeapons[slot][weaponSet];
    }

    // Armor
    public virtual void Equip(EquipmentData equipment, EquipmentData.Slot slot)
    {
        Stats stats = transform.parent.gameObject.GetComponent<Stats>();
        if (!stats.MeetsStatRequirments(equipment))
        {
            Debug.Log("Doesn't meet the attribute requierments");
            return;
        }
        //stats.UpdateStats(wielder, weapon);
        if (slot != equipment.slot)
        {
            Debug.Log("Can't equip that item in that slot");
            return;
        }
        else
        {
            if (equipment.slot == EquipmentData.Slot.finger || equipment.slot == EquipmentData.Slot.trinket)
            {
                if(equippedArmor[slot][0] == null)
                {
                    equippedArmor[slot][0] = equipment;
                } 
                else if (equippedArmor[slot][1] == null)
                {
                    equippedArmor[slot][1] = equipment;
                }
                else
                {
                    UnEquip(slot, 1);
                    equippedArmor[slot][1] = equipment;
                }
            }
            else
            {
                UnEquip(slot);
                equippedArmor[slot][0] = equipment;
            }
        }
    }

    public virtual void UnEquip(EquipmentData.Slot slot)
    {
        if (slot != EquipmentData.Slot.finger && slot != EquipmentData.Slot.trinket && equippedArmor[slot] != null)
        {
            inventory.Add(GetEquippedArmor(slot), 1);
            equippedArmor[slot][0] = null;
        }
    }

    public virtual void UnEquip(EquipmentData.Slot slot, int set)
    {
        if ((slot == EquipmentData.Slot.finger || slot == EquipmentData.Slot.trinket) && equippedArmor[slot] != null)
        {
            inventory.Add(GetEquippedArmor(slot, set), 1);
            equippedArmor[slot][set] = null;
        }
    }

    public EquipmentData GetEquippedArmor(EquipmentData.Slot slot)
    {
        return equippedArmor[slot][0];
    }

    public EquipmentData GetEquippedArmor(EquipmentData.Slot slot, int set)
    {
        return equippedArmor[slot][set];
    }

}
