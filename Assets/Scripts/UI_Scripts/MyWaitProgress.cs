using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyWaitProgress : MyUIBase
{
    [SerializeField] private Image image;
    [SerializeField] private float speed = 0.5f;

    private void Start()
    {
        if (!image) image = GetComponent<Image>();
    }

    private void Update()
    {
        if (!isShow) return;

        image.fillAmount += Time.unscaledDeltaTime * speed;
        if (image.fillAmount >= 1f || image.fillAmount <= 0f)
        {
            speed *= -1f;
            image.fillClockwise = !image.fillClockwise;
        }
    }
}
