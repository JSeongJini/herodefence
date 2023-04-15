using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    [Header("Panel References")]
    [SerializeField] private GameObject preventClickOther = null;
    [SerializeField] private PanelSelectWeapon selectWeaponPanel = null;
    [SerializeField] private PanelSelectBuff selectBuffPanel = null;
    [SerializeField] private PanelSelectSkill selectSkillPanel = null;

    [Space] [Header("Other Component References")]
    private GameSessionManager gameSessionManager = null;

    public void Awake()
    {
        if (!preventClickOther) preventClickOther = transform.Find("PreventClickOther").gameObject;
        if (!selectWeaponPanel) selectWeaponPanel = transform.Find("Panel_SelectWeapon").GetComponent<PanelSelectWeapon>();
        if (!selectBuffPanel) selectBuffPanel = transform.Find("Panel_SelectBuff").GetComponent<PanelSelectBuff>();
        if (!selectSkillPanel) selectSkillPanel = transform.Find("Panel_SelectSkill").GetComponent<PanelSelectSkill>();
        if (!gameSessionManager) gameSessionManager = FindObjectOfType<GameSessionManager>();
    }


    public void OpenSelectWeaponPanel()
    {
        selectWeaponPanel.Show();

#if UNITY_EDITOR
#else
        preventClickOther.SetActive(true);
#endif
    }

    public void OpenSelectPanel()
    {
        if (gameSessionManager.round % 10 == 6)
        {
            selectSkillPanel.Show();
        }
        else
        {
            selectBuffPanel.Show();
        }
#if UNITY_EDITOR
#else
        preventClickOther.SetActive(true);
#endif
    }
}
