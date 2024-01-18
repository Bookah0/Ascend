using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{

    public enum Type
    {
        frozen,
        burning,
        wet,
        gas,
        mist,
        bush,
        rubble,
        stone,
        ice,
        wood,
        tree
    }

    public Type type;
}
