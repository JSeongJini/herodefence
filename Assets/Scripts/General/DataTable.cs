using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTable<T> : MonoBehaviour
{
    public List<T> datas = null;
    public List<T> Datas
    {
        get { return datas; }
    }

    public void GetThreeRandomData(T[] result)
    {
        int max = datas.Count;
        if (max < 3) return;

        int first = Random.Range(0, max);
        int second = Random.Range(0, max);
        int third = Random.Range(0, max);

        while (true)
        {
            if (first == second)
            {
                second = Random.Range(0, max);
            }
            else if (first == third)
            {
                third = Random.Range(0, max);
            }
            else if (second == third)
            {
                third = Random.Range(0, max);
            }
            else
            {
                break;
            }
        }

        result[0] = datas[first];
        result[1] = datas[second];
        result[2] = datas[third];
    }
}
