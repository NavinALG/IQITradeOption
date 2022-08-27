using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartboostSDK;
using System;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.Advertisements;
using UnityEngine.Networking;
public class ChartBoost : MonoBehaviour
{
   
    private ChatBoostTimer ChartbosstTimerReference;
    // Start is called before the first frame update
    void Start()
    {
       // Chartboost.cacheRewardedVideo(CBLocation.Default);
        Chartboost.cacheInterstitial(CBLocation.Default);
        ChartbosstTimerReference = this.GetComponent<ChatBoostTimer>();
        Chartboost.cacheRewardedVideo(CBLocation.Default);
        Chartboost.didCompleteRewardedVideo += didCompleteRewardedVideo;
        Chartboost.didFailToLoadInPlay += didFailToLoadInPlay;
        Chartboost.didCacheRewardedVideo += didCacheRewardedVideo;
        Chartboost.didFailToLoadRewardedVideo += didFailToLoadRewardedVideo;
       Chartboost.didCloseRewardedVideo += didCloseRewardedVideo;
    }
   

    public void ChartboostInterstitial()
    {
        Chartboost.showInterstitial(CBLocation.Default);
    }
    public void ChartBooostCall()
    {

       
        Chartboost.cacheRewardedVideo(CBLocation.Default);
        Chartboost.showRewardedVideo(CBLocation.Default);
       
         var url1 = AppLoad.BaseUrl + "Updatevideo";
         string json1 = "{\"user_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"video_name\":\"Chartboost\",\"type\":\"1\",\"earn\":\""+Ui.vamount+"\"}";
         StartCoroutine(ChartbosstTimerReference.PostRequestCoroutine(url1, json1));    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    void didFailToLoadInterstitial(CBLocation location, CBImpressionError error)
    {
        Debug.Log(string.Format("didFailToLoadInterstitial: {0} at location {1}", error, location));
    }

    void didDismissInterstitial(CBLocation location)
    {
        Debug.Log("didDismissInterstitial: " + location);
    }

    void didCloseInterstitial(CBLocation location)
    {
        Debug.Log("didCloseInterstitial: " + location);
    }

    void didClickInterstitial(CBLocation location)
    {
        Debug.Log("didClickInterstitial: " + location);
    }

    void didCacheInterstitial(CBLocation location)
    {
        Debug.Log("didCacheInterstitial: " + location);
    }

    bool shouldDisplayInterstitial(CBLocation location)
    {
        Debug.Log("shouldDisplayInterstitial: " + location);
        return true;
    }

    void didDisplayInterstitial(CBLocation location)
    {
        Debug.Log("didDisplayInterstitial: " + location);
    }

    void didFailToLoadMoreApps(CBLocation location, CBImpressionError error)
    {
        Debug.Log(string.Format("didFailToLoadMoreApps: {0} at location: {1}", error, location));
    }

    void didDismissMoreApps(CBLocation location)
    {
        Debug.Log(string.Format("didDismissMoreApps at location: {0}", location));
    }

    void didCloseMoreApps(CBLocation location)
    {
        Debug.Log(string.Format("didCloseMoreApps at location: {0}", location));
    }

    void didClickMoreApps(CBLocation location)
    {
        Debug.Log(string.Format("didClickMoreApps at location: {0}", location));
    }

    void didCacheMoreApps(CBLocation location)
    {
        Debug.Log(string.Format("didCacheMoreApps at location: {0}", location));
    }

    bool shouldDisplayMoreApps(CBLocation location)
    {
        Debug.Log(string.Format("shouldDisplayMoreApps at location: {0}", location));
        return true;
    }

    void didDisplayMoreApps(CBLocation location)
    {
        Debug.Log("didDisplayMoreApps: " + location);
    }

    void didFailToRecordClick(CBLocation location, CBImpressionError error)
    {
        Debug.Log(string.Format("didFailToRecordClick: {0} at location: {1}", error, location));
    }

    void didFailToLoadRewardedVideo(CBLocation location, CBImpressionError error)
    {
        Debug.Log(string.Format("didFailToLoadRewardedVideo: {0} at location {1}", error, location));
    }

    void didDismissRewardedVideo(CBLocation location)
    {
        Debug.Log("didDismissRewardedVideo: " + location);
    }

    void didCloseRewardedVideo(CBLocation location)
    {
       
        Debug.Log("didCloseRewardedVideo: " + location);
    }

    void didClickRewardedVideo(CBLocation location)
    {
        Debug.Log("didClickRewardedVideo: " + location);
    }

    void didCacheRewardedVideo(CBLocation location)
    {
       Debug.Log("didCacheRewardedVideo: " + location);
    }

    bool shouldDisplayRewardedVideo(CBLocation location)
    {
        Debug.Log("shouldDisplayRewardedVideo: " + location);
        return true;
    }

    void didCompleteRewardedVideo(CBLocation location, int reward)
    {
          Debug.Log(string.Format("didCompleteRewardedVideo: reward {0} at location {1}", reward, location));
    }

    void didDisplayRewardedVideo(CBLocation location)
    {
        Debug.Log("didDisplayRewardedVideo: " + location);
    }

    void didCacheInPlay(CBLocation location)
    {
        Debug.Log("didCacheInPlay called: " + location);
    }

    void didFailToLoadInPlay(CBLocation location, CBImpressionError error)
    {
        Debug.Log(string.Format("didFailToLoadInPlay: {0} at location: {1}", error, location));
    }

    void didPauseClickForConfirmation()
    {
        Debug.Log("didPauseClickForConfirmation called");
    }

    void willDisplayVideo(CBLocation location)
    {
        Debug.Log("willDisplayVideo: " + location);
    }

#if UNITY_IPHONE
    void didCompleteAppStoreSheetFlow() {
        Debug.Log("didCompleteAppStoreSheetFlow");
    }
#endif
}
