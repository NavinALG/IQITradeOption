using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using VoxelBusters;
using VoxelBusters.NativePlugins;
using ChartAndGraph;
//using System;

//using System;

public class AppLoad : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject login, circle, main, msg, referralpanel, TradeDataOriginal,RateusPanel;
    public Text TotalAmounttxt, investAmounttxt, timestxt;
    private JSONNode walletinfo;
    public static bool click;
    public static float value = 0, time = 1;
    public static int id;
    public static float inc_amt=0, TimeOur;
    public InputField referraltxt;
    public GameObject checkpiont;
    public static float Rno, sendRno;
    public static string clickvalue;
    public Sprite Red, Green;
    public AudioSource Tapsound;
    public static int TradeNumCount = 0, TradeNumcountForOneDay;
    private GameObject UIreferencce;
    public GraphChart IsActiveeee;

    public Text TextPinData;
    public Sprite upicon, downicon;
    float cointake = 0;
    public GameObject Onetimepanel, SubscriptionPanel,PurchasePanel,PremiumPanel,Leftmenu,Salebutton,Starterbutton;
    public Image shopicon;
    public static string BaseUrl = "http://gamelovinstudio.com/Trading/Userprocess/";

    // float a = 190.6448f, b = 448.9498f;
    public void ShowSubscriptionPanel()
    {
        Onetimepanel.SetActive(false);
        SubscriptionPanel.SetActive(true);
        circle.SetActive(true);
        var url = BaseUrl+"walletDetails";
        string json = "{\"email\":\"" + PlayerPrefs.GetString("email") + "\"}";
        StartCoroutine(PostRequestCoroutine(url, json));

        var url3 = BaseUrl + "GetInappSale";
        string json3 = "{\"name\":\"Sale\"}";
        StartCoroutine(PostRequestSaleCoroutine(url3, json3));
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
        circle.SetActive(false);
        if (www.isNetworkError)
        {


            /* msg.transform.GetChild(0).GetComponent<Text>().text = "Network error!";
             msg.SetActive(true);
             StartCoroutine("hidenotification");*/
        }
        else
        {
            JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());

            if (jsonNode["status"] == "0")
            {
                Debug.Log("Starterkitamount"+ jsonNode["startertkit"]);
               if(int.Parse(jsonNode["startertkit"])==0)
                {
                    Starterbutton.SetActive(true);
                }
               else
                {
                    Starterbutton.SetActive(false);
                }

            }
            else
            {

                Starterbutton.SetActive(false);
            }

        }

    }

    private IEnumerator PostRequestSaleCoroutine(string url, string json)
    {
        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(json);

        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();

        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";

        UnityWebRequest www =
            new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        yield return www.SendWebRequest();
        circle.SetActive(false);
        if (www.isNetworkError)
        {

        }
        else
        {

            JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());
            // Debug.Log(jsonNode["status"]+"and"+ jsonNode["message"]);
            if (jsonNode["status"] == "0")
            {
                System.DateTime lastdate = System.DateTime.Parse(jsonNode["end"]);
                System.DateTime todaydate = System.DateTime.Parse(System.DateTime.Now.ToString("yyyy-MM-dd"));
                int comp = System.DateTime.Compare(lastdate, todaydate);
                Debug.Log("CompareDate:" + comp + "And The Date:" + lastdate + "current:" + todaydate);
                if (comp >= 0)
                {
                    Salebutton.SetActive(true);
                }
                else
                {
                    Salebutton.SetActive(false);
                }



            }

            else
            {
                Salebutton.SetActive(false);

            }
            //  Debug.Log(www.downloadHandler.text);
        }

    }
    public void ShowOnetimePanel()
    {
        Onetimepanel.SetActive(true);
        SubscriptionPanel.SetActive(false);
    }
    public void ShowPurchasePanel(string name)
    {
        Leftmenu.SetActive(false);
        PremiumPanel.SetActive(false);
        Color Yellow = new Color32(14, 102, 130, 255);
        shopicon.color = Yellow;
        PurchasePanel.SetActive(true);
        if (name== "Subscription")
        {
          
            PurchasePanel.gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            PurchasePanel.gameObject.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            PurchasePanel.gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Text>().text = "Subscription";
        }
        else
        {
            PurchasePanel.gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            PurchasePanel.gameObject.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            PurchasePanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Text>().text = "OneTimePurchase";
        }
       
       

    }
    void Start()
    {
        
       // PlayerPrefs.SetString("email","navinca.18@gmail.com");
      //  PlayerPrefs.SetInt("Login",1);
        UIreferencce = GameObject.Find("HANDLER");
       
       

    }

    public void skipreferrals()
    {
        PlayerPrefs.SetInt("Referralsvalue", 1);
        referralpanel.SetActive(false);
    }
    public void updatereferrals()
    {
        var url = BaseUrl + "updatereferrals";
        string json = "{\"user_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"referral_code\":\"" + referraltxt.text + "\"}";

        StartCoroutine(Postreferrals(url, json));
    }
    private IEnumerator Postreferrals(string url, string json)
    {
        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(json);

        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();

        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";

        UnityWebRequest www =
            new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        yield return www.SendWebRequest();
        circle.SetActive(false);
        if (www.isNetworkError)
        {
           

            msg.transform.GetChild(0).GetComponent<Text>().text = "Network error!";
            msg.SetActive(true);
            StartCoroutine("hidenotification");
        }
        else
        {
            JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());
            // Debug.Log(jsonNode["status"]+"and"+ jsonNode["message"]);
            if (jsonNode["status"] == "0")
            {
                PlayerPrefs.SetInt("Referralsvalue", 1);
               
                msg.transform.GetChild(0).GetComponent<Text>().text = "Success";
                msg.SetActive(true);
                StartCoroutine("hidenotification");


                referralpanel.SetActive(false);
                UIreferencce.GetComponent<Ui>().LockScreen();
            }
            else
            {
              
                msg.transform.GetChild(0).GetComponent<Text>().text = "Referral code not found";
                msg.SetActive(true);
                StartCoroutine("hidenotification");
            }
            //  Debug.Log(www.downloadHandler.text);
        }

    }
    IEnumerator hidenotification()
    {
        yield return new WaitForSeconds(2);
        msg.SetActive(false);
    }
    private void sharesheet()
    {
        ShareSheet _sharesheet = new ShareSheet();
        _sharesheet.Text = "Hi, I am earning free paypal and paytm cash upto 1000$ daily. You can also earn, Download this amazing app & Use my referral ID W4D7CEF3 and Get Free Bonus Coins, Download app now ";
        //_sharesheet.AttachImage(textur);
        _sharesheet.URL = "http://freepaytmcash.freecashier.online/r/?W4D7CEF3";
        NPBinding.Sharing.ShowView(_sharesheet, finishshare);


        /////HAS TO BE ACTIVE WHEN BUILD 

        ///////look here once
        //////////?????????????????????????????????????????????????????????????????????????????????????????????????????????

    }
    private void finishshare(eShareResult _result)
    {
        Debug.Log(_result);
    }
    public string ActionSaveTemp;
    Vector2 v2;
    float v3;
    public float offset;
    private string Action1;
    GameObject TradePrefab;
    public static float send1, send2, at = 0;
   int rcount=0, rateno=2;




    //public InterstitialAdScene interAds;
    public void TackAction(string action)
    {
       
        rcount++;
        Action1 = action;

       // interAds.LoadInterstitial();

        //TradeNumcountForOneDay += 1;
        Handheld.Vibrate();
        value = inc_amt;
        //Debug.Log(TradeNumCount);
        ActionSaveTemp = action;
        StartCoroutine("isactiveEnum");
        if (value > 0 && PlayerPrefs.GetFloat("Total") >= value)
        {
          
            
          
            string maxtime;
            if (time == 30)
            {
                maxtime = time.ToString() + "sec";
            }
            else
            {
                maxtime = time.ToString() + "min";

            }


          if(at==0)
            {


                if (action == "UP")
                {
                    if (GraphChart.Yvalue <= -30)
                    {
                        float rvalue1 = Random.Range(-29.342f, 0);
                        send1 = rvalue1;
                        sendRno = rvalue1;
                        v3 = rvalue1;
                        v2 = new Vector2(0, rvalue1);
                    }
                    else
                    {

                        send1 = GraphChart.Yvalue;
                        sendRno = GraphChart.Yvalue;
                        v2 = new Vector2(0, GraphChart.Yvalue+offset);
                        v3 = GraphChart.Yvalue;
                    }
                }
                else
                {
                    if (GraphChart.Yvalue >= 50)
                    {
                        float value = Random.Range(0.435f, 50);
                        send1 = value;
                        sendRno = value;
                        v3 = value;
                        v2 = new Vector2(0, value);
                    }
                    else
                    {

                        send1 = GraphChart.Yvalue;
                        sendRno = GraphChart.Yvalue;
                        v3 = GraphChart.Yvalue;
                        v2 = new Vector2(0, GraphChart.Yvalue + offset);
                    }
                }

                at = 1;
            }
          else
            {
                if (action == "UP")
                {
                    if (GraphChart.Yvalue <= -30)
                    {
                        float rvalue2 = Random.Range(-29.342f,0);
                        send2 = rvalue2;
                        sendRno = rvalue2;
                        v3 = rvalue2;
                        v2 = new Vector2(0, rvalue2);

                    }
                    else
                    {

                        send2 = GraphChart.Yvalue;
                        sendRno = GraphChart.Yvalue;
                        v3 = GraphChart.Yvalue;
                        v2 = new Vector2(0, GraphChart.Yvalue + offset);
                    }
                }
                else
                {
                    if (GraphChart.Yvalue >= 50)
                    {
                        float value3 = Random.Range(0.435f,50);
                        send2 = value3;
                        sendRno = value3;
                        v3 = value3;
                        v2 = new Vector2(0, value3);
                    }
                  
                    else
                    {

                        send2 = GraphChart.Yvalue;
                        sendRno = GraphChart.Yvalue;
                        v3 = GraphChart.Yvalue;
                        v2 = new Vector2(0, GraphChart.Yvalue + offset);
                    }
                }

              
                at = 0;
            }
           
            var url = BaseUrl + "AddMyTrades";
            string json = "{\"email\":\"" + PlayerPrefs.GetString("email") + "\",\"startpoint\":\"" + v3 + "\",\"invest\":\"" + value + "\",\"maxminutes\":\"" + maxtime + "\",\"actiontime\":\"" + System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "\",\"action\":\"" + action + "\"}";
            StartCoroutine(Posttrade(url, json, action));

            var url2 = BaseUrl+"tradecountlimit";
            string json2 = "{\"unique_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"date\":\"" + System.DateTime.Now.ToString("MM/dd/yyyy") + "\"}";
            StartCoroutine(PostRequestcounttradeCoroutine(url2, json2));


            checkpiont.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = inc_amt.ToString("F0");

        }
        else
        {
            
            msg.transform.GetChild(0).GetComponent<Text>().text = "Increase the amount!";
            msg.SetActive(true);
            StartCoroutine("hidenotification");
        }


    }

    private IEnumerator PostRequestcounttradeCoroutine(string url, string json)
    {
        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(json);

        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();

        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";

        UnityWebRequest www =
            new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        yield return www.SendWebRequest();
        circle.SetActive(false);
        if (www.isNetworkError)
        {

        }
        else
        {

            JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());
            // Debug.Log(jsonNode["status"]+"and"+ jsonNode["message"]);
            if (jsonNode["status"] == "0")
            {
                PlayerPrefs.SetInt("tcount", int.Parse(jsonNode["count"]));
               


            }

            else
            {


            }
          
        }

    }
    private IEnumerator Posttrade(string url, string json,string action)
    {
        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(json);

        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();

        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";

        UnityWebRequest www =
            new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        yield return www.SendWebRequest();
        // circle.SetActive(false);
        if (www.isNetworkError)
        {
           

            msg.transform.GetChild(0).GetComponent<Text>().text = "Network error!";
            msg.SetActive(true);
            StartCoroutine("hidenotification");


        }
        else
        {
            JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());
          
            if (jsonNode["status"] == "0")
            {
                //Debug.Log("trades Working");
                PlayerPrefs.SetFloat("Total", PlayerPrefs.GetFloat("Total") - value);
                TradeNumCount += 1;
                id = jsonNode["id"];
                clickvalue = action;
                GameObject point = Instantiate(checkpiont);
                //Debug.Log(value);
                point.transform.SetParent(GameObject.Find("Canvas/MainUI/GraphHolder/CheckpointSpawn").transform);
                point.transform.localPosition = v2;
                point.transform.localScale = new Vector3(1, 1, 1);
                point.GetComponent<checkpoint>().Timewhentradeapply = time;
               
                if (action == "UP")
                {
                    Color green = new Color32(25, 163, 3, 255);
                   
                    point.transform.GetChild(1).GetComponent<Image>().sprite = Red;
                    point.transform.GetChild(2).GetComponent<Image>().color = green;

                   


                }
                else
                {
                    Color red = new Color32(204, 4, 4, 255);
                    point.transform.GetChild(1).GetComponent<Image>().sprite = Green;
                    point.transform.GetChild(2).GetComponent<Image>().color = red;
                }



                TradePrefab = Instantiate(TradeDataOriginal);
              
                TradePrefab.transform.SetParent(GameObject.Find("Canvas/MainUI/ActiveTradeData/Content").transform);
                TradePrefab.transform.localScale = new Vector3(1, 1, 1);
                TradePrefab.transform.GetChild(1).GetComponent<Text>().text = sendRno.ToString();
                if (action == "UP")
                {
                    TradePrefab.transform.GetChild(0).GetComponent<Image>().sprite = upicon;
                    Color green = new Color32(25, 163, 3, 255);
                    TradePrefab.GetComponent<Image>().color = green;
                }
                else
                {
                    TradePrefab.transform.GetChild(0).GetComponent<Image>().sprite = downicon;
                    Color red = new Color32(204, 4, 4, 255);
                    TradePrefab.GetComponent<Image>().color = red;
                }
                TradePrefab.GetComponent<ActiveTradeTimerScript>().TimeGiven =TimeOur;
               
                if (PlayerPrefs.GetInt("Rateus") == 0)
                {
                    if (rcount == rateno * 2)
                    {
                        RateusPanel.SetActive(true);
                        rateno =rcount;
                    }
                }
            }
            else
            {
              

                msg.transform.GetChild(0).GetComponent<Text>().text = "Something going wrong!";
                msg.SetActive(true);
                StartCoroutine("hidenotification");
            }
          
        }

    }

    public void increaseinvest()
    {
      Tapsound.Play();
        
       
            cointake = Ui.max_take;
       
        if (inc_amt >= Ui.min_take && inc_amt < cointake && inc_amt+5<=PlayerPrefs.GetFloat("Total"))
        {
            if (inc_amt < 100 && inc_amt <PlayerPrefs.GetFloat("Total"))
            {
                inc_amt += 5;
            }
            else if (inc_amt > 99 && inc_amt < 200 && inc_amt+10 <= PlayerPrefs.GetFloat("Total"))
            {
                inc_amt += 10;
            }
            else if(inc_amt>199 && inc_amt+20 <= PlayerPrefs.GetFloat("Total"))
            {
                inc_amt += 20;
            }
            else
            {
               
            }

        }
        else
        {
            Debug.Log("ok ");
        }
    }
    public void decreaseinvest()
    {
        Tapsound.Play();
        

            cointake = Ui.max_take;
      
        if (inc_amt > Ui.min_take && inc_amt <= cointake)
        {
            if (inc_amt <= 100)
            {
                inc_amt -= 5;
            }
            else if (inc_amt > 99 && inc_amt < 200)
            {
                inc_amt -= 10;
            }
            else
            {
                inc_amt -= 20;
            }

        }
        else
        {
            Debug.Log("ok ");
        }
    }
    public void increasetime()
    {
        Tapsound.Play();
        if (time < 2 && time >= 1)
        {
            time += 1;
        }

        else if (time == 30)
        {
            time = 1;
        }
    }
    public void Decreasetime()
    {
        Tapsound.Play();
        if (time > 1 && time < 3)
        {
            time -= 1;
        }
        else if (time == 1)
        {
            time = 30;
        }



    }

    // Update is called once per frame
    void Update()
    {


        if (PlayerPrefs.GetFloat("Total", 0) > 0)
        {
           Ui.IscoinAvailable = true;
        }
        TimeOur = time;
        if (PlayerPrefs.GetInt("Login") == 1)
        {
            if (!main.activeSelf)
            {
                main.SetActive(true);
                login.SetActive(false);
            }
            if (login.activeSelf)
            {

                login.SetActive(false);
            }
        }
        else
        {
           
            if (main.activeSelf)
            {
                main.SetActive(false);

            }
            if (!login.activeSelf)
            {

                login.SetActive(true);
            }
        }
        if (main.activeSelf)
        {
            TotalAmounttxt.text = PlayerPrefs.GetFloat("Total", 0).ToString();
            Rno = Random.Range(0, 1000);
            investAmounttxt.text = inc_amt.ToString();
            if (time == 30)
            {
                timestxt.text = time.ToString() + " sec";
            }
            else
            {
                timestxt.text = time.ToString() + " min";
            }
           /* if (inc_amt > PlayerPrefs.GetFloat("Total"))
            {
                inc_amt = Ui.min_take;
            }*/

        }

    }

    IEnumerator isactiveEnum()
    {
        IsActiveeee.IsActiveGraph = true;
        yield return new WaitForSeconds(1);
        IsActiveeee.IsActiveGraph = false;
    }






}

