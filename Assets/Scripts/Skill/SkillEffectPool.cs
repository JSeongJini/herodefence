using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SkillEffectPool : MonoBehaviour
{
    private IDictionary<int, GameObject> prefabs = new Dictionary<int, GameObject>();
    private IDictionary<int, Queue<GameObject>> pool = new Dictionary<int, Queue<GameObject>>();

    protected delegate void OnInstantiate(GameObject _go);
    protected OnInstantiate onInstantiate;


    public GameObject GetSkill(SkillData _data)
    {
        Queue<GameObject> queue;

        if(pool.TryGetValue(_data.id, out queue))
        {
            if(queue.Count == 0)
            {
                return InstantiateObject(_data);
            }
            else
            {
                GameObject go = queue.Dequeue();
                go.SetActive(true);
                return go;
            }
        }
        else
        {
            //,,,,,
            return null;
        }
    }


    public void AddNewSkill(SkillData _data)
    {
        pool.Add(_data.id, new Queue<GameObject>());
        prefabs.Add(_data.id, _data.prefab);
    }

    public void ReturnToPool(int _id, GameObject _go)
    {
        _go.SetActive(false);
        pool[_id].Enqueue(_go);
    }


    private GameObject InstantiateObject(SkillData _data) 
    {
        GameObject go = Instantiate(prefabs[_data.id]);
        go.transform.SetParent(transform);

        SkillBase skill = go.GetComponent<SkillBase>();
        skill.pool = this;
        skill.skillData = _data;

        return go;
    }
}
