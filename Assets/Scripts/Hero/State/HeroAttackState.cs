using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroAttackState : IState<Hero>
{
    private Animator animator = null;
    private Hero hero;

    private float attackRate;
    private float nextAttack = 0f;
    private float previousAttackRate;

    public List<SkillData> ownSkills = new List<SkillData>();

    #region Cache
    private Monster target = null;
    private MonsterManager monsterManager = null;
    private WaitForSeconds strikeDelay = new WaitForSeconds(0.1f);
    private WaitForSeconds animDelay;
    private int hashAttack = Animator.StringToHash("Attack");
    private float attackAnimationDuration = 0f;
    private SkillEffectPool skillEffectPool = null;
    private IEnumerator attackCoroutine = null;
    #endregion

    public HeroAttackState(Hero _hero, MonsterManager _monsterManger, SkillEffectPool _skillEffectPool)
    {
        if (!hero) hero = _hero;
        if (!animator) animator = _hero.GetComponent<Animator>();
        if (!monsterManager) monsterManager = _monsterManger;
        if (!skillEffectPool) skillEffectPool = _skillEffectPool;
    }

    public void Enter(Hero _hero)
    {
        previousAttackRate = _hero.equipWeapon.Rate;
        attackRate = 1f / (hero.equipWeapon.Rate * 0.1f);
        
        if (attackAnimationDuration == 0f)
        {
            foreach(var clip in animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name.Contains("Attack"))
                    attackAnimationDuration += clip.length;
            }
        }
        animator.speed = attackAnimationDuration * (previousAttackRate * 0.1f);
        animDelay = new WaitForSeconds((attackAnimationDuration / animator.speed) * 0.9f);
    }


    public void Update(Hero _hero)
    {
        if(previousAttackRate != _hero.equipWeapon.Rate)
        {
            previousAttackRate = _hero.equipWeapon.Rate;
            animator.speed = attackAnimationDuration * (previousAttackRate * 0.1f);
            animDelay = new WaitForSeconds((attackAnimationDuration / animator.speed) * 0.9f);
            attackRate = 1f / (hero.equipWeapon.Rate * 0.1f);
        }

        Attack();       
    }

    public void End(Hero _hero)
    {
        _hero.StopCoroutine(attackCoroutine);
    }

    public void AddNewSkill(SkillData _skill)
    {
        ownSkills.Add(_skill);
    }


    private void Attack()
    {
        if (Time.time > nextAttack)
        {
            target = monsterManager.currentTarget;
            if (target != null)
            {
                nextAttack = Time.time + attackRate;
                attackCoroutine = AttackCoroutine();
                hero.StartCoroutine(attackCoroutine);
            }
        }
    }

    private IEnumerator AttackCoroutine()
    {
        hero.transform.LookAt(target.transform);
        animator.SetTrigger(hashAttack);
        yield return animDelay;

        UseSkill();

        bool isCritical = CheckProbability(hero.equipWeapon.Critical);
        float damage = CalcDamage(isCritical);
        EDamageType type = isCritical ? EDamageType.CRITICAL : EDamageType.NORMAL;

        for (int i = 0; i < hero.equipWeapon.Strike; i++)
        {
            if (target != null)
            {
                SoundManager.Instance.PlayHitEffect();
                target.TakeDamage(damage, hero.equipWeapon.Penetrate, type);
            }
            yield return strikeDelay;
        }
    }
   

    private void UseSkill()
    {
        if (ownSkills.Count == 0) return;

        for(int i = 0; i < ownSkills.Count; i++)
        {
            if (CheckProbability(ownSkills[i].chance + hero.equipWeapon.AdditionalSkillChance))
            {
                if (target != null)
                {
                    GameObject skillGo = skillEffectPool.GetSkill(ownSkills[i]);
                    skillGo.transform.position = target.transform.position;
                    skillGo.GetComponent<SkillBase>().ActiveSkill(hero.equipWeapon);
                }
            }
        }
    }


    private float CalcDamage(bool _IsCritical)
    {
        float damage = hero.equipWeapon.Damage;

        if (_IsCritical)
        {
            damage *= (1f + hero.equipWeapon.CriticalMul);
        }

        return damage;
    }

    private bool CheckProbability(float _probability)
    {
        return Random.Range(0f, 100f) <= _probability;
    }
}
