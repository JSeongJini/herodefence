using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Text;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("--About Spawn--")]
    [SerializeField] private Vector3 wayPoint = new Vector3(0f, 0f, 15f);
    private int spawnCountPerRound = 5;

    [Space][Header("--Other Component References--")]
    [SerializeField] private MonsterManager monsterManager = null;
    [SerializeField] private CoinManager coinManager = null;
    [SerializeField] private ScoreManager scoreManager = null;
    [SerializeField] private GameSessionManager gameSessionManager = null;
    [SerializeField] private DamageTextPool damageTextPool = null;
    [SerializeField] private HitEffectPool hitEffectPool = null;

    #region Cache
    private WaitForSeconds spawnPeriod = new WaitForSeconds(2f);
    private StringBuilder stringBuilder = new StringBuilder();
    private readonly string keyPrefix = "Monsters/Level_";
    private string key;
    #endregion



    void Awake()
    {
        if (!gameSessionManager) gameSessionManager = FindObjectOfType<GameSessionManager>();
        if (!monsterManager) monsterManager = FindObjectOfType<MonsterManager>();
        if (!coinManager) coinManager = FindObjectOfType<CoinManager>();
        if (!scoreManager) scoreManager = FindObjectOfType<ScoreManager>();
        if (!damageTextPool) damageTextPool = FindObjectOfType<DamageTextPool>();
        if (!hitEffectPool) hitEffectPool = FindObjectOfType<HitEffectPool>();
    }

    public void OnEnable()
    {
        GameEventBus.Subscribe(EGameState.READY, SetupMonsterSpec);
        GameEventBus.Subscribe(EGameState.ROUNDEND, SetupMonsterSpec);
        GameEventBus.Subscribe(EGameState.ROUND, SpawnWave);
        GameEventBus.Subscribe(EGameState.FAIL, StopSpawn);
    }

    public void OnDisable()
    {
        GameEventBus.Unsubscribe(EGameState.READY, SetupMonsterSpec);
        GameEventBus.Unsubscribe(EGameState.ROUNDEND, SetupMonsterSpec);
        GameEventBus.Unsubscribe(EGameState.ROUND, SpawnWave);
    }


    public void SetupMonsterSpec()
    {
        stringBuilder.Clear();
        stringBuilder.Append(keyPrefix);
        stringBuilder.Append(gameSessionManager.round);

        key = stringBuilder.ToString();
    }

    public void SpawnWave()
    {
        if (gameSessionManager.round % 5 == 0)
        {   //보스라운드
            SpawnBoss();
        }
        else
        {   //일반라운드
            spawnCountPerRound = (int)(gameSessionManager.remainTime / 2f);
            StartCoroutine(SpawnWaveCoroutine());
        }
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
    }

    private void SpawnBoss() {
        Addressables.InstantiateAsync(key, wayPoint, Quaternion.Euler(0f, -90f, 0f)).Completed += (op) =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                Monster monsterComp = op.Result.GetComponent<Monster>();
                MonsterSetup(monsterComp, true);
            }
        };
        UIContext.GetUIByPath("BossAlert", (result)=>
        {
            BossAlert alert = result as BossAlert;
            alert.Show();
        });
    }

    private void MonsterSetup(Monster _monster, bool _isBoss = false)
    {
        int earnCoin = _isBoss ? _monster.monsterStat.level * 100 : coinManager.RandomCoin(_monster.monsterStat.level);

        _monster.OnDie += monsterManager.RemoveMonster;
        _monster.OnDie += (monster) => { coinManager.EarnCoin(earnCoin); };
        _monster.OnDie += (monster) =>
        {
            int score = (int)(_monster.monsterStat.level * 100 - monster.aliveTime);
            if (score < _monster.monsterStat.level * 10)
                score = _monster.monsterStat.level * 10;

            if (_isBoss) score *= 10;

            scoreManager.AddScore(score);
        };
        _monster.damageTextPool = damageTextPool;
        _monster.hitEffectPool = hitEffectPool;
        monsterManager.AddMonseter(_monster);
        
        if(_isBoss)
            monsterManager.boss = _monster;
    }



    private IEnumerator SpawnWaveCoroutine()
    {
        int count = 0;
        while(count < spawnCountPerRound)
        {
            Addressables.InstantiateAsync(key, wayPoint, Quaternion.Euler(0f, -90f, 0f)).Completed += (op) =>
            {
                if (op.Status == AsyncOperationStatus.Succeeded)
                {
                    Monster monsterComp = op.Result.GetComponent<Monster>();
                    MonsterSetup(monsterComp);
                }
            };
            count++;
            yield return spawnPeriod;
        }
    }

    
}
