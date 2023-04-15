using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UpgradedWeapon
{
    #region Weapon Stat
    public float damage;
    public float rate;
    public float strike;
    public float penetrate;
    public float critical;
    public float criticalMul;
    public float additionalSkillChance;
    public float additionalSkillDamage;
    public float additionalSkillRange;
    #endregion

    #region Upgrade Level
    public readonly IDictionary<EWeaponStat, int> lv;
    #endregion

    #region constructor
    public UpgradedWeapon()
    {
        //Initialize every field to zero
        damage = 0;
        rate = 0f;
        strike = 0;
        penetrate = 0;
        critical = 0f;
        criticalMul = 0f;
        additionalSkillChance = 0f;
        additionalSkillDamage = 0f;
        additionalSkillRange = 0f;

        lv = new Dictionary<EWeaponStat, int>();
        foreach(EWeaponStat stat in typeof(EWeaponStat).GetEnumValues())
        {
            lv.Add(stat, 0);
        }
    }
    #endregion

    public void Upgrade(EWeaponStat _stat, float _value)
    {
        lv[_stat]++;

        switch (_stat)
        {
            case EWeaponStat.DAMAGE:
                damage += _value;
                break;
            case EWeaponStat.RATE:
                rate += _value;
                break;
            case EWeaponStat.STRIKE:
                strike += _value;
                break;
            case EWeaponStat.PENETRATE:
                penetrate +=_value;
                break;
            case EWeaponStat.CRITICAL:
                critical += _value;
                break;
            case EWeaponStat.CRITICALMUL:
                criticalMul += _value;
                break;
            case EWeaponStat.ADDITIONALSKILLCHANCE:
                additionalSkillChance += _value;
                break;
            case EWeaponStat.ADDITIONALSKILLDAMAGE:
                additionalSkillDamage += _value;
                break;
            case EWeaponStat.ADDITIONALSKILLRANGE:
                additionalSkillRange += _value;
                break;
            default:
                break;
        }
    }

    public int GetUpgradeCost(EWeaponStat _stat)
    {
        switch (_stat)
        {
            case EWeaponStat.DAMAGE:
                return 1;
            case EWeaponStat.RATE:
                return 1;
            case EWeaponStat.STRIKE:
                return 1;
            case EWeaponStat.PENETRATE:
                return 1;
            case EWeaponStat.CRITICAL:
                return 1;
            case EWeaponStat.CRITICALMUL:
                return 1;
            case EWeaponStat.ADDITIONALSKILLCHANCE:
                return 1;
            case EWeaponStat.ADDITIONALSKILLDAMAGE:
                return 1;
            case EWeaponStat.ADDITIONALSKILLRANGE:
                return 1;
            default:
                return 1;
        }
    }
}
