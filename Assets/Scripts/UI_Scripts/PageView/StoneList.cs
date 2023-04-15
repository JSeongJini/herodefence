using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoneList : MonoBehaviour, IDropHandler
{
    public float space = 10f;
    private int childCount = 0;

    public void Awake()
    {
        Alignmnet();
        childCount = transform.childCount;
    }

    private void Update()
    {
        if(childCount != transform.childCount)
        {
            childCount = transform.childCount;
            Alignmnet();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        Stone stone = dropped.GetComponent<Stone>();
        if (stone)
        {
            stone.parentAfterDrag = transform;
            Alignmnet();
        }
    }

    private void Alignmnet()
    {
        float startX = -445f;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector3(startX, 0f, 0f);
            startX += 100f + space;
        }
    }
}
