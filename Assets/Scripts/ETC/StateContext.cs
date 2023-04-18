using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateContext<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T owner = null;
    public IState<T> currentState = null;

    public void Awake()
    {
        if (!owner) owner = GetComponent<T>(); 
    }

    public void Update()
    {
        currentState.Update(owner);
    }

    public void Transition(IState<T> _state)
    {
        if (_state == currentState) return;

        if(currentState != null)
          currentState.End(owner);

        currentState = _state;

        currentState.Enter(owner);
    }
}
