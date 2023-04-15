using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWeaponGripType {
    RIGHT, LEFT, BOTH
}

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/Weapon/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    [Header("Stat")]
    public float damage;

    [Range(1f, 100f)][Tooltip("1초에 [rate X 0.1]회 공격, 예)rate = 10, 1초에 1회 공격")]
    public float rate;      //attack delay is 1f / (rate * 0.1f) second

    [Range(1f, 3f)]
    public float strike;

    [Tooltip("관통력, 몬스터 아머 무시, [최종 데미지 X (100f - (몬스터의 아머수치 - 관통력)")]
    public float penetrate; 

    [Range(0f, 100f)]
    [Tooltip("치명타 확률")]
    public float critical;

    [Range(0f, 20f)]
    [Tooltip("치명타 배율, 치명타 발생 시 [데미지 X 치명타 배율]")]
    public float criticalMul;

    [Range(0f, 100f)]
    [Tooltip("추가 스킬 발동 확률, 스킬 발동 확률 = [(스킬 기본 발동 확률 + 추가 스킬 발동 확률)]%")]
    public float additionalSkillChance;   

    [Range(0f, 20f)]
    [Tooltip("추가 스킬 데미지, 스킬 공격력 = [기본 공격력 X (스킬 기본 데미지 + 추가 스킬 데미지]")]
    public float additionalSkillDamage;

    [Range(0f, 20f)]
    [Tooltip("추가 스킬 범위, 스킬 범위 = [(스킬 기본 범위 + 추가 스킬 범위)]")]
    public float additionalSkillRange;

    [Space][Header("For UI")]
    public string weaponName;

    [TextArea]
    public string description;

    public GameObject prefab;

    public RuntimeAnimatorController controller;

    public Sprite sprite;

    [Tooltip("무기 그립타입, 어느 손에 쥐는가?")]
    public EWeaponGripType gripType;
}
