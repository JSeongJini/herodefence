using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingDialog : MyUIBase
{
    [SerializeField] private Button okButton;
    [SerializeField] private Text[] userNameTexts;
    [SerializeField] private Text[] scoreTexts;
    [SerializeField] private Text rankingText;

    public bool isReady = false;

    private void Start()
    {
        okButton.onClick.AddListener(() =>
        {
            Hide();
            SceneManager.Instance.LoadScene(0);
        }); 
    }

    public void SetupRanking(string _data, int _userRanking, int _userScore)
    {
        string[] datas = _data.Split("\t");
        int rank = 0;
        for(int i = 0; i < datas.Length - 1; i += 2)
        {
            userNameTexts[rank].text = datas[i];
            scoreTexts[rank].text = datas[i + 1];
            rank++;
        }

        rankingText.text = _userRanking + "µî";
        userNameTexts[5].text = GameManager.Instance.userName;
        scoreTexts[5].text = _userScore.ToString();

        isReady = true;
    }

    public override void Hide()
    {
        base.Hide();
        isReady = false;
    }
}
