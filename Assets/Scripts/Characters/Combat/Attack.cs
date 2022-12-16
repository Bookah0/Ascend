using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public GameObject target;
    public GameObject user;

    public float graceConversion;
    public float dodgeChance;
    public float critChance;

    public bool isMiss;
    public bool isCrit;

    public void CalculateHit() { }

    public void DealDamage(GameObject target) { }
}
