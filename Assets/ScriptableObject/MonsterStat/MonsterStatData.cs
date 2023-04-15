using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MonsterStatData", menuName = "ScriptableObjects/MonsterStat/Data", order = 1)]
public class MonsterStatData : ScriptableObject
{
    public int level;

    [Tooltip("���� �̸�")]
    public string monsterName;
    
    [Tooltip("������ �ִ� ü��")]
    [SerializeField] private int maxHp;

    [Range(1f, 100f)]
    [Tooltip("������ �̵� �ӵ�")]
    [SerializeField] private float speed;

    [Range(0f, 50f)]
    [Tooltip("������ ����, ���� ������ = [���� ���ݷ� * (������ ���� - ������ �����)%]")]
    [SerializeField] private int armor;

    public int MaxHp { get { return maxHp; } }
    public float Speed { get { return speed; } }
    public int Armor { get { return armor; } }
}
