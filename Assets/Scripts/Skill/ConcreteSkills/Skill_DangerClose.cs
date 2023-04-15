using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_DangerClose : SkillBase 
{
    public override void Ability(Monster _monster, Weapon _equipWeapon)
    {
        MonsterStatModifier modifier = new MonsterStatModifier(EStatModifier.ADD, -1f, 0f, 8f);
        MonsterStatModifier modifier2 = new MonsterStatModifier(EStatModifier.MUL, 0f, 0.1f, 8f);
        _monster.monsterStat.AddModifier(modifier);
        _monster.monsterStat.AddModifier(modifier2);
    }
}
