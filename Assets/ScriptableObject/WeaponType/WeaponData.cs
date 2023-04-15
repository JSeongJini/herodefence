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

    [Range(1f, 100f)][Tooltip("1�ʿ� [rate X 0.1]ȸ ����, ��)rate = 10, 1�ʿ� 1ȸ ����")]
    public float rate;      //attack delay is 1f / (rate * 0.1f) second

    [Range(1f, 3f)]
    public float strike;

    [Tooltip("�����, ���� �Ƹ� ����, [���� ������ X (100f - (������ �ƸӼ�ġ - �����)")]
    public float penetrate; 

    [Range(0f, 100f)]
    [Tooltip("ġ��Ÿ Ȯ��")]
    public float critical;

    [Range(0f, 20f)]
    [Tooltip("ġ��Ÿ ����, ġ��Ÿ �߻� �� [������ X ġ��Ÿ ����]")]
    public float criticalMul;

    [Range(0f, 100f)]
    [Tooltip("�߰� ��ų �ߵ� Ȯ��, ��ų �ߵ� Ȯ�� = [(��ų �⺻ �ߵ� Ȯ�� + �߰� ��ų �ߵ� Ȯ��)]%")]
    public float additionalSkillChance;   

    [Range(0f, 20f)]
    [Tooltip("�߰� ��ų ������, ��ų ���ݷ� = [�⺻ ���ݷ� X (��ų �⺻ ������ + �߰� ��ų ������]")]
    public float additionalSkillDamage;

    [Range(0f, 20f)]
    [Tooltip("�߰� ��ų ����, ��ų ���� = [(��ų �⺻ ���� + �߰� ��ų ����)]")]
    public float additionalSkillRange;

    [Space][Header("For UI")]
    public string weaponName;

    [TextArea]
    public string description;

    public GameObject prefab;

    public RuntimeAnimatorController controller;

    public Sprite sprite;

    [Tooltip("���� �׸�Ÿ��, ��� �տ� ��°�?")]
    public EWeaponGripType gripType;
}
