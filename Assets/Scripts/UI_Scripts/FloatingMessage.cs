using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingMessage : MonoBehaviour
{
    [SerializeField] private AnimationCurve floatCurve;
    [SerializeField] private Text floatText;

    private float duration;
    private Vector2 origin;

    private Coroutine coroutine;

    private void Awake()
    {
        if (!floatText) floatText = transform.Find("FloatText").GetComponent<Text>();
        origin = floatText.rectTransform.anchoredPosition;
    }

    public void ShowFloatingMessage(string _content, float _durtiation = 1f)
    {
        floatText.text = _content;
        duration = _durtiation;

        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(FloatTextCoroutine());
    }

    private IEnumerator FloatTextCoroutine()
    {
        floatText.enabled = true;
        float time = 0f;

        while(time <= duration)
        {
            floatText.rectTransform.anchoredPosition = origin + Vector2.up * floatCurve.Evaluate(time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        floatText.enabled = false;
    }
}
