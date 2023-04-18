using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    public float targetWidth;
    public float targetHeight;

    private void Awake()
    {
        Camera cam = Camera.main;

        float sizeToTargetWidth = targetWidth / (2f * cam.aspect);
        float sizeToTargetHeight = targetHeight / 2f;


        float size = (sizeToTargetWidth >= sizeToTargetHeight) ? sizeToTargetWidth : sizeToTargetHeight;

        cam.orthographicSize = size;
    }
}
