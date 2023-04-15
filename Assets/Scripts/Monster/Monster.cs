using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

[RequireComponent(typeof(Animator), typeof(HPBar), typeof(MonsterStat))]
[RequireComponent(typeof(BoxCollider))]
public class Monster : MonoBehaviour 
{
    [Header("Own Component References")]
    [SerializeField] private HPBar hpBar = null;
    [SerializeField] private Animator animator = null;
    public MonsterStat monsterStat = null;

    [Space]
    [Header("Other Component References")]
    public DamageTextPool damageTextPool = null;
    public HitEffectPool hitEffectPool = null;

    public delegate void OnDieDelegate(Monster _monster);
    [HideInInspector] public OnDieDelegate OnDie;

    public float aliveTime = 0f;

    #region Unity MonoBehaviour Funtions
    private void Awake()
    {
        if (!hpBar) hpBar = GetComponent<HPBar>();
        if (!animator) animator = GetComponent<Animator>();
        if (!monsterStat) monsterStat = GetComponent<MonsterStat>();
    }

    void Start()
    {
        hpBar.target = transform;
    }

    void Update()
    {
        transform.RotateAround(Vector3.zero, -Vector3.up, monsterStat.Speed * Time.deltaTime);
        aliveTime += Time.deltaTime;
    }
#endregion
    
    public void TakeDamage(float _damage, float _penetrating = 0f, EDamageType _type = EDamageType.NORMAL)
    {
        //(1.1^(������ ���� - �����) - 1f) ��ŭ ������ ����
        float finalArmor = monsterStat.Armor - _penetrating;
        float reduceRatio = Mathf.Pow(1.1f, Mathf.Abs(finalArmor)) - 1f;

        int takenDamage;
        if (finalArmor >= 0)
            takenDamage = Mathf.FloorToInt(_damage * (1f - reduceRatio));
        else
            takenDamage = Mathf.FloorToInt(_damage * (1f + reduceRatio));

        if (takenDamage <= 0) takenDamage = 0;

        monsterStat.currentHp -= takenDamage;

        if(monsterStat.currentHp <= 0)
        {
            monsterStat.currentHp = 0;
            Die();
        }

        //HPBar ������Ʈ
        hpBar.SetHPBar(monsterStat.currentHp, monsterStat.MaxHp);

        //������ �ؽ�Ʈ&�� ����Ʈ ����
        damageTextPool.Spawn(transform.position, takenDamage, _type);
        hitEffectPool.Spawn(transform.position);
    }

    public void Die()
    {
        OnDie.Invoke(this);
        Addressables.ReleaseInstance(gameObject);
    }
}
