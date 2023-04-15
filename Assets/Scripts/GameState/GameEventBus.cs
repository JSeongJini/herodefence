using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EGameState
{
    READY, START, RESTART, ROUND, ROUNDEND, FAIL, SUCCEED
}

public class GameEventBus
{
    private static readonly IDictionary<EGameState, UnityEvent>
        Events = new Dictionary<EGameState, UnityEvent>();

    public static void Subscribe(EGameState gameEvent, UnityAction listener)
    {
        UnityEvent thisEvent;

        if(Events.TryGetValue(gameEvent, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Events.Add(gameEvent, thisEvent);
        }
    }

    public static void Unsubscribe(EGameState gameEvent, UnityAction listener)
    {
        UnityEvent thisEvent;

        if(Events.TryGetValue(gameEvent, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void Publish(EGameState gameEvent)
    {
        UnityEvent thisEvent;
        if (Events.TryGetValue(gameEvent, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}
