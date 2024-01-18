using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Structs : ScriptableObject
{

    [System.Serializable]
    public struct EnumIntStruct<T>
    {
        public T type;
        public int value;

        public EnumIntStruct(T type, int value)
        {
            this.type = type;
            this.value = value;
        }
    }

    public static int GetValue<T>(T type, EnumIntStruct<T>[] arr)
    {
        return arr.FirstOrDefault(item => item.type.Equals(type)).value;
    }

    public static void SetValue<T>(T type, EnumIntStruct<T>[] arr, int newValue)
    {
        EnumIntStruct<T> itemToUpdate = arr.FirstOrDefault(item => item.type.Equals(type));
        itemToUpdate.value = newValue;
    }

    public static EnumIntStruct<T>[] InitEnumIntStructArr<T>()
    {
        T[] allValues = (T[])Enum.GetValues(typeof(T));

        return allValues.Select(value => new EnumIntStruct<T>(value, 0)).ToArray();
    }

    [System.Serializable]
    public struct EnumBoolStruct<T>
    {
        public T type;
        public bool value;

        public EnumBoolStruct(T type, bool value)
        {
            this.type = type;
            this.value = value;
        }
    }

    public static void TransferDataToDictionary<T>(EnumIntStruct<T>[] arr, Dictionary<T, int> dict)
    {
        if (arr == null)
        {
            return;
        }    
        foreach (EnumIntStruct<T> s in arr)
        {
            dict.Add(s.type, s.value);
        }
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(Structs.EnumIntStruct<>))]
public class EnumIntStructDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Draw the type field
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        Rect typeRect = new Rect(position.x, position.y, position.width / 2, position.height);
        EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("type"), GUIContent.none);

        // Draw the value field with a special label for immunity
        Rect valueRect = new Rect(position.x + position.width / 2, position.y, position.width / 2, position.height);
        EditorGUI.BeginChangeCheck();
        string valueString = EditorGUI.TextField(valueRect, property.FindPropertyRelative("value").intValue.ToString());
        if (EditorGUI.EndChangeCheck())
        {
            // Check if the input is "Immune" and assign int.MaxValue
            if (valueString.ToLower() == "immune")
            {
                property.FindPropertyRelative("value").intValue = int.MaxValue;
            }
            else
            {
                // If not "Immune", try parsing the integer
                int parsedValue;
                if (int.TryParse(valueString, out parsedValue))
                {
                    property.FindPropertyRelative("value").intValue = parsedValue;
                }
            }
        }

        EditorGUI.EndProperty();
    }
}
#endif