using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSucceedState : IState<GameSessionManager>
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
