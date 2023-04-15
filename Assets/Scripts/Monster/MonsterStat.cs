using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    [SerializeField] private MonsterStatData baseStat;
    
    private List<MonsterStatModifier> addModifiers = new List<MonsterStatModifier>();
    private List<MonsterStatModifier> mulModifiers = new List<MonsterStatModifier>();

    public int level = 0;
    public int currentHp = 0;
    private int maxHp = 0;
    private float speed = 0f;
    private int armor = 0;

    public int MaxHp { get { return maxHp; } }
    public float Speed { get { return speed; } }
    public int Armor { get { return armor; } }
    public string MonsterName { get { return baseStat.monsterName; } }

    private void Awake()
    {
        if (baseStat)
        {
            level = baseStat.level;
            maxHp = baseStat.MaxHp;
            currentHp = maxHp;
            speed = baseStat.Speed;
            armor = baseStat.Armor;
        }
    }

    public void AddModifier(MonsterStatModifier _modifier)
    {
        if (_modifier.type == EStatModifier.ADD)
            addModifiers.Add(_modifier);
        else if (_modifier.type == EStatModifier.MUL)
            mulModifiers.Add(_modifier);

        UpdateStat();
        StartCoroutine(WaitDurationCoroutine(_modifier));
    }

    public void RemoveModifier(MonsterStatModifier _modifier)
    {
        if (_modifier.type == EStatModifier.ADD)
            addModifiers.Remove(_modifier);
        else if (_modifier.type == EStatModifier.MUL)
            mulModifiers.Remove(_modifier);

        UpdateStat();
    }


    public void UpdateStat()
    {
        //Cacluate Add Modifiers;
        float platSpeed = 0f;
        float platArmor = 0f;
        for(int i = 0; i < addModifiers.Count; i++)
        {
            platSpeed += addModifiers[i].speed;
            platArmor += addModifiers[i].armor;
        }

        //Caclueate Mul Modifiers;
        float mulSpeed = 1f;
        float mulArmor = 1f;
        for (int i = 0; i < mulModifiers.Count; i++)
        {
            mulSpeed *= (1f - mulModifiers[i].speed);
            mulArmor *= (1f - mulModifiers[i].armor);
        }

        speed = (baseStat.Speed + platSpeed) * mulSpeed;
        if (speed < 0f) speed = 0f;

        armor = Mathf.FloorToInt((baseStat.Armor + platArmor) * mulArmor);
    }

    private IEnumerator WaitDurationCoroutine(MonsterStatModifier _modifier)
    {
        yield return new WaitForSeconds(_modifier.duration);
        RemoveModifier(_modifier);
    }

}
