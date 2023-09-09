using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat  
{
   [SerializeField] private int baseValue;
   [SerializeField] private float baseFloatValue;

   public List<int> modifiers;
   public List<float> floatmodifiers;


    public int GetValue()
    {
        int finalValue = baseValue;

        foreach(int modifier in modifiers)
        {
            finalValue += modifier;
        }
        return finalValue;
    }

      public float GetflaotValue()
    {
        float finalValue = baseFloatValue;

        foreach(float modifier in modifiers)
        {
            finalValue += modifier;
        }
        return finalValue;
    }

    public void SetDefaultValue(int _value)
    {
        baseValue = _value;
    }

     public void SetDefaultFloatValue(float _value)
    {
        baseFloatValue = _value;
    }

    public void AddModifier(int _modifier)
    {
        modifiers.Add(_modifier);
    }

    public void AddFloatModifier(float _modifier)
    {
        floatmodifiers.Add(_modifier);
    }

    public void RemoveModifier(int _modifier)
    {
        modifiers.Remove(_modifier);
    }

     public void RemoveFloatModifier(float _modifier)
    {
        floatmodifiers.Remove(_modifier);
    }
}
