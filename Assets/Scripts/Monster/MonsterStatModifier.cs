using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EStatModifier { ADD, MUL }

public class MonsterStatModifier
{
    public EStatModifier type;
    public float speed;
    public float armor;
    public float duration;

    public MonsterStatModifier(EStatModifier _type, float _speed, float _armor, float _duration)
    {
        type = _type;
        speed = _speed;
        armor = _armor;
        duration = _duration;
    }
}
