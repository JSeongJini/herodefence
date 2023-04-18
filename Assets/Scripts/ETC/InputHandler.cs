using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    [Header("--Other Component References--")]
    [SerializeField] private MonsterManager monsterManager = null;
    [SerializeField] private Camera cam = null;

    #region Cache
    private LayerMask monsterLayer;
    #endregion


    private void Awake()
    {
        if (!monsterManager) monsterManager = FindObjectOfType<MonsterManager>();
        if (!cam) cam = Camera.main;

        monsterLayer = 1 << 7;  //7 : Monster Layer
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            PickMonster();
        }
    }

    private void PickMonster()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, monsterLayer)){
            Monster monster = hit.transform.GetComponent<Monster>();
            if (monster)
            {
                monsterManager.SetSpecificTarget(monster);
            }
        }
    }
}
