using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectCore  {

    public EffectType m_EffectType;

    Color[] SignalColor = new Color[4] { Color.white, Color.red, Color.green, Color.black };

    public Color GetColor()
    {
        return SignalColor[(int)m_EffectType];
    }
}

public enum EffectType
{
    White,
    Red,
    Green,
    Black
}