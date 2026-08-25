using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdMobScript : MonoBehaviour
{
    private BannerView bannerView;

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-5713662065544983~4180197864";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-5713662065544983~7479743659";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Clean up banner before reusing
        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);  
        
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    public void LoadBanner()
    {
        print("Load Banner");
        RequestBanner();
    }

    public void DestroyBanner()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
            print("Destroy Banner");
        }
    }
}
