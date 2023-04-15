using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/Skill/SkillData", order = 1)]
public class SkillData : ScriptableObject
{
    public int id;

    public string skillName;

    [TextArea]
    public string description;

    public Sprite sprite;
    
    public GameObject prefab;

    public AudioClip sfx;

    [Tooltip("발동 확률")]
    [Range(0f, 100f)]
    public float chance;

    [Tooltip("무기 공격력 x N %, 1 = 100%")]
    [Range(0f, 20f)]
    public float damageMultiplier;

    [Range(3f, 60f)]
    public float radius;
}
