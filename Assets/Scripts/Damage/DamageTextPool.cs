using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DamageTextPool : MonoBehaviour
{
    [SerializeField] private string prefabKey;
    [SerializeField] private int maxPoolSize = 100;
    [SerializeField] private int stackDefaultCapacity = 100;
    [SerializeField] private Transform canvasTransform = null;

    [Space]
    [Header("Styles")]
    [SerializeField] private Dictionary<EDamageType, DamageTextStyle> styles
        = new Dictionary<EDamageType, DamageTextStyle>();

    private IObjectPool<DamageText> pool;
    public IObjectPool<DamageText> Pool
    {
        get
        {
            if(pool == null)
            {
                pool = new ObjectPool<DamageText>(
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
    private GameObject textPrefab;
    private Camera cam;
    #endregion


    public void Awake()
    {
        if (!canvasTransform) canvasTransform = GameObject.Find("Damage_Text_Canavs").transform;
        if (!cam) cam = Camera.main;


        Addressables.LoadAssetAsync<GameObject>(prefabKey).Completed += (op) =>
        {
            if (op.Status != AsyncOperationStatus.Succeeded)
                return;

            textPrefab = op.Result;
        };

        foreach(EDamageType type in typeof(EDamageType).GetEnumValues())
        {
            Addressables.LoadAssetAsync<DamageTextStyle>("DamageTextStyle_" + type.ToString()).Completed += (op) =>
            {
                if (op.Status != AsyncOperationStatus.Succeeded)
                    return;

                styles.Add(type, op.Result);
            };
        }
    }

    private DamageText CreatedPooledItem()
    {
        DamageText text = Instantiate(textPrefab).GetComponent<DamageText>();
        text.transform.SetParent(canvasTransform);
        text.pool = pool;

        return text;
    }

    private void OnTakeFromPool(DamageText _text)
    {
        _text.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(DamageText _text)
    {
        _text.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(DamageText _text)
    {
        Destroy(_text.gameObject);
    }

    public void Spawn(Vector3 _spawnPoint, float _damage, EDamageType _type)
    {
        DamageText text = Pool.Get();

        Vector3 point = cam.WorldToScreenPoint(_spawnPoint);
        Vector3 randomness = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0f);

        text.SetDamageText(point + randomness, _damage, styles[_type]);
    }
}
