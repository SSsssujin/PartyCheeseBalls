using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class StageAds : MonoBehaviour
{
    public Button rewardButton;
    public Text reward;

    public static readonly string reward1Id = "ca-app-pub-1195551850458243/2249352335";

    private RewardedAd rewardedAd;
    [HideInInspector] public bool isFinished = false;

    private void Start()
    {
        // 버튼 비활성화
        //rewardButton.interactable = false;          // 보상광고

        // 원래 초기화 버튼 따로 있지만
        // 걍 여기서 하자
        List<string> deviceIds = new List<string>();
        deviceIds.Add("3F5AB3505EB789D5E71618587F90154A");

        RequestConfiguration requestConfiguration = new RequestConfiguration
            .Builder()
            .SetTestDeviceIds(deviceIds)
            .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);
        MobileAds.Initialize(initStatus => { });
    }

    //public void OnClickRequestReward()
    public void NextStageAds()
    {
        Debug.Log("광고 요청");

        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
        }
        this.rewardedAd = new RewardedAd(reward1Id);
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;

        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);

        /// 광고 보기 추가
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();

            //CameraMove getHintNum = FindObjectOfType<CameraMove>();
            //getHintNum.hintNum += 1;
        }
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
        rewardButton.interactable = true;
        //reward.text = "리워드 광고 출력";
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
        //rewardButton.interactable = false;
        isFinished = true;
    }

    public void OnClickReward()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        //reward.text = "received for " + amount.ToString() + " " + type;

        SceneManagement sceneManagement = FindObjectOfType<SceneManagement>();
        sceneManagement.LoadNextScene();
    }
}
