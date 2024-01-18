using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public GameObject wearer;
    public EquipmentData data;

    public virtual void Effects() { }
}