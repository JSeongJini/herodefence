using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [Header("--Event Channel--")]
    [SerializeField] private IntEventChanelSO monsterCountChangeEvent = null;
    [SerializeField] private VoidEventChanelSO tartgetChangeEvent = null;


    [Header("--Other Component References--")]
    [SerializeField] private Hero hero;

    [Space]
    [Header("--Outline Materials--")]
    [SerializeField] private Material outlineMaterial = null;
    
    [Space]
    public Monster currentTarget = null;
    public Monster boss = null;

    private List<Monster> monsterList = new List<Monster>();


    #region Unity MonoBehaviour Funtions
    public void Awake()
    {
        if (!hero) hero = FindObjectOfType<Hero>();
    }

    public void Update()
    {
        if (currentTarget == null)
            AutoTargeting();
    }

    public void OnEnable()
    {
        GameEventBus.Subscribe(EGameState.FAIL, DestroyAllMonsters);
    }

    public void OnDisable()
    {
        GameEventBus.Unsubscribe(EGameState.FAIL, DestroyAllMonsters);
    }
    #endregion


    public void AutoTargeting()
    {
        if (monsterList.Count > 0)
        {
            currentTarget = monsterList[Random.Range(0, monsterList.Count)];
            MakeOutlineToTarget();

            tartgetChangeEvent.RaiseEvent();
        }
    }

    public void SetSpecificTarget(Monster _monster)
    {
        if (currentTarget) RemoveOutlineFromTarget();

        currentTarget = _monster;
        MakeOutlineToTarget();

        tartgetChangeEvent.RaiseEvent();
    }

    public Monster GetMonster()
    {
        if (monsterList.Count > 0)
            return monsterList[0];
        return null;
    }

    public void AddMonseter(Monster _monster)
    {
        monsterList.Add(_monster);
        if(monsterList.Count == 1)
        {
            hero.StartAttack();
        }

        monsterCountChangeEvent.RaiseEvent(monsterList.Count);
    }

    public void RemoveMonster(Monster _monster)
    {
        monsterList.Remove(_monster);



        if (monsterList.Count == 0)
            hero.StopAttack();

        monsterCountChangeEvent.RaiseEvent(monsterList.Count);
    }

    public bool IsBossAlive()
    {
        return (boss != null);
    }

    public int GetMonsterCount()
    {
        return monsterList.Count;
    }

    public void DestroyAllMonsters()
    {
        for(int i = 0; i < monsterList.Count; i++)
        {
            Destroy(monsterList[i].gameObject);
        }
        monsterList.Clear();
    }

    #region Private Method
    private void MakeOutlineToTarget()
    {
        SkinnedMeshRenderer renderer = currentTarget.GetComponentInChildren<SkinnedMeshRenderer>();
        List<Material> materials = renderer.sharedMaterials.ToList();
        materials.Add(outlineMaterial);
        renderer.materials = materials.ToArray();
    }

    private void RemoveOutlineFromTarget()
    {
        SkinnedMeshRenderer renderer = currentTarget.GetComponentInChildren<SkinnedMeshRenderer>();
        List<Material> materials = renderer.sharedMaterials.ToList();
        materials.Remove(outlineMaterial);
        renderer.materials = materials.ToArray();
    }

    #endregion
}