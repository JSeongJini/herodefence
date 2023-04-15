using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_BloodField : SkillBase
{
    [SerializeField] private float duration;

    private WaitForSeconds period;

    private void Start()
    {
        period = new WaitForSeconds(1f);
    }

    public override void Ability(Monster _monster, Weapon _equipWeapon)
    {
        float bleedingDamage = Mathf.Floor((_equipWeapon.Damage + _equipWeapon.AdditionalSkillDamage)* 0.2f);

        if(bleedingDamage >= 1)
            _monster.StartCoroutine(BleedingCoroutine(_monster, _equipWeapon, bleedingDamage));
    }

    private IEnumerator BleedingCoroutine(Monster _monster, Weapon _equipWeapon, float _damage)
    {
        yield return null;

        float time = 0f;
        while(_monster && time < duration)
        {
            _monster.TakeDamage(_damage, _monster.monsterStat.Armor, EDamageType.BLEEDING);
            time += 1f;
            yield return period;
        }
    }
}
