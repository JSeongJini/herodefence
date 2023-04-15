using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoneReinforceSlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [SerializeField] private ReinforcePage page;
    [SerializeField] private Transform stoneList;

    private Stone currentStone;

    public virtual void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 1)
        {
            GameObject dropped = eventData.pointerDrag;
            Stone stone = dropped.GetComponent<Stone>();
            if (stone)
            {
                stone.parentAfterDrag = transform;
                currentStone = stone;
                page.Stone = stone;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentStone)
        {
            currentStone.transform.SetParent(stoneList);
            currentStone.GetComponent<Image>().raycastTarget = true;
            page.Stone = null;
            currentStone = null;
        }
    }
}
