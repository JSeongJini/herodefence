using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Blackhole : SkillBase 
{
    public override void Ability(Monster _monster, Weapon _equipWeapon)
    {
        MonsterStatModifier modifier = new MonsterStatModifier(EStatModifier.MUL, 0.9f, 0f, 7f);
        _monster.monsterStat.AddModifier(modifier);

        _monster.transform.position = transform.position;
    }
}
