using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class MyUIBase : MonoBehaviour
{
    [SerializeField] protected RectTransform rectTransform = null;
    [SerializeField] protected Vector3 showPos = Vector3.zero;

    private Vector3 hidePos;
    protected bool isShow = false;


    protected virtual void Awake()
    {
        if (!rectTransform) rectTransform = GetComponent<RectTransform>();

        hidePos = rectTransform.anchoredPosition;
    }


    public virtual void Show() {
        rectTransform.anchoredPosition = showPos;
        isShow = true;
    }

    public virtual void Hide()
    {
        rectTransform.anchoredPosition = hidePos;
        isShow = false;
    }
}
