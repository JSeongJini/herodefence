using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoundEndState : IState<GameSessionManager>
{
    MonsterManager monsterManager = null;
    PanelController panelController = null;

    public GameRoundEndState(MonsterManager _monsterManger, PanelController _panelController)
    {
        monsterManager = _monsterManger;
        panelController = _panelController;
    }

    public void Enter(GameSessionManager _gameSessionManager)
    {
        if(_gameSessionManager.round == _gameSessionManager.lastRound)
        {
            _gameSessionManager.SetState(EGameState.SUCCEED);
            return;
        }

        if (IsBossRound(_gameSessionManager.round))
        {
            if (monsterManager.IsBossAlive())
            {
                _gameSessionManager.SetState(EGameState.FAIL);
                return;
            }
        }

        _gameSessionManager.round++;
        _gameSessionManager.isRounding = false;

        Time.timeScale = 0f;

        panelController.OpenSelectPanel();
    }

    public void Update(GameSessionManager _gameSessionManager)
    {
    }

    public void End(GameSessionManager _gameSessionManager)
    {
        Time.timeScale = 1f;
    }

    private bool IsBossRound(int _round)
    {
        return (_round % 5 == 0);
    }
}
