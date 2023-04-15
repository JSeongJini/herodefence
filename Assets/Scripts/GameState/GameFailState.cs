using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFailState : IState<GameSessionManager>
{
    public void Enter(GameSessionManager _t)
    {
        _t.GameEnd();
    }

    public void Update(GameSessionManager _t)
    {
    }

    public void End(GameSessionManager _t)
    {
    }
}
