using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppLovinShowVid : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject msg;
   
    void Start()
    {
        AppLovin.SetSdkKey("4OHB3a_TttPgCRSo2P1zJcWnHT1HQsUVFmgILfnCXAukcKWfkTc2A8pRcqi2gCXwzDDIt_VP9N8nieQRC2TMAG");
        AppLovin.InitializeSdk();
        AppLovin.LoadRewardedInterstitial();

    }
    IEnumerator hidenotify()
    {
        GameObject.Find("Canvas/Notify/NetworkErrorMessage").SetActive(true);
        yield return new WaitForSeconds(3);
        GameObject.Find("Canvas/Notify/NetworkErrorMessage").SetActive(false);
    }
    public void applovin()
    {
       
        AppLovin.SetUnityAdListener(gameObject.name);
        if (AppLovin.IsIncentInterstitialReady())
        {

            
            AppLovin.ShowRewardedInterstitial();
            
        }
        else
        {
           
          //  StartCoroutine("hidenotify");
            // No rewarded ad is available.  Perform failover logic...
            Debug.Log("No rewarded ad is available.  Perform failover logic...");
        }
        
    }
    void onAppLovinEventReceived(string ev)
    {
        Debug.Log(ev);

        if (ev.Contains("REWARDAPPROVEDINFO"))
        {
            // Split the string into its three components.
            char[] delimiter = { '|' };
            string[] split = ev.Split(delimiter);

            // Pull out and parse the amount of virtual currency.
            double amount = double.Parse(split[1]);

            // Pull out the name of the virtual currency
            string currencyName = split[2];

            // Do something with this info - for example, grant coins to the user
            //myFunctionToUpdateBalance(currencyName, amount);
        }

        if (string.Equals(ev, "DISPLAYEDINTER") || string.Equals(ev, "VIDEOBEGAN"))
        {
            Time.timeScale = 0.0f;
            AudioListener.pause = true;
        }

        if (string.Equals(ev, "HIDDENINTER") || string.Equals(ev, "VIDEOSTOPPED"))
        {
            Time.timeScale = 1.0f;
            AudioListener.pause = false;
        }

        if (string.Equals(ev, "LOADEDINTER"))
        {
            // The last ad load was successful.
            // Probably do AppLovin.ShowInterstitial();
        }
        if (string.Equals(ev, "LOADFAILED"))
        {
            // The last ad load failed.
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
