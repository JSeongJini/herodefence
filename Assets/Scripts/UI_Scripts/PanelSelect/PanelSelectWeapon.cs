using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PanelSelectWeapon : PanelSelectBase<WeaponData, WeaponDataTable>
{
    [SerializeField] private GameObject[] detailViews = null;

    protected override void Start()
    {
        base.Start();
        table.GetThreeRandomData(randomDatas);
        SetDataIntoComponents();
        HideDetail();
    }

    public void SeeDetail()
    {
        for (int i = 0; i < detailViews.Length; i++)
            detailViews[i].SetActive(true);
    }

    public void HideDetail()
    {
        for (int i = 0; i < detailViews.Length; i++)
            detailViews[i].SetActive(false);
    }

    protected override void Select(int _index)
    {
        hero.SetHeroWeapon(randomDatas[_index]);
        gameSessionManager.SetState(EGameState.START);
        
        Hide();
    }

    protected override void SetDataIntoComponents()
    {
        if (sb == null) sb = new StringBuilder();

        for (int i = 0; i < 3; i++)
        {
            sb.Clear();
            sb.Append("Weapon");
            sb.Append(i);
            sb.Append("/Image");
            UIContext.SetImageComponent(sb.ToString(), randomDatas[i].sprite);
        }

        for (int i = 0; i < 3; i++)
        {
            sb.Clear();
            sb.Append("Weapon");
            sb.Append(i);
            sb.Append("/Name");
            UIContext.SetTextComponent(sb.ToString(), randomDatas[i].weaponName);
        }

        for (int i = 0; i < 3; i++)
        {
            sb.Clear();
            sb.Append("Weapon");
            sb.Append(i);
            sb.Append("/Detail/Name");
            UIContext.SetTextComponent(sb.ToString(), randomDatas[i].weaponName);
        }

        for (int i = 0; i < 3; i++)
        {
            sb.Clear();
            sb.Append("Weapon");
            sb.Append(i);
            sb.Append("/Description");

            UIContext.SetTextComponent(sb.ToString(), randomDatas[i].description);
        }

        for (int i = 0; i < 3; i++)
        {
            sb.Clear();
            sb.Append("Weapon");
            sb.Append(i);
            sb.Append("/Damage");

            UIContext.SetTextComponent(sb.ToString(), randomDatas[i].damage.ToString());
        }

        for (int i = 0; i < 3; i++)
        {
            sb.Clear();
            sb.Append("Weapon");
            sb.Append(i);
            sb.Append("/Rate");
          
            UIContext.SetTextComponent(sb.ToString(), string.Format("{0:0.#}", randomDatas[i].rate));
        }

        for (int i = 0; i < 3; i++)
        {
            sb.Clear();
            sb.Append("Weapon");
            sb.Append(i);
            sb.Append("/Strike");

            UIContext.SetTextComponent(sb.ToString(), randomDatas[i].strike.ToString());
        }

        for (int i = 0; i < 3; i++)
        {
            sb.Clear();
            sb.Append("Weapon");
            sb.Append(i);
            sb.Append("/Penetrate");
            UIContext.SetTextComponent(sb.ToString(), randomDatas[i].penetrate.ToString());
        }

        for (int i = 0; i < 3; i++)
        {
            sb.Clear();
            sb.Append("Weapon");
            sb.Append(i);
            sb.Append("/Critical");
            UIContext.SetTextComponent(sb.ToString(), string.Format("{0:0.#}", randomDatas[i].critical));
        }

        for (int i = 0; i < 3; i++)
        {
            sb.Clear();
            sb.Append("Weapon");
            sb.Append(i);
            sb.Append("/CriticalMultiplier");
            UIContext.SetTextComponent(sb.ToString(), string.Format("{0:0.#}", randomDatas[i].criticalMul));
        }

        for (int i = 0; i < 3; i++)
        {
            sb.Clear();
            sb.Append("Weapon");
            sb.Append(i);
            sb.Append("/AdditionalSkillChance");
            UIContext.SetTextComponent(sb.ToString(), string.Format("{0:0.#}", randomDatas[i].additionalSkillChance));
        }

        for (int i = 0; i < 3; i++)
        {
            sb.Clear();
            sb.Append("Weapon");
            sb.Append(i);
            sb.Append("/AdditionalSkillDamage");
            UIContext.SetTextComponent(sb.ToString(), string.Format("{0:0.#}", randomDatas[i].additionalSkillDamage));
        }

        for (int i = 0; i < 3; i++)
        {
            sb.Clear();
            sb.Append("Weapon");
            sb.Append(i);
            sb.Append("/AdditionalSkillRange");
            UIContext.SetTextComponent(sb.ToString(), string.Format("{0:0.#}", randomDatas[i].additionalSkillRange));
        }
    }
}
