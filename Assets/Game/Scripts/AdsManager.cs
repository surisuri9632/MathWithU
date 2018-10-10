using UnityEngine;
using System;
using GoogleMobileAds.Api;


public class AdsManager : MonoBehaviour
{

    // Admob 작업
    public string android_banner_id;
    public string ios_banner_id;

    public string android_interstitial_id;
    public string ios_interstitial_id;

    private BannerView bannerView;
    private InterstitialAd interstitialAd;

    public void Start()
    {
        RequestBannerAd();
        RequestInterstitialAd();

        ShowBannerAd();
    }

    public void RequestBannerAd()
    {
        string adUnitId = string.Empty;

#if UNITY_ANDROID
        adUnitId = android_banner_id;
#elif UNITY_IOS
        adUnitId = ios_banner_id;
#endif

        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();


        bannerView.LoadAd(request);
    }

    private void RequestInterstitialAd()
    {
        string adUnitId = string.Empty;
#if UNITY_ANDROID
        adUnitId = android_interstitial_id;
#elif UNITY_IOS
        adUnitId = ios_interstitialAdUnitId;
#endif

        interstitialAd = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();

        interstitialAd.LoadAd(request);

        interstitialAd.OnAdClosed += HandleOnInterstitialAdClosed;
    }

    public void HandleOnInterstitialAdClosed(object sender, EventArgs args)
    {
        print("HandleOnInterstitialAdClosed event received");
        interstitialAd.Destroy();
        RequestInterstitialAd();

    }
    public void ShowBannerAd()
    {
        bannerView.Show();
    }

    public void ShowInterstitialAd()
    {
        if (!interstitialAd.IsLoaded())
        {
            RequestInterstitialAd();
            return;
        }

        interstitialAd.Show();
    }


    /*
    // 게임 오버 되었을 때 비디오 광고 출력되는 소스
    private int i = 0;

    // Use this for initialization
    void Start() {
        i = 0;
    }

    // Update is called once per frame
    void Update() {
        if (GameManager.singleton.isGameOver == true)
        {
            if (i == 0)
            {
                ShowAd();
                i++;
            }
        }
    }
    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
    }
    */
}
