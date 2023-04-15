using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private IntEventChanelSO scoreChangeEvent;

    [SerializeField] private int score = 0;

    [Space]
    [Header("Score Scale")]
    [SerializeField] private int remainMonsterScale = 50;
    [SerializeField] private int remainCoinScale = 10;

    public int Score { get { return score; } }

    public void AddScore(int _value)
    {
        score += _value;

        scoreChangeEvent.RaiseEvent(score);
    }

    public int GetRemainMonsterScore(int _remainMonsterCount, int _round)
    {
        return (50 - _remainMonsterCount) *_round * remainMonsterScale;
    }

    public int GetRemainCoinScore(int _remainCoinCount)
    {
        return _remainCoinCount * remainCoinScale;
    }

    public int GetTotalScore(int _remainMonsterCount, int _remainCoinCount, int _round)
    {
        return score + GetRemainMonsterScore(_remainMonsterCount, _round) + GetRemainCoinScore(_remainCoinCount);
    }
}
