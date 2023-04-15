using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartState : IState<GameSessionManager>
{
    public void Enter(GameSessionManager _gameSessionManager)
    {
        Time.timeScale = 1f;
        _gameSessionManager.isRecordTime = true;
        _gameSessionManager.StartCoroutine(RoundStartCoroutine(_gameSessionManager));
    }

    public void Update(GameSessionManager _gameSessionManager)
    {
    }

    public void End(GameSessionManager _gameSessionManager)
    {

    }

    private IEnumerator RoundStartCoroutine(GameSessionManager _gameSessionManager)
    {
        yield return new WaitForSeconds(2f);
        _gameSessionManager.SetState(EGameState.ROUND);
    }
}
