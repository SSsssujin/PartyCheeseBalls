using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class GoogleMobileAdTest : MonoBehaviour
{
    public Button interstitialButton;
    public Button rewardButton;
    public Text reward;

    public static readonly string interstitial1Id = "ca-app-pub-1195551850458243/5758186652";
    public static readonly string reward1Id = "ca-app-pub-1195551850458243/2249352335";
    //public static readonly string reward1Id = "ca-app-pub-1195551850458243/7429526827";   // 지은

    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;

    private void Start()
    {
        // 버튼 비활성화
        interstitialButton.interactable = false;    // 전면광고
        rewardButton.interactable = true;          // 보상광고
    }


    public void OnClickInit()
    {
        // APK 빌드,실행해서 나오는 내 ID, 기획자 ID 추가
        // 근데 나중에 이부분 빼고 출시해야 한다고 함;
        //2021 - 11 - 04 11:36:13.989 28482 28482 Info Ads Use RequestConfiguration.Builder().
        //setTestDeviceIds(Arrays.asList("3F5AB3505EB789D5E71618587F90154A")) to get test ads on this device.

        List<string> deviceIds = new List<string>();
        deviceIds.Add("3F5AB3505EB789D5E71618587F90154A");
        deviceIds.Add("113805B9D5861693327775B7D823425D");

        RequestConfiguration requestConfiguration = new RequestConfiguration
            .Builder()
            .SetTestDeviceIds(deviceIds)
            .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);
        MobileAds.Initialize(initStatus => { });
    }

    public void OnClickRequestInterstitial()
    {
        if (interstitial != null)
        {
            interstitial.Destroy();
        }
        interstitial = new InterstitialAd(interstitial1Id);
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        this.interstitial.OnAdOpening += HandleOnAdOpened;

        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        interstitialButton.interactable = true;
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
        interstitialButton.interactable = false;
    }

    public void OnClickInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }
    public void OnClickRequestReward()
    {
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
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        // "리워드 광고 요청" 버튼 눌렀을 시
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
        rewardButton.interactable = true;
        reward.text = "리워드 광고 출력";
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        // "리워드 광고 출력" 버튼 눌렀을 시
        MonoBehaviour.print("HandleRewardedAdOpening event received");
        rewardButton.interactable = false;
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
        reward.text = "received for " + amount.ToString() + " " + type;
    }
}
