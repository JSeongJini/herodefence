using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreDialog : MyUIBase
{
    [SerializeField] private Button okButton;
    [SerializeField] private Text monsterKillScoreText;
    [SerializeField] private Text remainMonsterCountText;
    [SerializeField] private Text remainMonsterScoreText;
    [SerializeField] private Text remainCoinCountText;
    [SerializeField] private Text remainCoinScoreText;
    [SerializeField] private Text totalScoreText;

    private void Start()
    {
        if (okButton != null) okButton.onClick.AddListener(() =>
        {
            Hide();
            UIContext.GetUIByPath("Dialog/RankingDialog", (result) =>
            {
                RankingDialog dialog = result as RankingDialog;
                StartCoroutine(ShowRankingDialogCoroutine(dialog));
            });
        });
    }

    
    public void SetupScore(int _score, int _remainMonsterCount, int _remainCoinCount, int _remainMonsterScore, int _remainCoinScore, int _totalScore)
    {
        monsterKillScoreText.text = _score.ToString();
        remainMonsterCountText.text = _remainMonsterCount.ToString();
        remainCoinCountText.text = _remainCoinCount.ToString();

        remainMonsterScoreText.text = _remainMonsterScore.ToString();
        remainCoinScoreText.text = _remainCoinScore.ToString();

        totalScoreText.text = _totalScore.ToString();
    }

    private IEnumerator ShowRankingDialogCoroutine(RankingDialog _dialog)
    {
        MyWaitProgress waitProgress = null;

        if (_dialog.isReady == false)
        {
            UIContext.GetUIByPath("WaitProgress", (result) =>
            {
                waitProgress = result as MyWaitProgress;
                waitProgress.Show();
            });
        }

        while (_dialog.isReady == false)
        {
            yield return null;
        }

        if(_dialog.isReady)
        {
            waitProgress?.Hide();
            _dialog.Show();
        }
    }
}
