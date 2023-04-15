using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfoPanel : MonoBehaviour
{
    [Header("--Subscribe Event Channel--")]
    [SerializeField] private VoidEventChanelSO targetChangeEvent = null;

    [Space]
    [SerializeField] private GameObject[] infos = null;
    [SerializeField] private MonsterManager monsterManager = null;

    private float previousHp = 0f;
    private float previousArmor = 0f;
    private bool isShow = false;

    private MonsterStat stat = null;

    protected void Awake()
    {
        if (!monsterManager) monsterManager = FindObjectOfType<MonsterManager>();
    }

    private void Start()
    {
        OnOff(false);
    }

    private void OnEnable()
    {
        targetChangeEvent.OnRequested += UpdateMonsterInfo;
    }

    private void Update()
    {
        if (!isShow) return;

        if(monsterManager.currentTarget == null)
        {
            OnOff(false);
            return;
        }

        if(previousHp != monsterManager.currentTarget.monsterStat.currentHp)
        {
            previousHp = monsterManager.currentTarget.monsterStat.currentHp;
            UIContext.SetTextComponent("Info/Monster/CurrentHP",
                previousHp.ToString());
        }

        if(previousArmor != monsterManager.currentTarget.monsterStat.Armor)
        {
            previousArmor = monsterManager.currentTarget.monsterStat.Armor;
            UIContext.SetTextComponent("Info/Monster/Armor",
                previousArmor.ToString());
        }
    }


    public void UpdateMonsterInfo()
    {
        if (!isShow && monsterManager.currentTarget != null)
        {
            OnOff(true);
            return;
        }else if (!isShow)
        {
            return;
        }

        SetDataIntoComponent();
    }

    public void OnOff(bool _isShow)
    {
        foreach(GameObject go in infos)
        {
            go.SetActive(_isShow);
        }
        isShow = _isShow;

        if (isShow)
        {
            SetDataIntoComponent();
        }
    } 

    private void SetDataIntoComponent()
    {
        stat = monsterManager.currentTarget.monsterStat;

        UIContext.SetTextComponent("Info/Monster/Name", stat.MonsterName);
        UIContext.SetTextComponent("Info/Monster/MaxHP", stat.MaxHp.ToString());
        UIContext.SetTextComponent("Info/Monster/Armor", stat.Armor.ToString());
    }
}
