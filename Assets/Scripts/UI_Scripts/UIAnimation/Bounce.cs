using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bounce : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform = null;
    [SerializeField] private AnimationCurve curve = null;
    public UnityEvent OnAnimationEnd = new UnityEvent();
    public bool playOnAwake = true;

    private float bounceTime = 0f;
    private Vector3 originScale;

    private void Awake()
    {
        if (!rectTransform) rectTransform = GetComponent<RectTransform>();
        originScale = rectTransform.localScale;

        if (curve != null) bounceTime = curve.keys[curve.length - 1].time;
    }

    private void OnEnable()
    {
        if(playOnAwake)
            StartCoroutine(BounceCoroutine());
    }

    public void StartBounce()
    {
        StartCoroutine(BounceCoroutine());
    }

    private IEnumerator BounceCoroutine()
    {
        yield return null;
        float time = 0f;

        while (time <= bounceTime) {
            float scale = curve.Evaluate(time);
            rectTransform.localScale = new Vector3(scale, scale, 1f);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        rectTransform.localScale = originScale;
        
        OnAnimationEnd.Invoke();
    }
}
