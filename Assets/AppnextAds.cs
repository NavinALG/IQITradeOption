using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using appnext;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;
using System;

public class AppnextAds : MonoBehaviour
{
    // Start is called before the first frame update
    Interstitial interstitial;
    public static DateTime date1;
    public AudioSource GiftsSound;
    RewardedVideo rewardedVideo;
    public Gift GiftDataReference;
  //  public Text info;
    float valueToIncreaseEverySec = 1;
    public GameObject tost;
    private string datetimer = "";
    public GameObject tcounter, tcounterb, WATCH;
    void Start()
    {
        interstitial = new Interstitial("853edd67-702a-437f-b2cd-c17535ad9acc");
        rewardedVideo = new RewardedVideo(" 853edd67-702a-437f-b2cd-c17535ad9acc");
        interstitial.loadAd();
        configads();
        rewardedVideo.onVideoEndedDelegate += onVideoEnded;
        if (datetimer == "")
        {
            //loading.SetActive(true);
            var url1 = "https://gamelovin.com/tradeoption/tradeoption/Trading/Userprocess/Gettimer";
            string json1 = "{\"unique_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"sdkname\":\"Appnext\",\"datetime\":\"" + DateTime.Now.ToString() + "\",\"earn\":\""+ Ui.vamount +"\"}";
            StartCoroutine(PosttimerCoroutine(url1, json1));
        }
    }
    public void onVideoEnded(Ad ad)
    {
       
        var url1 = "https://gamelovin.com/tradeoption/Trading/Userprocess/Updatevideo";
        string json1 = "{\"user_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"video_name\":\"Appnext\",\"type\":\"1\",\"earn\":\"" + Ui.vamount + "\"}";
        StartCoroutine(PostRequestCoroutine(url1, json1));
    }
    private IEnumerator PostRequestCoroutine(string url, string json)
    {

        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(json);

        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();

        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";

        UnityWebRequest www =
            new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {

            //  GameObject tost = GameObject.Find("Canvas/Notify/NetworkErrorMessage");
            tost.transform.GetChild(0).GetComponent<Text>().text = "Network error!";
            tost.SetActive(true);
            StartCoroutine("hidenotify");
        }
        else
        {

            JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());
            // Debug.Log(jsonNode["status"]+"and"+ jsonNode["message"]);
            if (jsonNode["status"] == "0")
            {
                PlayerPrefs.SetFloat("Total", PlayerPrefs.GetFloat("Total") + Ui.vamount);
                //  GameObject tost = GameObject.Find("Canvas/Notify/NetworkErrorMessage");
                tost.transform.GetChild(0).GetComponent<Text>().text = "+"+Ui.vamount+" Coins";
                tost.SetActive(true);
                StartCoroutine("hidenotify");
                Invoke("watchinvoke", 10);
                GiftsSound.Play();
                PlayerPrefs.SetInt("appnextwatch", 1);
                DateTime dd = DateTime.Parse(DateTime.Now.ToString());
                DateTime newdate = dd.AddMinutes(30);
                // PlayerPrefs.SetString("unitydate", newdate.ToString());
                datetimer = newdate.ToString();
                GiftDataReference.UpdateRemaningAD();
                Ui.LockscreenNum += 1;
                
                var url1 = "https://gamelovin.com/tradeoption/Trading/Userprocess/Addtimer";
                string json1 = "{\"unique_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"sdkname\":\"Appnext\",\"datetime\":\"" + datetimer + "\",\"earn\":\"10\"}";
                StartCoroutine(PostAddtimerCoroutine(url1, json1));

            }

            else
            {
                // GameObject tost = GameObject.Find("Canvas/Notify/NetworkErrorMessage");
                tost.transform.GetChild(0).GetComponent<Text>().text = "Somthing going wrong!";
                tost.SetActive(true);
                StartCoroutine("hidenotify");
                // TimerSlider.gameObject.SetActive(false);
            }
            //  Debug.Log(www.downloadHandler.text);
        }

    }
    private IEnumerator PostAddtimerCoroutine(string url, string json)
    {

        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(json);

        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();

        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";

        UnityWebRequest www =
            new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {

            //  GameObject tost = GameObject.Find("Canvas/Notify/NetworkErrorMessage");
            tost.transform.GetChild(0).GetComponent<Text>().text = "Network error!";
            tost.SetActive(true);
            StartCoroutine("hidenotify");
        }
        else
        {

            JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());
            // Debug.Log(jsonNode["status"]+"and"+ jsonNode["message"]);
            if (jsonNode["status"] == "0")
            {


                // TimerSlider.gameObject.SetActive(true);
            }

            else
            {
                //  GameObject tost = GameObject.Find("Canvas/Notify/NetworkErrorMessage");
                tost.transform.GetChild(0).GetComponent<Text>().text = "Somthing going wrong2!";
                tost.SetActive(true);
                StartCoroutine("hidenotify");

            }

        }

    }
    private IEnumerator PosttimerCoroutine(string url, string json)
    {

        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(json);

        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();

        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";

        UnityWebRequest www =
            new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        yield return www.SendWebRequest();
       
        if (www.isNetworkError)
        {

            // GameObject tost = GameObject.Find("Canvas/Notify/NetworkErrorMessage");
            tost.transform.GetChild(0).GetComponent<Text>().text = "Network error!";
            tost.SetActive(true);
            StartCoroutine("hidenotify");
        }
        else
        {

            JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());

            if (jsonNode["status"] == "0")
            {

                datetimer = jsonNode["lastwatch"];

            }

            else
            {
                //  GameObject tost = GameObject.Find("Canvas/Notify/NetworkErrorMessage");
                tost.transform.GetChild(0).GetComponent<Text>().text = "Somthing going wrong!";
                tost.SetActive(true);
                StartCoroutine("hidenotify");

            }

        }

    }
    public void VideoShow()
    {
        
        rewardedVideo.loadAd();
        
        rewardedVideo.setMute(false);
        rewardedVideo.setProgressType(Video.PROGRESS_CLOCK);
        rewardedVideo.setProgressColor("#ffffff");
        rewardedVideo.setVideoLength(Video.VIDEO_LENGTH_LONG);
        //Or
        //rewardedVideo.setShowClose(true, 5000);
        rewardedVideo.setOrientation(Ad.ORIENTATION_PORTRAIT);
        rewardedVideo.showAd();
    }
    void watchinvoke()
    {

        WATCH.SetActive(false);
    }
    void timecounter()
    {
        DateTime dddd = DateTime.Parse(DateTime.Now.ToString());

        // if (DateTime.Parse(PlayerPrefs.GetString("unitydate")) > dddd)
        if (DateTime.Parse(datetimer) > dddd)
        {

            //  TimeSpan span = DateTime.Parse(PlayerPrefs.GetString("unitydate")).Subtract(dddd);
            TimeSpan span = DateTime.Parse(datetimer).Subtract(dddd);
            tcounter.SetActive(true);

            if (span.Minutes <= 9)
            {
                tcounter.transform.GetComponent<Text>().text = "0" + span.Minutes.ToString("F0") + ":";
            }
            else
            {
                tcounter.transform.GetComponent<Text>().text = span.Minutes.ToString("F0") + ":";
            }
            if (span.Seconds <= 9)
            {
                tcounterb.transform.GetComponent<Text>().text = "0" + span.Seconds;
            }
            else
            {
                tcounterb.transform.GetComponent<Text>().text = span.Seconds.ToString("F0");
            }

        }
        else
        {
            tcounter.SetActive(false);
        }
    }

    private void Awake()
    {
        
    }

    public void ShowAppnextAds()
    {
       
        interstitial.setButtonColor("#ffffff");
        interstitial.setButtonText("Install");    
        interstitial.setMute(false);
        interstitial.setAutoPlay(true);
        interstitial.setCreativeType(Interstitial.TYPE_MANAGED);
        interstitial.setOrientation(Ad.ORIENTATION_DEFAULT);
        interstitial.showAd();
    }
    void configads()
    {
          interstitial.onAdLoadedDelegate += onAdLoaded;
        //Get notified when the ad was clicked:
        interstitial.onAdClickedDelegate += onAdClicked;
         //Get notified when the ad was closed:
        
        //Get notified when an error occurred:
        interstitial.onAdErrorDelegate += onAdError;
    }
    // Update is called once per frame
    void onAdLoaded(Ad ads)
    {
        if(ads!=null)
        {
          //  info.text = "Loding..";
        }
    }
    void onAdClicked(Ad ads)
    {
        if (ads != null)
        {
          //  info.text = "Clicking..";
        }
    }
    void onAdError(Ad ad, string error)
    {
       // info.text = "Error:" + error;
    }
    float foo;
    void Update()
    {
        if (datetimer != "")
        {
            Invoke("timecounter", 5);

            date1 = DateTime.Parse(datetimer);
            DateTime predate1 = date1;
            DateTime currentdate1 = DateTime.Parse(DateTime.Now.ToString());

            if (currentdate1 >= predate1)
            {
                WATCH.SetActive(true);

                PlayerPrefs.SetFloat("appnextfoo", 0);
                PlayerPrefs.SetInt("appnextwatch", 0);
                GiftDataReference.UpdateRemaningAD();
            }
            else
            {

                foo += valueToIncreaseEverySec * Time.deltaTime;

                PlayerPrefs.SetFloat("appnextfoo", foo);
                WATCH.SetActive(false);

            }
        }

    }
    IEnumerator hidenotify()
    {
        yield return new WaitForSeconds(3);
        // GameObject.Find("Canvas/Notify/NetworkErrorMessage").SetActive(false);
        tost.SetActive(false);
    }
}
