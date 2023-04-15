using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Event Channel/Void", order = 1)]
public class VoidEventChanelSO : ScriptableObject
{
    public UnityAction OnRequested;

    public void RaiseEvent()
    {
        if (OnRequested != null)
            OnRequested.Invoke();
    }
}
