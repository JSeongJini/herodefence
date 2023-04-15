using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class SkillBase : MonoBehaviour
{
    [HideInInspector] public SkillData skillData;
    [HideInInspector] public SkillEffectPool pool;

    private LayerMask monsterLayer = new LayerMask();

    #region Cache
    private WaitForSeconds duration = new WaitForSeconds(2f);
    private Collider[] results;
    #endregion

    private void Awake()
    {
        results = new Collider[50];
        
        monsterLayer = 1 << 7;  //Monster : 7
    }

    public void ActiveSkill(Weapon _weapon)
    {
        float range = (skillData.radius + _weapon.AdditionalSkillRange) * 0.05f;
        transform.localScale = new Vector3(range, range, range);
        StartCoroutine(SkillCoroutine(_weapon));
    }

    private IEnumerator SkillCoroutine(Weapon _weapon)
    {
        yield return null;

        if (skillData.sfx != null)
            SoundManager.Instance.PlayClip(skillData.sfx);

        float radius = skillData.radius + _weapon.AdditionalSkillRange;

        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, radius, results, monsterLayer);
        for (int i = 0; i < numColliders; i++)
        {
            Monster monster = results[i].GetComponent<Monster>();
            Ability(monster, _weapon);

            float damage = _weapon.Damage * (skillData.damageMultiplier + _weapon.AdditionalSkillDamage);
            monster.TakeDamage(damage, _weapon.Penetrate, EDamageType.SKILL);
                
        }
        yield return duration;
        pool.ReturnToPool(skillData.id, gameObject);
    }

    //피격당한 몬스터에게 데미지 외에 추가적인 효과를 부여
    public abstract void Ability(Monster _monster, Weapon _equipWeapon);
}
