using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSetter : MonoBehaviour
{
    [SerializeField] private Hero hero = null;

    [Header("--Weapon Holder Transform--")]
    [SerializeField] private Transform LeftGrip = null;
    [SerializeField] private Transform RightGrip = null;

    public void Awake()
    {
        if (!hero) hero = FindObjectOfType<Hero>();
    }

    public void SetHeroWeapon(WeaponData _weaponData)
    {
        hero.equipWeapon = new Weapon(_weaponData);

        if (_weaponData.prefab)
        {
            if (_weaponData.gripType == EWeaponGripType.LEFT || _weaponData.gripType == EWeaponGripType.BOTH)
                Instantiate(_weaponData.prefab, LeftGrip);
            if (_weaponData.gripType == EWeaponGripType.RIGHT || _weaponData.gripType == EWeaponGripType.BOTH)
                Instantiate(_weaponData.prefab, RightGrip);
        }

        hero.GetComponent<Animator>().runtimeAnimatorController = _weaponData.controller;

        Destroy(this);
    }
}
