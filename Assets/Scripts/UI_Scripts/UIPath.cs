using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPath: MonoBehaviour
{
    public string path;
    public Component component;

    private void Awake()
    {
        if(path != null)
        {
            UIContext.Subscribe(path, this);
        }
    }

    private void OnDestroy()
    {
        UIContext.Unsubscribe(path);
    }
}
