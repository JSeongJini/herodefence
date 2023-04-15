using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using TMPro;

public class DamageText : MonoBehaviour
{
    [Header("Own Component Refernece")]
    [SerializeField] private TextMeshProUGUI textMesh = null;
    [SerializeField] private RectTransform rectTransform = null;


    public IObjectPool<DamageText> pool;
    private float time = 0;

    private Vector3 origin;
    private DamageTextStyle style;

    public void Awake()
    {
        if (!textMesh) textMesh = GetComponent<TextMeshProUGUI>();
        if (!rectTransform) rectTransform = GetComponent<RectTransform>();
    }

    public void Update()
    {
        if (style == null) return;

        Color color = style.color;
        color.a = style.opacityCurve.Evaluate(time);
        textMesh.color = color;

        rectTransform.localScale = Vector3.one * style.scaleCurve.Evaluate(time);

        rectTransform.position = origin + Vector3.up * style.heightCurve.Evaluate(time);

        time += Time.unscaledDeltaTime;
        
        if (time >= 1f)
            ReturnToPool();
    }

    public void OnDisable()
    {
        ResetText();
    }

    public void ReturnToPool()
    {
        pool.Release(this);
    }

    private void ResetText()
    {
        transform.localPosition = Vector3.zero;
        time = 0f;
    }

    public void SetDamageText(Vector3 _position, float _damge, DamageTextStyle _style)
    {
        rectTransform.position = _position;
        origin = _position;
        textMesh.text = ((int)_damge).ToString();
        style = _style;
    }
}
