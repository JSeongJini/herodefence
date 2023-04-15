using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;


public class AdmobAdsController : Singlton<AdmobAdsController>
{
    private readonly string unitID = "ca-app-pub-6552740244010959/5549273759";
    private readonly string testUnitID = "ca-app-pub-3940256099942544/5224354917";

    public UnityEvent OnEarendReward = new UnityEvent();

    private RewardedAd rewardedAd = null;


    private void Start()
    {
        MobileAds.Initialize(initStauts => { });

        CreateAndLoadRewardedAd();
    }

    private void OnEnable()
    {
        GameEventBus.Subscribe(EGameState.ROUNDEND, CreateAndLoadRewardedAd);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(EGameState.ROUNDEND, CreateAndLoadRewardedAd);
    }

    public void ShwoRewardedAd()
    {
        if (rewardedAd.CanShowAd())
        {
            float beforeTimeScale = Time.timeScale;
            rewardedAd.Show((rewardedAd) => {
                OnEarendReward.Invoke();
                Time.timeScale = beforeTimeScale;
            });
        }
        else
        {
            UIContext.GetUIByPath("Dialog/ErrorShowAd", (result)=> {
                MyDialog dialog = result as MyDialog;
                dialog.Show();
            });
        }
    }

    public void CreateAndLoadRewardedAd()
    {
        string id = Debug.isDebugBuild ? testUnitID : unitID;

        AdRequest request = new AdRequest.Builder().Build();

        RewardedAd.Load(id, request, (rewardedAd, error) => {
            this.rewardedAd = rewardedAd;
        });
    }
}
