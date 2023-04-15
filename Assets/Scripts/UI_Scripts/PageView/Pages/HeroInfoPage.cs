using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class HeroInfoPage : MyUIBase, IObserver
{
    [SerializeField] Hero hero = null;

    #region for Cache
    private string[] path = { "/Weapon", "/Upgrade", "/Buff", "/Total" };
    private string prefix = "HeroInfo/";
    #endregion

    protected override void Awake()
    {
        base.Awake();
        if (!hero) hero = FindObjectOfType<Hero>();
    }

    private void OnEnable()
    {
        hero.Attach(this);
    }

    private void OnDisable()
    {
        hero.Detach(this);
    }

    public void Notify(Component subject)
    {
        SetDataIntoComponents();
    }

    private void SetDataIntoComponents()
    {
        Weapon weapon = hero.equipWeapon;

        StringBuilder sb = new StringBuilder();
        
        foreach(EWeaponStat stat in typeof(EWeaponStat).GetEnumValues())
        {
            sb.Clear(); sb.Append(prefix);  // "HeroInfo/"
            sb.Append(stat.ToString());     // "HeroInfo/STAT_TYPE"
            
            sb.Append(path[0]);             // "HeroInfo/STAT_TYPE/Weapon"
            UIContext.SetTextComponent(sb.ToString(),
                weapon.GetBaseStatToString(stat));

            sb.Replace(path[0], path[1]); //"HeroInfo/STAT_TYPE/Upgrade
            UIContext.SetTextComponent(sb.ToString(),
                "+" + weapon.GetUpgradedStatToString(stat));

            sb.Replace(path[1], path[2]); //"HeroInfo/STAT_TYPE/Buff
            UIContext.SetTextComponent(sb.ToString(),
                "x" + weapon.GetBuffStatToString(stat));

            sb.Replace(path[2], path[3]); //"HeroInfo/STAT_TYPE/Total
            UIContext.SetTextComponent(sb.ToString(),
                weapon.GetTotalStatToString(stat));
        }
    }
}
