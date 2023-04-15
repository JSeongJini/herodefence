using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PanelSelectSkill : PanelSelectBase<SkillData, SkillDataTable>
{
    [SerializeField] private GameObject[] detailViews = null;
    [SerializeField] private SkillEffectPool skillEffectPool = null;
    

    protected override void Awake()
    {
        base.Awake();
        if (!skillEffectPool) skillEffectPool = FindObjectOfType<SkillEffectPool>();

    }

    protected override void Start()
    {
        base.Start();
        HideDetail();
    }

    protected override void Select(int _index)
    {
        skillEffectPool.AddNewSkill(randomDatas[_index]);
        hero.AddNewSkill(randomDatas[_index]);
        gameSessionManager.SetState(EGameState.ROUND);
        table.RemoveOptions(randomDatas[_index]);
        Hide();
    }

    protected override void SetDataIntoComponents()
    {
        if(sb == null ) sb = new StringBuilder();

        for (int i = 0; i < 3; i++)
        {
            sb.Clear();
            sb.Append("Skill");
            sb.Append(i);
            sb.Append("/Image");
            UIContext.SetImageComponent(sb.ToString(), randomDatas[i].sprite);
        }

        for (int i = 0; i < 3; i++)
        {
            sb.Clear();
            sb.Append("Skill");
            sb.Append(i);
            sb.Append("/SkillName");
            UIContext.SetTextComponent(sb.ToString(), randomDatas[i].skillName);
        }

        for (int i = 0; i < 3; i++)
        {
            sb.Clear();
            sb.Append("Skill");
            sb.Append(i);
            sb.Append("/Detail/SkillName");
            UIContext.SetTextComponent(sb.ToString(), randomDatas[i].skillName);
        }

        for (int i = 0; i < 3; i++)
        {
            sb.Clear();
            sb.Append("Skill");
            sb.Append(i);
            sb.Append("/Detail/Description");
            UIContext.SetTextComponent(sb.ToString(), randomDatas[i].description);
        }
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
}
