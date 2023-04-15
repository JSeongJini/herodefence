using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectWeapon : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text weaponName;
    //TODO : °ø°Ý·Â....


    public void SetWeaponData(WeaponData _weaponData)
    {
        weaponName.text = _weaponData.weaponName;
    }
}

