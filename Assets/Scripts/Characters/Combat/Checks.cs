using UnityEngine;

public class Checks : MonoBehaviour
{

    // Events
    public delegate int OnCheckEvent(Type type, GameObject source, GameObject target, Enums.Attribute checkAttribute);
    public delegate void OnCheckResultEvent(Type type, GameObject source, GameObject target);

    public static event OnCheckEvent OnCheck;
    public static event OnCheckResultEvent OnCheckSuccess;
    public static event OnCheckResultEvent OnCheckFail;

    public enum Type
    {
        pull,
        push,
        stun,
        freeze,
        confusion
    }

    public static bool Check(Type type, GameObject source, GameObject target)
    {
        Stats sourceStats = source.GetComponent<Stats>();
        Stats targetStats = target.GetComponent<Stats>();
        int rollResult = Random.Range(1, 21);
        bool checkSuccess = false;

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
            case Type.confusion:
                Enums.Attribute checkAttribute = Enums.Attribute.Mental;
                int rollModifier = sourceStats.GetAttribute(checkAttribute) - targetStats.GetAttribute(checkAttribute);
                rollModifier += OnCheck(Type.confusion, source, target, checkAttribute);
                checkSuccess = (rollResult + rollModifier >= 10) || checkSuccess;
                break;
            default:
                break;
        }

        if (checkSuccess)
        {
            OnCheckSuccess(Type.confusion, source, target);
        }
        else
        {
            OnCheckFail(Type.confusion, source, target);
        }
        return checkSuccess;
    }
}
