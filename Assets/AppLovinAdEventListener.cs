
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;
using System;


using UnityEngine;

public class AppLovinAdEventListener : MonoBehaviour
{
    public GameObject tcounter,tcounterb, WATCH;
    public static DateTime date1;
    public Gift GiftDataReference;
    //public Slider TimerSlider;
    float foo;
    float valueToIncreaseEverySec = 1;
    public AudioSource GiftsSound;
    private GameObject UiReference;
    private string datetimer = "";
    public GameObject tost;
    void Start()
    {
       
      
            
        UiReference = GameObject.Find("HANDLER");
        foo = PlayerPrefs.GetFloat("AoolovinFoo");
        if (datetimer == "")
        {

            var url1 = AppLoad.BaseUrl + "Gettimer";
            string json1 = "{\"unique_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"sdkname\":\"AppLovin\",\"datetime\":\"" + DateTime.Now.ToString() + "\",\"earn\":\"10\"}";
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

          //  GameObject tost = GameObject.Find("Canvas/Notify/NetworkErrorMessage");
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
               // GameObject tost = GameObject.Find("Canvas/Notify/NetworkErrorMessage");
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
       
        if (DateTime.Parse(datetimer) > dddd)
        {

            TimeSpan span = DateTime.Parse(datetimer).Subtract(dddd);
            tcounter.SetActive(true);
            
            if (span.Minutes <= 9)
            {
                tcounter.transform.GetComponent<Text>().text = "0" + span.Minutes.ToString("F0")+":";
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
           
            if (jsonNode["status"] == "0")
            {
                // GameObject tost = GameObject.Find("Canvas/Notify/NetworkErrorMessage");
             
                tost.transform.GetChild(0).GetComponent<Text>().text ="+"+Ui.vamount+" Coins";
                tost.SetActive(true);
                StartCoroutine("hidenotify");
               // Invoke("watchinvoke", 10);
                GiftsSound.Play();
                PlayerPrefs.SetInt("watch", 1);
                DateTime dd = DateTime.Parse(DateTime.Now.ToString());
                DateTime newdate = dd.AddMinutes(30);
                // PlayerPrefs.SetString("date", newdate.ToString());
                datetimer = newdate.ToString();
                GiftDataReference.UpdateRemaningAD();
               
               // Ui.LockscreenNum += 1;
               
                var url1 = AppLoad.BaseUrl + "Addtimer";
                string json1 = "{\"unique_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"sdkname\":\"AppLovin\",\"datetime\":\"" + datetimer + "\"}";
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
    private void Update()
    {
        if(datetimer!="")
        {
            Invoke("timecounter", 5);
           
            date1 = DateTime.Parse(datetimer);
            DateTime predate1 = date1;
            DateTime currentdate1 = DateTime.Parse(DateTime.Now.ToString());

            if (currentdate1 >= predate1)
            {
                WATCH.SetActive(true);
               
                PlayerPrefs.SetFloat("AoolovinFoo", 0);
                PlayerPrefs.SetInt("watch", 0);
                GiftDataReference.UpdateRemaningAD();
            }
            else
            {
                
                foo += valueToIncreaseEverySec * Time.deltaTime;
               
                PlayerPrefs.SetFloat("AoolovinFoo", foo);
                WATCH.SetActive(false);

            }
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

           
        }

        if (string.Equals(ev, "DISPLAYEDINTER") || string.Equals(ev, "VIDEOBEGAN"))
        {
        
           
        }

        if (string.Equals(ev, "HIDDENINTER") || string.Equals(ev, "VIDEOSTOPPED"))
        {
         
            var url1 = AppLoad.BaseUrl + "Updatevideo";
            string json1 = "{\"user_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"video_name\":\"AppLovin\",\"type\":\"1\",\"earn\":\""+Ui.vamount+ "\"}";
            StartCoroutine(PostRequestCoroutine(url1, json1));
            PlayerPrefs.SetFloat("Total", PlayerPrefs.GetFloat("Total") + Ui.vamount);
          
        }

        if (string.Equals(ev, "LOADEDINTER"))
        {
           
        }
        if (string.Equals(ev, "LOADFAILED"))
        {
           
        }
    }
    IEnumerator hidenotify()
    {
        yield return new WaitForSeconds(3);
        // GameObject.Find("Canvas/Notify/NetworkErrorMessage").SetActive(false);
        tost.SetActive(false);
    }
}
