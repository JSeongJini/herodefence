using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReadyState : IState<GameSessionManager>
{
    private PanelController panelController = null;

    public GameReadyState(PanelController _panelController)
    {
        panelController = _panelController;
    }

    public void Enter(GameSessionManager _gameSessionManager)
    {
        panelController.OpenSelectWeaponPanel();
    }

    public void Update(GameSessionManager _gameSessionManager)
    {
    }

    public void End(GameSessionManager _gameSessionManager)
    {
    }
}
