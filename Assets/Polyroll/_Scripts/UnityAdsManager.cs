using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.Monetization;

public class UnityAdsManager : MonoBehaviour {

    [Header(" Test ")] 
    public bool testMode;
    public bool enableUnityAds;

    [Header(" Ids ")]	
	public string iOSGameID;
	public string androidGameID;
	string gameId;

    [Header(" Settings ")]
    public int gamesBeforeVideo;

	public string videoPlacementID = "video";
	public string rewardedVideoPlacementID = "rewardedVideo";
	
	public static bool showVideo;
	public static int adCounter;

	[Header(" Rewarded Video Stuff ")]
	public Button watchRewardedVideoButton;


    void Start () {

        int noAds = PlayerPrefs.GetInt("NOADS");

		if(noAds == 1)
		{
			this.enabled = false;
		}

        if(!enableUnityAds)
            this.enabled = false;

    #if UNITY_IOS
        gameId = iOSGameID;
	#elif UNITY_ANDROID
        gameId = androidGameID;
    #else
        gameId = iOSGameID;
	#endif

        
        // Monetization.Initialize (gameId, testMode);

        if(watchRewardedVideoButton != null)
		    watchRewardedVideoButton.onClick.AddListener(ShowRewardedVideo);
    }

    public static void ShowVideoStatic()
    {
        showVideo = true;
    }

	private void Update() {

		if(showVideo)
		{
            adCounter++;

            if(adCounter >= gamesBeforeVideo)
            {
                ShowVideo();
                adCounter = 0;
            }

			showVideo = false;
		}

		// if(watchRewardedVideoButton != null)
			// watchRewardedVideoButton.interactable = Monetization.IsReady(rewardedVideoPlacementID);
	}

	public void ShowVideo () {
        Debug.Log("Showing Video");
        // StartCoroutine (ShowVideoWhenReady ());
    }

    // private IEnumerator ShowVideoWhenReady () {
        // while (!Monetization.IsReady (videoPlacementID)) {
        //     yield return new WaitForSeconds(0.25f);
        // }

        // ShowAdPlacementContent ad = null;
        // ad = Monetization.GetPlacementContent (videoPlacementID) as ShowAdPlacementContent;

        // if(ad != null) {
        //     ad.Show ();
        // }
    // }

	void ShowRewardedVideo () {

        // ShowAdCallbacks options = new ShowAdCallbacks ();
        // options.finishCallback = HandleShowResult;
        // ShowAdPlacementContent ad = Monetization.GetPlacementContent (rewardedVideoPlacementID) as ShowAdPlacementContent;
        // ad.Show (options);
    }

   //  void HandleShowResult (ShowResult result) {
   //      if (result == ShowResult.Finished) {
   //          // Reward the player
			// Debug.Log("Finished Video");
   //
   //      } else if (result == ShowResult.Skipped) {
   //          Debug.LogWarning ("The player skipped the video - DO NOT REWARD!");
   //      } else if (result == ShowResult.Failed) {
   //          Debug.LogError ("Video failed to show");
   //      }
   //  }

}

