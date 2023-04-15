using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameSessionManager : MonoBehaviour
{
    [Header("--Own Component Reference--")]
    [SerializeField] private GameStateContext context;
    private readonly Dictionary<EGameState, IState<GameSessionManager>> states
        = new Dictionary<EGameState, IState<GameSessionManager>>();

    [Space]
    [Header("--Round--")]
    public int round = 1;
    public int lastRound = 40;
    public readonly float minRoundTime = 10f;
    public readonly float roundTimeIncrease = 5f;
    public readonly float maxRoundTime = 40f;
    public float remainTime = 30f;

    [Space]
    [Header("--Playing--")]
    public int score = 0;
    public int monsterCountToFail = 50;
    public float playTime = 0f;

    [Space]
    [Header("--Other Component Reference--")]
    [SerializeField] private PanelController panelController = null;
    [SerializeField] private MonsterManager monsterManager = null;
    [SerializeField] private ScoreManager scoreManager = null;
    [SerializeField] private CoinManager coinManager = null;

    [HideInInspector] public bool isRecordTime;
    [HideInInspector] public bool isRounding;
    public EGameState currentState;
    [HideInInspector] public EGameState previousState;
    

    public void Awake()
    {
        context = GetComponent<GameStateContext>();
        if (!panelController) panelController = FindObjectOfType<PanelController>();
        if (!monsterManager) monsterManager = FindObjectOfType<MonsterManager>();
        if (!scoreManager) scoreManager = FindObjectOfType<ScoreManager>();
        if (!coinManager) coinManager = FindObjectOfType<CoinManager>();
    }

    public void Start()
    {
        states.Add(EGameState.READY, new GameReadyState(panelController));
        states.Add(EGameState.START, new GameStartState());
        states.Add(EGameState.ROUND, new GameRoundState(monsterManager));
        states.Add(EGameState.ROUNDEND, new GameRoundEndState(monsterManager, panelController));
        states.Add(EGameState.FAIL, new GameFailState());
        states.Add(EGameState.SUCCEED, new GameSucceedState());

        SetState(EGameState.READY);
    }

    public void Update()
    {
        if (isRecordTime)
            playTime += Time.unscaledDeltaTime;
    }

    public void SetState(EGameState _state)
    {
        previousState = currentState;
        currentState = _state;

        context.Transition(states[_state]);

        GameEventBus.Publish(_state);
    }

    public void GameEnd()
    {
        int remainMonsterCount = monsterManager.GetMonsterCount();
        int remainCoinCount = coinManager.Coin;
        int remainMonsterScore = scoreManager.GetRemainMonsterScore(remainMonsterCount, round);
        int remainCoinScore = scoreManager.GetRemainCoinScore(remainCoinCount);
        int totalScore = scoreManager.Score + remainMonsterScore + remainCoinScore;

        UIContext.GetUIByPath("Dialog/FinalScoreDialog", (result) =>
        {
            FinalScoreDialog dialog = result as FinalScoreDialog;
            dialog.SetupScore(scoreManager.Score, remainMonsterCount, remainCoinCount, remainMonsterScore, remainCoinScore, totalScore);
            dialog.Show();
        });

        UIContext.GetUIByPath("Dialog/RankingDialog", (result) =>
        {
            RankingDialog dialog = result as RankingDialog;
            dialog.Hide();
            StartCoroutine(GetRainkingCoroutine(totalScore, dialog));
        });
    }

    private IEnumerator GetRainkingCoroutine(int _totalScore, RankingDialog _dialog)
    {
        //최종 점수로 랭킹 갱신 후 현재 랭킹을 얻어 오기
        WWWForm form = new WWWForm();
        form.AddField("command", "newScore");
        form.AddField("userName", GameManager.Instance.userName);
        form.AddField("score", _totalScore);

        NetworkData rankingData = new NetworkData();
        yield return StartCoroutine(GoogleSheetManager.Instance.Post(form, rankingData));


        int highScore = 0;
        int userRanking = 0;
        if (rankingData.result == "success")
        {
            highScore = rankingData.value;
            userRanking = int.Parse(rankingData.msg);
            Debug.Log("HighScore : " + highScore);
            Debug.Log("userRanking : " + userRanking);
        }

        //1~5등까지의 유저 정보 얻어오기
        form = new WWWForm();
        form.AddField("command", "getRanking");
        rankingData = new NetworkData();
        yield return StartCoroutine(GoogleSheetManager.Instance.Post(form, rankingData));

        _dialog.SetupRanking(rankingData.msg, userRanking, highScore);
    }
}

