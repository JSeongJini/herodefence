using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : MonoBehaviour
{
    public Hero hero = null;
    public BuffData damageUp;
    public BuffData rateUp;
    public BuffData strikeUp;
    public BuffData penetrateUp;

    private void Start()
    {
        if (hero == null) hero = FindObjectOfType<Hero>();
    }


    public void OnGUI()
    {
        if (GUILayout.Button("DamageUp")){
            hero.BuffWeapon(damageUp);
        }
        if (GUILayout.Button("rateUp"))
        {
            hero.BuffWeapon(rateUp);
        }
        if (GUILayout.Button("strikeUp"))
        {
            hero.BuffWeapon(strikeUp);
        }
        if (GUILayout.Button("penetrateUp"))
        {
            hero.BuffWeapon(penetrateUp);
        }
    }
}
