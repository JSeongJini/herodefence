using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private readonly ArrayList observers = new ArrayList();

    public void Attach(IObserver _observer)
    {
        observers.Add(_observer);
    }

    public void Detach(IObserver _observer)
    {
        observers.Remove(_observer);
    }

    public void NotifyObservers()
    {
        foreach(IObserver observer in observers)
        {
            observer.Notify(this);
        }
    }
}
