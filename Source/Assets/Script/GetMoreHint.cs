using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class GetMoreHint : MonoBehaviour
{
    //public Button interstitialButton;
    //public Text reward;
    
    private InterstitialAd interstitial;
    public Button rewardButton;

    private bool isInit = false;
    bool isRequest;

    public static readonly string interstitial1Id = "ca-app-pub-1195551850458243/5758186652";
    public static readonly string reward1Id = "ca-app-pub-1195551850458243/2249352335";

    //private RewardedAd rewardedAd;

    //public void TestHint()
    private void Awake()
    {
        //버튼 비활성화 

        if (!isInit)
        {
            rewardButton.interactable = false;

            // 초기화
            List<string> deviceIds = new List<string>();
            deviceIds.Add("3F5AB3505EB789D5E71618587F90154A");

            RequestConfiguration requestConfiguration = new RequestConfiguration
                .Builder()
                .SetTestDeviceIds(deviceIds)
                .build();
            MobileAds.SetRequestConfiguration(requestConfiguration);
            //MobileAds.Initialize(initStatus => { this.OnClickRequestReward(); });       // 광고 요청
            MobileAds.Initialize(initStatus => { this.OnClickRequestInterstitial(); });       // 광고 요청

            isInit = true;
        }
    }

    private void Update()
    {
        if (!isRequest)
        {
            OnClickRequestInterstitial();
            isRequest = true;
        }
    }

    private void OnClickRequestInterstitial()
    {
        if (interstitial != null)
        {
            interstitial.Destroy();
        }
        interstitial = new InterstitialAd(interstitial1Id);
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        rewardButton.interactable = true;
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
        rewardButton.interactable = false;
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("closed");
        OnClickRequestInterstitial();
        //GameManager.Instance.isAdEnd = true;
        //OnClickRequestInterstitial();
    }

    public void OnClickInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }

        CameraMove getHintNum = FindObjectOfType<CameraMove>();
        getHintNum.hintNum += 1;

        GameObject.Find("No Hint").SetActive(false);
    }

    



    //public void OnClickRequestReward()
    //{
    //    Debug.Log("광고 요청");
    //
    //    if (rewardedAd != null)
    //    {
    //        rewardedAd.Destroy();
    //    }
    //    this.rewardedAd = new RewardedAd(reward1Id);
    //    this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
    //    this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
    //
    //    this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
    //    AdRequest request = new AdRequest.Builder().Build();
    //    this.rewardedAd.LoadAd(request);
    //
    //    /// 광고 보기 추가
    //    /// /여기서 추가하면 안된다함 
    //    //if (this.rewardedAd.IsLoaded())
    //    //{
    //    //    this.rewardedAd.Show();
    //    //
    //    //    CameraMove getHintNum = FindObjectOfType<CameraMove>();
    //    //    getHintNum.hintNum += 1;
    //    //
    //    //    GameObject.Find("No Hint").SetActive(false);
    //    //}
    //
    //}
    //
    //public void HandleRewardedAdLoaded(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleRewardedAdLoaded event received");
    //    rewardButton.interactable = true;
    //
    //    //Debug.Log(rewardedAd.IsLoaded);
    //
    //    //isLoaded = true;
    //
    //    //Debug.Log(rewardButton.interactable);
    //    //Debug.Log("Hi");
    //    //reward.text = "리워드 광고 출력";
    //}
    //
    //public void HandleRewardedAdOpening(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleRewardedAdOpening event received");
    //    //rewardButton.interactable = false;
    //}
    //
    //public void OnClickReward()
    //{
    //    StartCoroutine(CoPrintAds());
    //
    //    //if (this.rewardedAd.IsLoaded())
    //    //{
    //    //    this.rewardedAd.Show();
    //    //}
    //
    //    //CameraMove getHintNum = FindObjectOfType<CameraMove>();
    //    //getHintNum.hintNum += 1;
    //    //
    //    //GameObject.Find("No Hint").SetActive(false);
    //}
    //
    //public void HandleUserEarnedReward(object sender, Reward args)
    //{
    //    string type = args.Type;
    //    double amount = args.Amount;
    //    //reward.text = "received for " + amount.ToString() + " " + type;
    //
    //    // 광고요청
    //    OnClickRequestReward();
    //}
}
