using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Event Channel/Int", order = 2)]
public class IntEventChanelSO : ScriptableObject
{
    public UnityAction<int> OnRequested;

    public void RaiseEvent(int _value)
    {
        if (OnRequested != null)
            OnRequested.Invoke(_value);
    }
}
