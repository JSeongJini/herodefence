using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private IntEventChanelSO coinChangeEvent;

    [SerializeField] private int coin = 0;
    public int Coin { get { return coin; } }

    public void EarnCoin(int _value)
    {
        coin += _value;

        coinChangeEvent.RaiseEvent(coin);
    }

    public bool UseCoin(int _value)
    {
        if (CheckHasCoin(_value))
        {
            EarnCoin(-_value);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckHasCoin(int _value)
    {
        return coin >= _value;
    }

    public int RandomCoin(int round)
    {
        int min = 20;
        int max = 50;
        return Random.Range(min, max);
    }
}
