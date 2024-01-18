using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject wielder;
    public WeaponData data;



    public virtual void Effects() { }
}