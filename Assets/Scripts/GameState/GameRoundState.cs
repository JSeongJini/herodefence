using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoundState : IState<GameSessionManager>
{
    private MonsterManager monsterManager = null;

    public GameRoundState(MonsterManager _monsterManager)
    {
        monsterManager = _monsterManager;
    }

    public void Enter(GameSessionManager _gameSessionManager)
    {
        float roundTime;
        if (IsBossRound(_gameSessionManager.round)) roundTime = _gameSessionManager.maxRoundTime;
        else
        {
            // 10�� + (Round/5) X 5��
            roundTime = _gameSessionManager.minRoundTime + (_gameSessionManager.round / 5) * _gameSessionManager.roundTimeIncrease;
            if (roundTime > _gameSessionManager.maxRoundTime) roundTime = _gameSessionManager.maxRoundTime;
        }

        _gameSessionManager.remainTime = roundTime;
    }

    public void Update(GameSessionManager _gameSessionManager)
    {
        //���� ���� ���������� �����ϸ� ���潺 ����
        if(monsterManager.GetMonsterCount() == _gameSessionManager.monsterCountToFail)
        {
            _gameSessionManager.SetState(EGameState.FAIL);
            return;
        }

        _gameSessionManager.remainTime -= Time.deltaTime;
        if (_gameSessionManager.remainTime <= 0f)
        {
            _gameSessionManager.SetState(EGameState.ROUNDEND);
        }
    }

    public void End(GameSessionManager _gameSessionManager)
    {
    }

    private bool IsBossRound(int _round)
    {
        return (_round % 5 == 0);
    }
}
