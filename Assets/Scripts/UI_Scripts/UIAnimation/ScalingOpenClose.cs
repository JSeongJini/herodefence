using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingOpenClose : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform = null;
    [SerializeField] private AnimationCurve scaleUpCurve;
    [SerializeField] private AnimationCurve scaleDownCurve;

    private float scaleUpTime;
    private float scaleDownTime;


    private void Awake()
    {
        if (!rectTransform) rectTransform = GetComponent<RectTransform>();

        if (scaleUpCurve != null) scaleUpTime = scaleUpCurve.keys[scaleUpCurve.length - 1].time;
        if (scaleDownCurve != null) scaleDownTime = scaleDownCurve.keys[scaleDownCurve.length - 1].time;
    }

    public IEnumerator ScaleUpCoroutine()
    {
        float time = 0f;
        while(time < scaleUpTime)
        {
            float scale = scaleUpCurve.Evaluate(time);
            rectTransform.localScale = new Vector3(scale, scale, 1f);
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        rectTransform.localScale = Vector3.one;
    }

    public IEnumerator ScaleDownCoroutine()
    {
        float time = 0f;
        while (time < scaleDownTime)
        {
            float scale = scaleDownCurve.Evaluate(time);
            rectTransform.localScale = new Vector3(scale, scale, 1f);
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        rectTransform.localScale = Vector3.zero;
    }
}
