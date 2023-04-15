using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataTable : DataTable<SkillData>
{
    public void RemoveOptions(SkillData _data)
    {
        datas.Remove(_data);
    }
}
