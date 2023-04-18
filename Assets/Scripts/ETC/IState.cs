using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T> 
{
    public abstract void Enter(T _t);

    public abstract void Update(T _t);

    public abstract void End(T _t);
}
