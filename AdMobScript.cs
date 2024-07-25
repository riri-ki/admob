using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class AdMobScript : MonoBehaviour
{
  private BannerView bannerView;
  public void Initialize()
  {
    // Google AdMob Initial
    MobileAds.Initialize(initStatus => { });
    
  }
  public void RequestBanner()
  {
#if UNITY_ANDROID
    string adUnitId = "ca-app-pub-3940256099942544/6300978111"; // Androidのバナー広告用のユニットID
#elif UNITY_IPHONE
    string adUnitId = "ca-app-pub-3940256099942544/2934735716"; // Iphone用のバナー広告用のユニットID
#else
    string adUnitId = "unexpected_platform";
#endif
    // Create a 320x50 banner at the bottom of the screen.
    this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
    // Create an empty ad request.
    AdRequest request = new AdRequest();
    // Load the banner with the request.
    bannerView.LoadAd(request);
  }



  private InterstitialAd interstitial;
  public void loadInterstitialAd()
  {
#if UNITY_ANDROID
    string adUnitId = "ca-app-pub-3940256099942544/1033173712";// Androidのインタースティシャル広告用のユニットID
#elif UNITY_IPHONE
    string adUnitId = "ca-app-pub-3940256099942544/4411468910";// Iphoneのインタースティシャル広告用のユニットID
#else
    string adUnitId = "unexpected_platform";
#endif
    InterstitialAd.Load(adUnitId, new AdRequest(),
      (InterstitialAd ad, LoadAdError loadAdError) =>
      {
        if (loadAdError != null)
        {
          // Interstitial ad failed to load with error
          interstitial.Destroy();
          return;
        }
        else if (ad == null)
        {
          // Interstitial ad failed to load.
          return;
        }
        ad.OnAdFullScreenContentClosed += () => {
          HandleOnAdClosed();
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
          HandleOnAdClosed();
        };
        interstitial = ad;
      });
  }
  private void HandleOnAdClosed()
  {
    this.interstitial.Destroy();
    this.loadInterstitialAd();
  }
  public void showInterstitialAd()
  {
    if (interstitial != null && interstitial.CanShowAd())
    {
      interstitial.Show();
    } else
    {
      Debug.Log("Interstitial Ad not load");
    }
  }



}