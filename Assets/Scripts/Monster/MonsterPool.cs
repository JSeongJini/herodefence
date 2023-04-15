using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Pool;

public class MonsterPool : MonoBehaviour
{
    [SerializeField] private string key;
    [SerializeField] private int maxPoolSize = 50;
    [SerializeField] private int stackDefaultCapacity = 50;

    #region Cache
    private GameObject monsterPrefab = null;
    private MonsterManager monsterManager = null;
    #endregion

    public void Awake()
    {
        Addressables.LoadAssetAsync<GameObject>(key).Completed += (op) =>
        {
            if (op.Status != AsyncOperationStatus.Succeeded)
                return;

            monsterPrefab = op.Result;
        };
    }

    public void Start()
    {
        if (!monsterManager) monsterManager = FindObjectOfType<MonsterManager>();
    }


    private IObjectPool<Monster> pool;
    public IObjectPool<Monster> Pool
    {
        get
        {
            if(pool == null)
            {
                pool = new ObjectPool<Monster>(
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

    private Monster CreatedPooledItem()
    {
        if (monsterPrefab == null)
            GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = Vector3.zero;

        Monster monster = Instantiate(monsterPrefab).GetComponent<Monster>();
        monster.transform.SetParent(transform);

        return monster;
    }

    private void OnTakeFromPool(Monster _monster)
    {
        _monster.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(Monster _monster)
    {
        monsterManager.RemoveMonster(_monster);
        _monster.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(Monster _monster)
    {
        monsterManager.RemoveMonster(_monster);
        Destroy(_monster.gameObject);
    }

    public void Spawn(Vector3 _wayPoint)
    {
        Monster monster = Pool.Get();
        monster.transform.SetPositionAndRotation(_wayPoint, monsterPrefab.transform.rotation);
        monsterManager.AddMonseter(monster);
    }
}
