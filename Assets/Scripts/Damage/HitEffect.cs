using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HitEffect : MonoBehaviour
{
    public IObjectPool<HitEffect> pool;

    private void OnDisable()
    {
        pool.Release(this);
    }
}
