using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checks : MonoBehaviour
{
    public enum Type
    {
        pull,
        push,
        stun,
        freeze
    }

    public bool Check(Type type, GameObject target, GameObject caster)
    {
        switch (type)
        {
            case Type.pull:
                break;
            case Type.push:
                break;
            case Type.stun:
                break;
            case Type.freeze:
                break;
            default:
                break;
        }
        return true;
    }
}
