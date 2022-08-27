using ChartboostSDK;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Networking;
using UnityEngine.UI;
public class ChatBoostTimer : MonoBehaviour
{
    public static DateTime date1;
    public Gift GiftDataReference;
    public GameObject tcounter, tcounterb, WATCH;
   // public Slider TimerSlider;
    float foo;
    float valueToIncreaseEverySec = 1;
    public AudioSource GiftsSound;
    private GameObject UiReference;
    private string datetimer = "";
    public GameObject tost;
    // Start is called before the first frame update
    void Start()
    {

        UiReference = GameObject.Find("HANDLER");
        foo = PlayerPrefs.GetFloat("chartboostfoo");
       
        if (datetimer == "")
        {

            var url1 = AppLoad.BaseUrl + "Gettimer";
            string json1 = "{\"unique_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"sdkname\":\"Chartboost\",\"datetime\":\"" + DateTime.Now.ToString() + "\",\"earn\":\"10\"}";
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

        if (www.isNetworkError)
        {

            //GameObject tost = GameObject.Find("Canvas/Notify/NetworkErrorMessage");
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




    void timecounter()
    {
        DateTime dddd = DateTime.Parse(DateTime.Now.ToString());
     
        if (DateTime.Parse(datetimer) > dddd)
        {

            TimeSpan span = DateTime.Parse(datetimer).Subtract(dddd);
            tcounter.SetActive(true);
           
            if (span.Minutes <= 9)
            {
                tcounter.transform.GetComponent<Text>().text = "0" + span.Minutes.ToString("F0") + ":";
            }
            else
            {
                tcounter.transform.GetComponent<Text>().text =  span.Minutes.ToString("F0") + ":";
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

    void Update()
    {
        if(datetimer!="")
        {
            Invoke("timecounter", 5);
           // if (PlayerPrefs.GetString("chartboostdate") == "")
           
             date1 = DateTime.Parse(datetimer);
            DateTime predate1 = date1;
            DateTime currentdate1 = DateTime.Parse(DateTime.Now.ToString());

            if (currentdate1 >= predate1)
            {

                WATCH.SetActive(true);
               
                PlayerPrefs.SetFloat("chartboostfoo", 0);
                PlayerPrefs.SetInt("chartboostwatch", 0);
                GiftDataReference.UpdateRemaningAD();
            }
            else
            {
               
                foo += valueToIncreaseEverySec * Time.deltaTime;
               
                PlayerPrefs.SetFloat("chartboostfoo", foo);
                WATCH.SetActive(false);

            }
        }
       

    }

    public IEnumerator PostRequestCoroutine(string url, string json)
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
                PlayerPrefs.SetFloat("Total", PlayerPrefs.GetFloat("Total") + Ui.vamount);
               // GameObject tost = GameObject.Find("Canvas/Notify/NetworkErrorMessage");
                tost.transform.GetChild(0).GetComponent<Text>().text = "+"+Ui.vamount+" Coins";
                tost.SetActive(true);
                StartCoroutine("hidenotify");
               
                GiftsSound.Play();
                PlayerPrefs.SetInt("chartboostwatch", 1);
                DateTime dd = DateTime.Parse(DateTime.Now.ToString());
                DateTime newdate = dd.AddMinutes(30);
                // PlayerPrefs.SetString("chartboostdate", newdate.ToString());
                datetimer = newdate.ToString();
                GiftDataReference.UpdateRemaningAD();
                
                Ui.LockscreenNum += 1;
                if (PlayerPrefs.GetInt("lockvalue") == 0)
                {
                    UiReference.GetComponent<Ui>().CongrateYouUnlcokApp();
                }
                var url1 = AppLoad.BaseUrl + "Addtimer";
                string json1 = "{\"unique_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"sdkname\":\"Chartboost\",\"datetime\":\"" + datetimer + "\"}";
                StartCoroutine(PostAddtimerCoroutine(url1, json1));
            }

            else
            {
               // GameObject tost = GameObject.Find("Canvas/Notify/NetworkErrorMessage");
                tost.transform.GetChild(0).GetComponent<Text>().text = "Somthing going wrong!";
                tost.SetActive(true);
                StartCoroutine("hidenotify");
              
            }
          
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
               // GameObject tost = GameObject.Find("Canvas/Notify/NetworkErrorMessage");
                tost.transform.GetChild(0).GetComponent<Text>().text = "Somthing going wrong2!";
                tost.SetActive(true);
                StartCoroutine("hidenotify");
               
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

