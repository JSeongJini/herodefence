using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
public class DynamicCanvasScalerMode : MonoBehaviour
{
    [SerializeField] private CanvasScaler scaler;

    private void Awake()
    {
        if (!scaler) scaler = GetComponent<CanvasScaler>();
    }

    private void Start()
    {
        if (!Camera.main) return;

        if(Camera.main.aspect > (9f / 16f))
        {
            scaler.matchWidthOrHeight = 1f;
        }
        else
        {
            scaler.matchWidthOrHeight = 0f;
        }
    }
}
