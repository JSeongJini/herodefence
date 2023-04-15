using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageTypeStyle", menuName = "ScriptableObjects/DamageTypeStyle", order = 3)]
public class DamageTextStyle : ScriptableObject
{
    public EDamageType type;
    public Color color;

    public AnimationCurve opacityCurve;
    public AnimationCurve scaleCurve;
    public AnimationCurve heightCurve;
}
