using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_IceField : SkillBase 
{
    public override void Ability(Monster _monster, Weapon _equipWeapon)
    {
        MonsterStatModifier modifier = new MonsterStatModifier(EStatModifier.MUL, 0.3f, 0f, 8f);
        _monster.monsterStat.AddModifier(modifier);
    }
}
