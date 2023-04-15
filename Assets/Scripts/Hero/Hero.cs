using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Subject 
{
    [Header("--Own Component Reference--")]
    [SerializeField] private Animator animator = null;
    [SerializeField] private WeaponSetter weaponSetter = null;
    public HeroStateContext context = null;

    [Space]
    [Header("--Other Component Reference--")]
    [SerializeField] private MonsterManager monsterManager = null;
    [SerializeField] private SkillEffectPool skillEffectPoool = null;


    public Weapon equipWeapon = null;

    #region State
    private HeroIdleState idleState = null;
    private HeroAttackState attackState = null;
    private HeroFailState failState = null;
    #endregion

    #region Cache

    #endregion


    #region Unity Monobehaviour Funtions
    public void Awake()
    {
        if (!weaponSetter) weaponSetter = GetComponent<WeaponSetter>(); 
        if (!context) context = GetComponent<HeroStateContext>();
        if (!animator) animator = GetComponent<Animator>();
        if (!monsterManager) monsterManager = FindObjectOfType<MonsterManager>();
        if (!skillEffectPoool) skillEffectPoool = FindObjectOfType<SkillEffectPool>();
    }

    public void Start()
    {
        idleState = new HeroIdleState(this);
        attackState = new HeroAttackState(this, monsterManager, skillEffectPoool);
        failState = new HeroFailState(this);

        context.Transition(idleState);

        SoundManager.Instance.LoadHitEffect();
    }

    public void OnEnable()
    {
        GameEventBus.Subscribe(EGameState.FAIL, Fail);
    }

    public void OnDisable()
    {
        GameEventBus.Unsubscribe(EGameState.FAIL, Fail);
    }
    #endregion

    #region public Method
    public void SetHeroWeapon(WeaponData _weaponData)
    {
        if (weaponSetter)
        {
            weaponSetter.SetHeroWeapon(_weaponData);
            weaponSetter = null;
            NotifyObservers();
        }
    }

    public void BuffWeapon(BuffData _buffData)
    {
        equipWeapon.BuffWeapon(_buffData);
        NotifyObservers();
    }

    public void UpgradeWeapon(EWeaponStat _stat, float _value)
    {
        equipWeapon.UpgradeWeapon(_stat, _value);
        NotifyObservers();
    }

    public void AddNewSkill(SkillData _skill)
    {
        attackState.AddNewSkill(_skill);
        NotifyObservers();
    }

    public void StartAttack()
    {
        context.Transition(attackState);
    }

    public void StopAttack()
    {
        context.Transition(idleState);
    }

    public void Fail()
    {
        context.Transition(failState);
    }
    #endregion
}
