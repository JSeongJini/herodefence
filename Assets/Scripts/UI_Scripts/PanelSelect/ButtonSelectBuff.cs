using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonSelectBuff : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text buffName;
    [SerializeField] private Text explain;
    //TODO : ���ݷ�....���,


    public void SetWeaponBuff(BuffData _weaponBuff)
    {
        buffName.text = _weaponBuff.name;
    }
}
