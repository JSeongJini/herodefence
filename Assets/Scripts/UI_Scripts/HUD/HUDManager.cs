using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("--Subscribe Event Channel--")]
    [SerializeField] private IntEventChanelSO monsterCountChangeEvent = null;
    [SerializeField] private IntEventChanelSO coinChangeEvent = null;
    [SerializeField] private IntEventChanelSO scoreChangeEvent = null;

    [Header("--Other Components References--")]
    [SerializeField] private GameSessionManager gameSessionManager = null;
    [SerializeField] private CoinManager coinManager = null;

    [Space]
    [Header("--UI Componentes References--")]
    [SerializeField] private Text remainTimeText = null;
    [SerializeField] private Text roundText = null;
    [SerializeField] private Text monsterCountText = null;
    [SerializeField] private Text coinText = null;
    [SerializeField] private Text scoreText = null;
    [SerializeField] private Button settingButton = null;
    [SerializeField] private GameObject MonsterInfoBox = null;

    #region for Cache
    private float previousTime = 0f;
    #endregion

    private Color bossTextColor = Color.red;


    public void Awake()
    {
        if (!gameSessionManager) gameSessionManager = FindObjectOfType<GameSessionManager>();
        if (!remainTimeText) remainTimeText = GameObject.Find("RemainTime_Text").GetComponent<Text>();
        if (!roundText) remainTimeText = GameObject.Find("Round_Num_Text").GetComponent<Text>();
        if (!coinText) coinText = GameObject.Find("Coin_Text").GetComponent<Text>();
        if (!monsterCountText) monsterCountText = GameObject.Find("Monster_Text").GetComponent<Text>();
        if (!settingButton) settingButton = GameObject.Find("SettingButton").GetComponent<Button>();
        if (!MonsterInfoBox) MonsterInfoBox = GameObject.Find("Monster_Information_Box");
        if (!coinManager) coinManager = FindObjectOfType<CoinManager>();

        if (settingButton) settingButton.onClick.AddListener(() =>
        {
             UIContext.GetUIByPath("SettingPanel", (result) => { 
                 MyUIBase ui = result as MyUIBase;
                 ui.Show();

                 Time.timeScale = 0f;
             });
        });
    }

#if UNITY_EDITOR
    private void Start()
    {
        GameManager.Instance.isInGame = true;
    }
#endif

    public void Update()
    {
        float remainTime = Mathf.CeilToInt(gameSessionManager.remainTime);
        if (remainTime != previousTime)
        {
            remainTimeText.text = remainTime.ToString();
            previousTime = remainTime;
        }
    }

    public void OnEnable()
    {
        GameEventBus.Subscribe(EGameState.ROUND, UpdateRoundText);
        monsterCountChangeEvent.OnRequested += UpdateMonsterCountText;
        coinChangeEvent.OnRequested += UpdateCoinText;
        scoreChangeEvent.OnRequested += UpdateScoreText;
    }

    public void OnDisable()
    {
        GameEventBus.Unsubscribe(EGameState.ROUND, UpdateRoundText);
        monsterCountChangeEvent.OnRequested -= UpdateMonsterCountText;
        coinChangeEvent.OnRequested -= UpdateCoinText;
        scoreChangeEvent.OnRequested -= UpdateScoreText;
    }

    public void UpdateRoundText()
    {
        roundText.text = string.Format("{0:D2}", gameSessionManager.round);
        if(gameSessionManager.round % 5 == 0)
        {
            roundText.color = bossTextColor;
        }
    }

    public void UpdateMonsterCountText(int _count)
    {
        monsterCountText.text = _count.ToString();
    }

    public void UpdateCoinText(int _coin)
    {
        coinText.text = _coin.ToString();
    }

    public void UpdateScoreText(int _score)
    {
        scoreText.text = _score.ToString();
    }
}
