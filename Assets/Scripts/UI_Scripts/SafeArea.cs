using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;

    public void Awake()
    {
        if (!rectTransform) rectTransform = GetComponent<RectTransform>();

        Rect SafeArea = Screen.safeArea;

        Vector2 anchorMin = SafeArea.position;
        Vector2 anchorMax = SafeArea.position + SafeArea.size;
        
        anchorMin.x = rectTransform.anchorMin.x;
        anchorMax.x = rectTransform.anchorMax.x;

        anchorMin.y /= Screen.height;
        anchorMax.y /= Screen.height;

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }
}
