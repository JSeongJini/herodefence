using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MonsterStatData", menuName = "ScriptableObjects/MonsterStat/Data", order = 1)]
public class MonsterStatData : ScriptableObject
{
    public int level;

    [Tooltip("몬스터 이름")]
    public string monsterName;
    
    [Tooltip("몬스터의 최대 체력")]
    [SerializeField] private int maxHp;

    [Range(1f, 100f)]
    [Tooltip("몬스터의 이동 속도")]
    [SerializeField] private float speed;

    [Range(0f, 50f)]
    [Tooltip("몬스터의 방어력, 최종 데미지 = [최종 공격력 * (몬스터의 방어력 - 무기의 관통력)%]")]
    [SerializeField] private int armor;

    public int MaxHp { get { return maxHp; } }
    public float Speed { get { return speed; } }
    public int Armor { get { return armor; } }
}
