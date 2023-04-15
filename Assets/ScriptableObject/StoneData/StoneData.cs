using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StoneData", menuName = "ScriptableObjects/Stone/StoneData", order = 1)]
public class StoneData : ScriptableObject
{
    public EWeaponStat type;

    public float startProbability;

    public float[] increase = new float[5];
}
