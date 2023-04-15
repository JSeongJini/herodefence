using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PanelSelectBuff : PanelSelectBase<BuffData, BuffDataTable>
{
    protected override void Select(int _index)
    {
        hero.BuffWeapon(randomDatas[_index]);
        gameSessionManager.SetState(EGameState.ROUND);

        Hide();
    }

    protected override void SetDataIntoComponents()
    {
        if (sb == null) sb = new StringBuilder();

        for (int i = 0; i < 3; i++)
        {
            sb.Clear();
            sb.Append("Buff");
            sb.Append(i);
            sb.Append("/Image");
            UIContext.SetImageComponent(sb.ToString(), randomDatas[i].sprite);
        }

        for (int i = 0; i < 3; i++)
        {
            sb.Clear();
            sb.Append("Buff");
            sb.Append(i);
            sb.Append("/Description");

            UIContext.SetTextComponent(sb.ToString(), randomDatas[i].description);
        }
    }

    
}
