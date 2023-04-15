using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_PoisonExplosion : SkillBase
{
    [SerializeField] private float duration;
    
    private WaitForSeconds period;

    private void Start()
    {
        period = new WaitForSeconds(1f);
    }

    public override void Ability(Monster _monster, Weapon _equipWeapon)
    {
        float poisonDamage = Mathf.Floor(_monster.monsterStat.MaxHp * (0.01f + (_equipWeapon.AdditionalSkillDamage * 0.01f)));

        MonsterStatModifier modifier = new MonsterStatModifier(EStatModifier.MUL, 0.25f, 0f, 10f);
        _monster.StartCoroutine(PoisonCoroutine(_monster, poisonDamage));

    }

    private IEnumerator PoisonCoroutine(Monster _monster, float _damage)
    {
        yield return null;

        float time = 0f;
        while (_monster && time < duration)
        {
            _monster.TakeDamage(_damage, _monster.monsterStat.Armor, EDamageType.POISON);
            time += 1f;
            yield return period;
        }
    }
}
