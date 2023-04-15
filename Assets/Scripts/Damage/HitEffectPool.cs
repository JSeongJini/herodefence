using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class HitEffectPool : MonoBehaviour
{
    [SerializeField] private string prefabKey;
    [SerializeField] private int maxPoolSize = 50;
    [SerializeField] private int stackDefaultCapacity = 50;

    private IObjectPool<HitEffect> pool;
    public IObjectPool<HitEffect> Pool
    {
        get
        {
            if (pool == null)
            {
                pool = new ObjectPool<HitEffect>(
                    CreatedPooledItem,
                    OnTakeFromPool,
                    OnReturnedToPool,
                    OnDestroyPoolObject,
                    true,
                    stackDefaultCapacity,
                    maxPoolSize);
            }
            return pool;
        }
    }

    #region Cache
    private GameObject effectPrefab = null;
    #endregion

    public void Awake()
    {
        Addressables.LoadAssetAsync<GameObject>(prefabKey).Completed += (op) =>
        {
            if (op.Status != AsyncOperationStatus.Succeeded)
                return;

            effectPrefab = op.Result;
        };
    }

    private HitEffect CreatedPooledItem()
    {
        HitEffect effect = Instantiate(effectPrefab).GetComponent<HitEffect>();
        effect.transform.SetParent(transform);
        effect.pool = pool;

        return effect;
    }

    private void OnTakeFromPool(HitEffect _effect)
    {
        _effect.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(HitEffect _effect)
    {
        _effect.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(HitEffect _effect)
    {
        Destroy(_effect.gameObject);
    }

    public void Spawn(Vector3 _spawnPoint)
    {
        HitEffect effect = Pool.Get();

        Vector3 randomness = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        effect.transform.position = _spawnPoint + randomness;
    }
}
