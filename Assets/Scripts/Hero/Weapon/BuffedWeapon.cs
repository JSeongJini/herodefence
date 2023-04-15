using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffedWeapon
{
    public  IDictionary<EWeaponStat, float> stats;

    public BuffedWeapon()
    {
        stats = new Dictionary<EWeaponStat, float>();

        foreach(EWeaponStat stat in typeof(EWeaponStat).GetEnumValues())
        {
            stats.Add(stat, 1f);
        }
    }

    public void BuffWeapon(BuffData _buffData)
    {
        _buffData.Buff(this);
    }
}
