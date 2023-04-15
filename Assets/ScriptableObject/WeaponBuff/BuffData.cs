using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffData", menuName = "ScriptableObjects/Weapon/BuffData", order = 1)]
public class BuffData : ScriptableObject
{
    [Header("Buff Stat, 0.15f = 15% ¡ı∞°")]
    [SerializeField] private EWeaponStat type;
    [SerializeField] private float value;

    [Space][Header("For UI")]
    public Sprite sprite;

    public string description;

    public void Buff(BuffedWeapon _weapon)
    {
        _weapon.stats[type] += value;
    }
}
