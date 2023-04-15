using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro = null;

    [SerializeField] private AnimationCurve blinkCurve = null;

    private Color color;
    
    private float time = 0f;
    private float duration;

    private void Awake()
    {
        if (!textMeshPro) textMeshPro = GetComponent<TextMeshProUGUI>();
        color = textMeshPro.color;

        if (blinkCurve != null)
        {
            duration = blinkCurve.keys[blinkCurve.length - 1].time;
        }
    }

    private void Update()
    {
        color.a = blinkCurve.Evaluate(time);
        textMeshPro.color = color;

        time += Time.unscaledDeltaTime;

        if (time >= duration) time = 0f;
    }
}
