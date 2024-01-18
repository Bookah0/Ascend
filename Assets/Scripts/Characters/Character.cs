using Assets.Scripts.Characters.Combat;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IMovable
{
    public Ability[] abilitySlots = new Ability[8];
    public List<Proficiency> proficiencies = new() { };
    public List<Effect> effects = new() { };
    
    public GameObject position;
    public GameObject spawnPosition;

    public void TriggerDeath()
    {
        Destroy(transform.parent.gameObject);
    }

    public bool HasProficiency(string name)
    {
        foreach (Proficiency p in proficiencies)
        {
            if (p.name.Equals(name))
            {
                return true;
            }
        }
        return false;
    }

    internal bool IsAlly()
    {
        throw new NotImplementedException();
    }

    public void MoveToSpawnPosition()
    {
        transform.position = new Vector3(spawnPosition.transform.position.x, transform.position.y, spawnPosition.transform.position.z);
        position = spawnPosition;
    }

    public GameObject GetPosition()
    {
        return position;
    }

    public void SetPosition(GameObject position)
    {
        this.position = position;
    }
}
