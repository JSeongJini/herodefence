using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Nuke : SkillBase 
{
    public override void Ability(Monster _monster, Weapon _equipWeapon)
    {
        MonsterStatModifier modifier = new MonsterStatModifier(EStatModifier.ADD, 0f, -2f, 10f);
        _monster.monsterStat.AddModifier(modifier);
    }
}
