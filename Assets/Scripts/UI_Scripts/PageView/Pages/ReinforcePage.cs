using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class ReinforcePage : MyUIBase
{
    [Header("SFX")]
    [SerializeField] private AudioClip succeessSFX;
    [SerializeField] private AudioClip failSFX;


    [Space]
    [Header("Other Component References")]
    [SerializeField] private Hero hero = null;
    [SerializeField] private FloatingMessage floatingMessage = null;

    private Stone stone = null;
    public Stone Stone { set { stone = value; SetDataIntoComponents(); } }

    #region Cache
    private StringBuilder sb = null;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        if (!hero) hero = FindObjectOfType<Hero>();
        if (!floatingMessage) floatingMessage = FindObjectOfType<FloatingMessage>();
    }

    public void Reinforce()
    {
        if (!stone) return;
        if (stone.tryCount >= 5) return;

        AudioClip clip = stone.Reinforce() ? succeessSFX : failSFX;

        SoundManager.Instance.PlayClip(clip);
        SetDataIntoComponents();
    }

    public void Apply()
    {
        if (!stone) return;

        if (stone.tryCount < 5)
        {
            UIContext.GetUIByPath("Dialog/CheckApply", (result) =>
            {
                MyDialog dialog = result as MyDialog;
                dialog.OnYesEvent.AddListener(() => {
                    hero.UpgradeWeapon(stone.data.type, stone.GetCurrentIncrease());
                    Addressables.ReleaseInstance(stone.gameObject);
                    floatingMessage.ShowFloatingMessage(GetFloatingMessageContent(stone.data.type, stone.GetCurrentIncrease()));
                    stone = null;
                    SetDataIntoComponents();
                });
                dialog.Show();
            });
        }
        else
        {
            hero.UpgradeWeapon(stone.data.type, stone.GetCurrentIncrease());
            Addressables.ReleaseInstance(stone.gameObject);
            floatingMessage.ShowFloatingMessage(GetFloatingMessageContent(stone.data.type, stone.GetCurrentIncrease()));
            stone = null;
            SetDataIntoComponents();
        }
    }

    private void SetDataIntoComponents()
    {
        string statType = (stone == null) ? "" : Weapon.GetWeaponStatToUIString(stone.data.type);
        UIContext.SetTextComponent("Upgrade/StatType", statType);

        StringBuilder sb = new StringBuilder();
        sb.Append("Upgrade/ResultStone");
        for (int i = 0; i < 5; i++)
        {
            sb.Append(i.ToString());
            UIContext.GetUIByPath(sb.ToString(), (result)=> {
                Image image = result as Image;
                
                if (stone == null) image.color = new Color(0.5f, 0.5f, 0.5f);
                else image.color = stone.GetResultColor(i);
            });

            sb.Remove(sb.Length - 1, 1);
        }

        string before = (stone == null) ? "" : stone.GetCurrentIncrease().ToString();
        UIContext.SetTextComponent("Upgrade/Reinforce/BeforeValue", before);

        string next = (stone == null || stone.tryCount == 5) ? "" : stone.GetNextIncrease().ToString();
        UIContext.SetTextComponent("Upgrade/Reinforce/AfterValue", next);

        string probability = (stone == null || stone.tryCount == 5) ? "" : stone.probability.ToString();
        UIContext.SetTextComponent("Upgrade/Probability", probability);
    }

    private string GetFloatingMessageContent(EWeaponStat _stat, float _value)
    {
        if (sb == null) sb = new StringBuilder();

        sb.Clear();
        sb.Append(Weapon.GetWeaponStatToUIString(_stat));
        sb.Append(" +");
        sb.Append(_value.ToString());

        return sb.ToString();
    }
}
