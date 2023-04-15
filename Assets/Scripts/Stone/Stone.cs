using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stone : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image image;
    [SerializeField] private RectTransform rectTransform;

    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public int[] results = { 0, 0, 0, 0, 0 };
    [HideInInspector] public float probability;

    public StoneData data;
    public int tryCount = 0;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if(data != null) probability = data.startProbability;
    }
    

    public bool Reinforce()
    {
        float random = Random.Range(0f, 100f);
        if (random <= probability)
        {
            results[tryCount++] = 1;
            probability -= 10f;
            return true;
        }
        else
        {
            results[tryCount++] = -1;
            return false;
        }
    }


    public float GetCurrentIncrease()
    {
        float result = 0f;
        int succeded = 0;
        for(int i = 0; i < tryCount; i++)
        {
            if(results[i] == 1)
                result += data.increase[succeded++];
        }

        return result;
    }

    public float GetNextIncrease()
    {
        int succeded = 0;
        for (int i = 0; i < 5; i++)
            if (results[i] == 1) succeded++;

        return GetCurrentIncrease() + data.increase[succeded];
    }

    public Color GetResultColor(int index)
    {
        if (results[index] == 1)    //success
            return new Color(1f, 0.7f, 0f);
        else if (results[index] == -1) //fail
            return new Color(1f, 0f, 0f);
        else
            return new Color(0.5f, 0.5f, 0.5f);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;

        if (parentAfterDrag.CompareTag("ReinforceSlot")) {
            image.raycastTarget = false;
        }

        rectTransform.anchoredPosition = Vector2.zero;
    }
}
