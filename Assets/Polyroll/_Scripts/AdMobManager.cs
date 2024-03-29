using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using GoogleMobileAds.Api;

public class AdMobManager : MonoBehaviour {

	[Header(" Test ")]
	public bool testMode;
	string iOSTestAppID = "ca-app-pub-3940256099942544~1458002511";
	string androidTestAppID = "ca-app-pub-3940256099942544~3347511713";
	string iOSTestBannerID = "ca-app-pub-3940256099942544/2934735716";
	string androidTestBannerID = "ca-app-pub-3940256099942544/6300978111";
	bool bannerRequested;

	[Header(" Ids ")]	
	public string iOSAppID;
	public string androidAppID;
	string appID;

	[Header(" Ad Types ")]	
	public string iOSBannerID;
	public string androidBannerID;
	string bannerID;
	// BannerView bannerView;


	// Use this for initialization
	void Start () {

		int noAds = PlayerPrefs.GetInt("NOADS");

		if(noAds == 1)
		{
			this.enabled = false;
		}

	#if UNITY_IOS
		if(!testMode)
		{
			appID = iOSAppID;
			bannerID = iOSBannerID;
		}
		else
		{
			appID = iOSTestAppID;
			bannerID = iOSTestBannerID;
		}


	#elif UNITY_ANDROID

		if(!testMode)
		{
			appID = androidAppID;
			bannerID = androidBannerID;
		}
		else
		{
			appID = androidTestAppID;
			bannerID = androidTestBannerID;
		}
	#endif

	// MobileAds.Initialize(appID);

	

	}
	
	void RequestBanner()
	{
		// Create a 320x50 banner at the top of the screen.
        // bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);
		
		// Create an empty ad request.
        // AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        // bannerView.LoadAd(request);
	}

	public void ShowBanner()
	{
		if(!bannerRequested)
		{
			this.RequestBanner();
			bannerRequested = true;
		}
		
		// bannerView.Show();
	}

	public void HideBanner()
	{
		// if(bannerRequested)
		// 	bannerView.Hide();
	}
}
