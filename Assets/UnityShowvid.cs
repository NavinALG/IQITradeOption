using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UnityShowvid : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject tcounter, tcounterb, WATCH;
    public static DateTime date1;
    public Gift GiftDataReference;
   //public Slider TimerSlider;
    float foo;
    float valueToIncreaseEverySec=1;
    public AudioSource GiftsSound;
    private GameObject UiReference;
    private string datetimer="";
    public GameObject loading;
    public GameObject tost;
    void Start()
    {
        // tost.SetActive(false);
        UiReference = GameObject.Find("HANDLER");
        foo = PlayerPrefs.GetFloat("unityfoo");
        Debug.Log(UiReference.name);
        //TimerSlider.gameObject.SetActive(false);
        if (datetimer == "")
        {
            loading.SetActive(true);
            var url1 = AppLoad.BaseUrl + "Gettimer";
            string json1 = "{\"unique_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"sdkname\":\"UnityAds\",\"datetime\":\"" + DateTime.Now.ToString() + "\",\"earn\":\"10\"}";
            StartCoroutine(PosttimerCoroutine(url1, json1));
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
        loading.SetActive(false);
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

    public void ShowvideoAd()
    {
        if (Advertisement.IsReady())
        {

            var option = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", option);

        }



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
                PlayerPrefs.SetFloat("Total", PlayerPrefs.GetFloat("Total") + Ui.vamount );
                //  GameObject tost = GameObject.Find("Canvas/Notify/NetworkErrorMessage");
                tost.transform.GetChild(0).GetComponent<Text>().text = "+"+Ui.vamount+"Coins";
                tost.SetActive(true);
                StartCoroutine("hidenotify");
               // Invoke("timecounter", 10);
                GiftsSound.Play();
                PlayerPrefs.SetInt("unitywatch", 1);
                DateTime dd = DateTime.Parse(DateTime.Now.ToString());
                DateTime newdate = dd.AddMinutes(30);
                // PlayerPrefs.SetString("unitydate", newdate.ToString());
                datetimer = newdate.ToString();
                GiftDataReference.UpdateRemaningAD();
                Ui.LockscreenNum += 1;
                if (PlayerPrefs.GetInt("lockvalue") == 0)
                {
                    UiReference.GetComponent<Ui>().CongrateYouUnlcokApp();
                }
                var url1 = AppLoad.BaseUrl + "Addtimer";
                string json1 = "{\"unique_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"sdkname\":\"UnityAds\",\"datetime\":\"" + datetimer + "\",\"earn\":\"10\"}";
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

            // GameObject tost = GameObject.Find("Canvas/Notify/NetworkErrorMessage");
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

            }

            else
            {
                //  GameObject tost = GameObject.Find("Canvas/Notify/NetworkErrorMessage");
                tost.transform.GetChild(0).GetComponent<Text>().text = "Somthing going wrong!";
                tost.SetActive(true);
                StartCoroutine("hidenotify");
                // TimerSlider.gameObject.SetActive(false);
            }

        }

    }


    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Ad Finished");
                //TotalCoins.staticInstance.AddCoins(100);
                var url1 = AppLoad.BaseUrl + "Updatevideo";
                string json1 = "{\"user_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"video_name\":\"UnityAds\",\"type\":\"1\",\"earn\":\"" + Ui.vamount + "\"}";
                StartCoroutine(PostRequestCoroutine(url1, json1));

                break;
            case ShowResult.Skipped:
                Debug.Log("Ad Skip");
                break;
            case ShowResult.Failed:
                Debug.Log("Ad Fail");
                break;
        }
    }
    // Update is called once per frame
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
                // TimerSlider.gameObject.SetActive(false);
                PlayerPrefs.SetFloat("unityfoo", 0);
                PlayerPrefs.SetInt("unitywatch", 0);
                GiftDataReference.UpdateRemaningAD();
            }
            else
            {
                // TimerSlider.gameObject.SetActive(true);
                foo += valueToIncreaseEverySec * Time.deltaTime;

                PlayerPrefs.SetFloat("unityfoo", foo);
                //Debug.Log(foo);
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



