using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWeaponStat
{
    DAMAGE, RATE, STRIKE,
    PENETRATE, CRITICAL, CRITICALMUL,
    ADDITIONALSKILLCHANCE,
    ADDITIONALSKILLDAMAGE,
    ADDITIONALSKILLRANGE
}


public class Weapon
{
    private readonly WeaponData baseWeapon;
    private readonly BuffedWeapon buffedWeapon;
    private readonly UpgradedWeapon upgradedWeapon;

    #region Final Weapon Stat
    private float damage;
    private float rate;
    private float strike;
    private float penetrate;
    private float critical;
    private float criticalMul;
    private float additionalSkillChance;
    private float additionalSkillDamage;
    private float additionalSkillRange;
    #endregion

    public float Damage { get { if (needUpdate) { UpdateFianlStat(); } return damage; } }
    public float Rate { get { if (needUpdate) { UpdateFianlStat(); } return rate; } }
    public float Strike { get { if (needUpdate) { UpdateFianlStat(); } return strike; } }
    public float Penetrate { get { if (needUpdate) { UpdateFianlStat(); } return penetrate; } }
    public float Critical { get { if (needUpdate) { UpdateFianlStat(); } return critical; } }
    public float CriticalMul { get { if (needUpdate) { UpdateFianlStat(); } return criticalMul; } }
    public float AdditionalSkillChance { get { if (needUpdate) { UpdateFianlStat(); } return additionalSkillChance; } }
    public float AdditionalSkillDamage { get { if (needUpdate) { UpdateFianlStat(); } return additionalSkillDamage; } }
    public float AdditionalSkillRange { get { if (needUpdate) { UpdateFianlStat(); } return additionalSkillRange; } }

    private bool needUpdate;

    public Weapon(WeaponData _data)
    {
        baseWeapon = _data;
        upgradedWeapon = new UpgradedWeapon();
        buffedWeapon = new BuffedWeapon();

        damage = baseWeapon.damage;
        rate = baseWeapon.rate;
        strike = baseWeapon.strike;
        penetrate = baseWeapon.penetrate;
        critical = baseWeapon.critical;
        criticalMul = baseWeapon.criticalMul;
        additionalSkillChance = baseWeapon.additionalSkillChance;
        additionalSkillDamage = baseWeapon.additionalSkillDamage;
        additionalSkillRange = baseWeapon.additionalSkillRange;

        needUpdate = false;
    }

    public void BuffWeapon(BuffData _buffData)
    {
        buffedWeapon.BuffWeapon(_buffData);
        needUpdate = true;
    }

    public void UpgradeWeapon(EWeaponStat _type, float _value)
    {
        upgradedWeapon.Upgrade(_type, _value);
        needUpdate = true;
    }

    public void UpdateFianlStat()
    {
        damage = Mathf.FloorToInt((baseWeapon.damage + upgradedWeapon.damage) * buffedWeapon.stats[EWeaponStat.DAMAGE]);
        rate = (baseWeapon.rate + upgradedWeapon.rate) * buffedWeapon.stats[EWeaponStat.RATE];
        strike = Mathf.FloorToInt((baseWeapon.strike + upgradedWeapon.strike) * buffedWeapon.stats[EWeaponStat.STRIKE]);
        penetrate = (baseWeapon.penetrate + upgradedWeapon.penetrate) * buffedWeapon.stats[EWeaponStat.PENETRATE];
        critical = (baseWeapon.critical + upgradedWeapon.critical) * buffedWeapon.stats[EWeaponStat.CRITICAL];
        criticalMul = (baseWeapon.criticalMul + upgradedWeapon.criticalMul) * buffedWeapon.stats[EWeaponStat.CRITICALMUL];
        additionalSkillChance = (baseWeapon.additionalSkillChance + upgradedWeapon.additionalSkillChance) * buffedWeapon.stats[EWeaponStat.ADDITIONALSKILLCHANCE];
        additionalSkillDamage = (baseWeapon.additionalSkillDamage + upgradedWeapon.additionalSkillDamage) * buffedWeapon.stats[EWeaponStat.ADDITIONALSKILLDAMAGE];
        additionalSkillRange = (baseWeapon.additionalSkillRange + upgradedWeapon.additionalSkillRange) * buffedWeapon.stats[EWeaponStat.ADDITIONALSKILLRANGE];

        needUpdate = false;
    }

    public string GetBaseStatToString(EWeaponStat _type)
    {
        switch (_type)
        {
            case EWeaponStat.DAMAGE:
                return baseWeapon.damage.ToString("F1");
            case EWeaponStat.RATE:
                return baseWeapon.rate.ToString("F1");
            case EWeaponStat.STRIKE:
                return baseWeapon.strike.ToString("F1");
            case EWeaponStat.PENETRATE:
                return baseWeapon.penetrate.ToString("F1");
            case EWeaponStat.CRITICAL:
                return baseWeapon.critical.ToString("F1");
            case EWeaponStat.CRITICALMUL:
                return baseWeapon.criticalMul.ToString("F1");
            case EWeaponStat.ADDITIONALSKILLCHANCE:
                return baseWeapon.additionalSkillChance.ToString("F1");
            case EWeaponStat.ADDITIONALSKILLDAMAGE:
                return baseWeapon.additionalSkillDamage.ToString("F1");
            case EWeaponStat.ADDITIONALSKILLRANGE:
                return baseWeapon.additionalSkillRange.ToString("F1");
            default:
                return "";
        }
    }

    public string GetUpgradedStatToString(EWeaponStat _type)
    {
        switch (_type)
        {
            case EWeaponStat.DAMAGE:
                return upgradedWeapon.damage.ToString("F1");
            case EWeaponStat.RATE:
                return upgradedWeapon.rate.ToString("F1");
            case EWeaponStat.STRIKE:
                return upgradedWeapon.strike.ToString("F1");
            case EWeaponStat.PENETRATE:
                return upgradedWeapon.penetrate.ToString("F1");
            case EWeaponStat.CRITICAL:
                return upgradedWeapon.critical.ToString("F1");
            case EWeaponStat.CRITICALMUL:
                return upgradedWeapon.criticalMul.ToString("F1");
            case EWeaponStat.ADDITIONALSKILLCHANCE:
                return upgradedWeapon.additionalSkillChance.ToString("F1");
            case EWeaponStat.ADDITIONALSKILLDAMAGE:
                return upgradedWeapon.additionalSkillDamage.ToString("F1");
            case EWeaponStat.ADDITIONALSKILLRANGE:
                return upgradedWeapon.additionalSkillRange.ToString("F1");
            default:
                return "";
        }
    }

    public int GetUpgaradLevel(EWeaponStat _type)
    {
        return upgradedWeapon.lv[_type];
    }

    public int GetUpgradeCost(EWeaponStat _type)
    {
        return upgradedWeapon.GetUpgradeCost(_type);
    }

    public string GetBuffStatToString(EWeaponStat _type)
    {
        return buffedWeapon.stats[_type].ToString("F1");
    }

    public string GetTotalStatToString(EWeaponStat _type)
    {
        switch (_type)
        {
            case EWeaponStat.DAMAGE:
                return Damage.ToString("F1");
            case EWeaponStat.RATE:
                return Rate.ToString("F1");
            case EWeaponStat.STRIKE:
                return Strike.ToString("F1");
            case EWeaponStat.PENETRATE:
                return Penetrate.ToString("F1");
            case EWeaponStat.CRITICAL:
                return Critical.ToString("F1");
            case EWeaponStat.CRITICALMUL:
                return CriticalMul.ToString("F1");
            case EWeaponStat.ADDITIONALSKILLCHANCE:
                return AdditionalSkillChance.ToString("F1");
            case EWeaponStat.ADDITIONALSKILLDAMAGE:
                return AdditionalSkillDamage.ToString("F1");
            case EWeaponStat.ADDITIONALSKILLRANGE:
                return AdditionalSkillRange.ToString("F1");
            default:
                return "";
        }
    }

    public static string GetWeaponStatToUIString(EWeaponStat _stat)
    {
        switch (_stat)
        {
            case EWeaponStat.DAMAGE:
                return "공격력";
            case EWeaponStat.RATE:
                return "공격속도";
            case EWeaponStat.STRIKE:
                return "타격횟수";
            case EWeaponStat.PENETRATE:
                return "관통력";
            case EWeaponStat.CRITICAL:
                return "치명타확률";
            case EWeaponStat.CRITICALMUL:
                return "치명타배율";
            case EWeaponStat.ADDITIONALSKILLCHANCE:
                return "추가스킬발동확률";
            case EWeaponStat.ADDITIONALSKILLDAMAGE:
                return "추가스킬공격력";
            case EWeaponStat.ADDITIONALSKILLRANGE:
                return "추가스킬범위";
            default:
                return "";
        }
    }
}
