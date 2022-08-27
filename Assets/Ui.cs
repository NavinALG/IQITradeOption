using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class Ui : MonoBehaviour
{

    public GameObject AlertPanel, HistoryPanel, GiftsPanel, ProfilePanel, LoginPanel, loadingalert, NetworkErrorMessage, PaypalPanel, PaytmPanel, Historydata,
        alertdata,  TradeDataOriginal,NetworkError,btnarrowleft, btnarrowright;
    public Image AlertReplace, HistoryReplace, GiftsReplace, ProfileReplacce,ShopRepalce;
    public Image GetchildOfAlert, GetchildOfProfile, GetchildOfgifts, GetchildOfHistory;
    public Animator AlertDownPanels, ProfileDownPanel, GiftDownPanel, HistoryDownPanel;
    public GameObject HistoryItemReference,purchasePanel;
    //SETTINGS 
    public Animator dailyNotify;
    public Image ToggleButton, DailyButton;
    public Image MaleSprite, FemaleSprite;
    // Start is called before the first frame update

    //Left Panel area
    public GameObject LeftMenu, FadedAnimation;
    public Animator AnimLeftMenu;
    public float DropDownTimer;

    public Sprite upicon, downicon;
    /// <summary>
    /// TOUCH FIELD 
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    int final;
    public Text[] videocoinstxt;


    //Left Panel area
    public GameObject submitbuton, editbutton, loading;


    public InputField[] profilecontain;
    public Text Uid;
    private GameObject parentobj, parentobj1, scrollview;



    public GameObject TradePopUpUp, TradePopupDown,starterkit,starterbtn, noffertxt,salebtn;

    public float Auctionamount, AuctionTime;
    public Text AuctionText, AuctionTimeText, AuctionText2, AuctionTimeText2;
    public AppLoad IncrementAmount;
    //Left Panel area



    public Slider AudioVolume;
    public AudioSource Win, Tap;
    public Text TextPinData,Runningtxt;
    GameObject TradePrefab;
   public static bool IscoinAvailable = false;
    public GameObject CoinUnavailablePopup, TradesAlreadyRunningPopup, TradeOnedayLimitPopup;


    /// LOCK SCREEN FIRST TIME OPEN
   public GameObject LocakPopUpFirstTimeopen, LocakScreenContainer,CongratesScreen;
    public static int LockscreenNum = 0;
    public GameObject Referralearndata, EarningPanel, AlertNorecord,M,F,updatePanel;
   //public AdViewScene  Bannerads;
    float per = 80;
    int perdaylimit;

    public Text btntxtUp, btntxtDown,PopuptxtUp,PopupDown;
   public static int tradeno=1;
   
    public static float vamount=10,normalpayout,crown_coins,max_take,min_take,minearn;
    public void OpenStarterkit()
    {
        starterkit.SetActive(true);
    }
    public void CheckCrown()
    {
        string url5 = AppLoad.BaseUrl + "getsettings";
        string json1 = "{\"response\":\"Get\"}";
        StartCoroutine(PostgetsettingCoroutine(url5, json1));
    }
    void Start()
    {
      
        // PlayerPrefs.SetInt("Login", 1);
        // PlayerPrefs.SetString("email","navinca.18@gmail.com");
        // PlayerPrefs.SetString("profileicon", "https://lh5.googleusercontent.com/-mocArUfowIc/AAAAAAAAAAI/AAAAAAAABHA/9uQERpM9j8I/s96-c/photo.jpg");
        string url5 = AppLoad.BaseUrl + "getsettings";
        string json1 ="{\"response\":\"Get\"}";
        StartCoroutine(PostgetsettingCoroutine(url5, json1));
        


        AudioVolume.value = 0.4f;
        //AudioVolume.value = PlayerPrefs.GetInt("audio");
        GiftsPanel.SetActive(false);
        HistoryPanel.SetActive(false);
        AlertPanel.SetActive(false);
        ProfilePanel.SetActive(false);
        LeftMenu.SetActive(false);
        FadedAnimation.SetActive(false);
        TradePopUpUp.SetActive(false);
        TradePopupDown.SetActive(false);
        CoinUnavailablePopup.SetActive(false);
        TradesAlreadyRunningPopup.SetActive(false);
        TradeOnedayLimitPopup.SetActive(false);
       // LocakPopUpFirstTimeopen.SetActive(false);
        LockscreenNum = PlayerPrefs.GetInt("lockscreennum");
        
        DateTime dddd = DateTime.Parse(DateTime.Now.ToString());
     
     
    }
   
    public IEnumerator PostgetsettingCoroutine(string url, string json)
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
            Debug.LogError(string.Format("{0}: {1}", www.url, www.error));
         ///   error.text = string.Format("{0}: {1}", www.url, www.error);
            Debug.Log("Network Fail");
        }
        else
        {
            JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());
            // Debug.Log(jsonNode["status"]+"and"+ jsonNode["message"]);
            
            if(jsonNode["status"]=="0")
            {
                min_take= jsonNode["mintake"];
                AppLoad.inc_amt = jsonNode["mintake"];
                if(PlayerPrefs.GetInt("crown")==1)
                {
                    per= float.Parse(jsonNode["crown_per"]);
                    perdaylimit= int.Parse(jsonNode["crown_perday"]);
                    max_take = jsonNode["max_crowntake"];
                }
                else
                {
                    perdaylimit = int.Parse(jsonNode["normal_perday"]);
                    per = float.Parse(jsonNode["trade"]);
                    max_take = jsonNode["max_basictake"];
                }
               
                vamount= float.Parse(jsonNode["video_amount"]);
                for(int i=0;i< videocoinstxt.Length;i++)
                {
                    videocoinstxt[i].text = vamount.ToString();
                }
                crown_coins= float.Parse(jsonNode["crown_maxcoins"]);
                minearn= float.Parse(jsonNode["mini_earn"]);
                btntxtUp.text = per.ToString()+ "%";
                btntxtDown.text = per.ToString() + "%";
                PopuptxtUp.text = per.ToString() + "%";
                PopupDown.text = per.ToString() + "%";
                Debug.Log("udatevalue:"+ jsonNode["updatemondatory"]);
                if(jsonNode["updatemondatory"]=="true" && Application.version!= jsonNode["appversion"])
                {
                    updatePanel.SetActive(true); 
                }
                else
                {
                    updatePanel.SetActive(false);
                }
            }
            else
            {
                Debug.Log("fail to get setting");
            }
                
            
        }




    }
    public void AppUpdate()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.algmedia.tradeoption");
    }
    public void Rateus()
    {
        PlayerPrefs.SetInt("Rateus", 1);
        Application.OpenURL("market://details?id=com.algmedia.tradeoption");
      
    }
    public void closeapp()
    {
        Application.Quit();
    }
  public static  bool chechiap = false;
    public void Purchage()
    {
        AlertPanel.SetActive(false);
        ProfilePanel.SetActive(false);
        GiftsPanel.SetActive(false);
        HistoryPanel.SetActive(false);
        
        alert = true;
        Gifts = true;
        if (!chechiap)
        {
            
            Color Yellow1 = new Color32(255, 255, 255, 255);
            AlertReplace.color = Yellow1;
            GiftsReplace.color = Yellow1;
            Color Yellow = new Color32(14, 102, 130, 255);
            ShopRepalce.color = Yellow;

            purchasePanel.SetActive(false);
            purchasePanel.SetActive(true);
            chechiap = true;
            FadedAnimation.SetActive(true);
           
        }
        else
        {
           
            Color Yellow = new Color(255, 255, 255, 255);
            ShopRepalce.color = Yellow;
            purchasePanel.GetComponent<Animator>().SetTrigger("IAPDown");
            chechiap = false;
            FadedAnimation.SetActive(false);
           // StartCoroutine(closeiap());
        }
       
    }
    IEnumerator closeiap()
    {
        yield return new WaitForSeconds(1);
        purchasePanel.SetActive(false);
    }
    public void ReferEarnngPanel2()
    {
      
        loading.SetActive(true);
        var url = AppLoad.BaseUrl + "getReferralsearning";
        string json = "{\"id\":\"" + PlayerPrefs.GetString("uniqueid") + "\"}";
        StartCoroutine(PostRequestReferralsCoroutine(url, json));
    }
    public void closeearningpanel()
    {
        EarningPanel.GetComponent<Animator>().SetTrigger("close");
        EarningPanel.SetActive(false);
    }

    private IEnumerator PostRequestReferralsCoroutine(string url, string json)
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
          

            NetworkErrorMessage.transform.GetChild(0).GetComponent<Text>().text = "Network error!";
            NetworkErrorMessage.SetActive(true);
            StartCoroutine("hidenotification");
        }
        else
        {

            JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());
            // Debug.Log(jsonNode["status"]+"and"+ jsonNode["message"]);
            if (jsonNode["status"] == "0")
            {

                int i = 0;
                foreach (JSONNode item in jsonNode["info"])
                {

                    System.DateTime theTime;
                    string date, time;

                    if (item["date"] != null)
                    {
                        theTime = System.DateTime.Parse(item["date"]);
                        date = theTime.ToString("yyyy-MM-dd");
                        time = theTime.ToString("hh:mm:ss tt");
                    }
                    else
                    {
                        date = "";
                        time = "";
                    }

                    //  Debug.Log("ID:"+item["id"]);
                    GameObject obj = Instantiate(Referralearndata);
                    // obj.SetActive(true);
                    parentobj1 = GameObject.Find("Canvas/MainUI/ReferalEarning/Container");
                    obj.transform.SetParent(parentobj1.transform);
                    obj.transform.localScale = new Vector3(1, 1, 1);
                    obj.transform.GetChild(0).GetComponent<Text>().text = item["name"];
                    obj.transform.GetChild(1).GetComponent<Text>().text = item["status"];
                    obj.transform.GetChild(2).GetComponent<Text>().text = item["coins"];
                    obj.transform.GetChild(3).GetComponent<Text>().text = date;
                    obj.transform.GetChild(4).GetComponent<Text>().text = time;




                }

            }
            else
            {
               
                NetworkErrorMessage.transform.GetChild(0).GetComponent<Text>().text = "No record found";
                NetworkErrorMessage.SetActive(true);
                StartCoroutine("hidenotification");

            }
            //  Debug.Log(www.downloadHandler.text);
        }

    }


    public void LockScreen()
    {
       
       
       
        
    }
    private void Update()
    {
        if(n_value<1)
        {
            if(btnarrowleft.activeSelf)
            {
                btnarrowleft.SetActive(false);
            }

        }
        else
        {
            if (!btnarrowleft.activeSelf)
            {
                btnarrowleft.SetActive(true);
            }
        }

        Win.volume = AudioVolume.value;
        Tap.volume = AudioVolume.value;
        //PlayerPrefs.SetFloat("audio", AudioVolume.value);
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //Debug.Log("Error. Check internet connection!");
            NetworkError.SetActive(true);
        }
        else
        {
            NetworkError.SetActive(false);
        }

        if(starterbtn.activeSelf==false&&salebtn.activeSelf==false)
        {
            noffertxt.SetActive(true);
        }
        else
        {
            noffertxt.SetActive(false);
        }

    }

    
    void newUpdate()
    {
#if UNITY_ANDROID
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    Debug.Log("up swipe");
                }
                //swipe down
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    Debug.Log("down swipe");
                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    Debug.Log("left swipe");
                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    Debug.Log("right swipe");
                    LeftPanelOpen();
                }
            }
        }
#endif

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

          
            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                Debug.Log("right swipe");
                LeftPanelOpen();
            }
        }
#endif
    }
    //LeftMenuPanel
    public void LeftPanelOpen()
    {
        LeftMenu.SetActive(false);
        LeftMenu.SetActive(true);
        FadedAnimation.SetActive(true);
      //  ads.hidebanner();
    }
    public void LeftMenuClose()
    {
        FadedAnimation.SetActive(false);
        AnimLeftMenu.SetTrigger("leftmenuclose");
       
    }
    IEnumerator menu()
    {
        //Debug.Log("exit");
       
        FadedAnimation.SetActive(false);
     
        yield return new WaitForSeconds(0.5f);
        LeftMenu.SetActive(false);


    }


    //TradePopUp
    public void TradePoPupDisplayUP()
    {

      
        float amount = AppLoad.inc_amt + (AppLoad.inc_amt * per) / 100;
        AuctionTime = AppLoad.TimeOur;
        if(AuctionTime > 2)
        {
            AuctionTimeText.text = AuctionTime.ToString() + " sec";
            AuctionTimeText2.text = AuctionTime.ToString() + " sec";
        }
        else
        {
            AuctionTimeText.text = AuctionTime.ToString() + " min";
            AuctionTimeText2.text = AuctionTime.ToString() + " min";
        }
        
        AuctionText.text = amount.ToString();
        AuctionText2.text = amount.ToString();
        if (IscoinAvailable == false )
        {
            CoinUnavailablePopup.SetActive(true);
            return;
        }else
        {
            CoinUnavailablePopup.SetActive(false);
            TradePopUpUp.SetActive(true);
        }
       // Debug.Log(AppLoad.TradeNumCount);
       if(PlayerPrefs.GetInt("tcount") >= perdaylimit)
        {
            Runningtxt.text = "You can only put "+perdaylimit.ToString()+" in day. Please wait for next day.";
            TradesAlreadyRunningPopup.SetActive(true);
            TradePopUpUp.SetActive(false);
        }
       else if( AppLoad.TradeNumCount > tradeno)
        {
            Runningtxt.text = "You can only put "+ (tradeno +1)+ " trades at a time. Please wait for the trade to Complete.";
            TradesAlreadyRunningPopup.SetActive(true);
            TradePopUpUp.SetActive(false);
        }
        else
        {
            TradesAlreadyRunningPopup.SetActive(false);
        }

        
        //Debug.Log(amount);
       
    }
    public void TradePoPupDisplayDown()
    {
        float amount = AppLoad.inc_amt + (AppLoad.inc_amt * per) / 100;
        AuctionTime = AppLoad.TimeOur;
        if (AuctionTime > 2)
        {
            AuctionTimeText.text = AuctionTime.ToString() + " sec";
            AuctionTimeText2.text = AuctionTime.ToString() + " sec";
        }
        else
        {
            AuctionTimeText.text = AuctionTime.ToString() + " min";
            AuctionTimeText2.text = AuctionTime.ToString() + " min";
        }
        AuctionText.text = amount.ToString();
        AuctionText2.text = amount.ToString();

        //Debug.Log(amount);
        if (IscoinAvailable == false)
        {
            CoinUnavailablePopup.SetActive(true);
            return;
        }else
        {
            CoinUnavailablePopup.SetActive(false);
            TradePopupDown.SetActive(true);
        }
        // Debug.Log(AppLoad.TradeNumCount);
        if (PlayerPrefs.GetInt("tcount") >= perdaylimit)
        {
            Runningtxt.text = "You can only put " + perdaylimit.ToString() + " in day. Please wait for next day.";
            TradesAlreadyRunningPopup.SetActive(true);
            TradePopUpUp.SetActive(false);
        }
        else if(AppLoad.TradeNumCount > tradeno)
        {
            Runningtxt.text = "You can only put " + (tradeno + 1) + " trades at a time. Please wait for the trade to Complete.";

            TradesAlreadyRunningPopup.SetActive(true);
            TradePopupDown.SetActive(false);
        }
        else
        {
            TradesAlreadyRunningPopup.SetActive(false);
        }

    }


    //PROFILE PANEL
    public void ToogleMale()
    {
        ProfileDownPanel.SetTrigger("Male");

     //   ToggleButton.color = Color.red;



    }
    public void ToogleFeMale()
    {
        ProfileDownPanel.SetTrigger("Female");
      //  ToggleButton.color = Color.cyan;


    }
    bool Tooggeele = true;

    public void ToogleMaleFemale()
    {
        if(mf==true)
        {
        if (Tooggeele == true)
        {
            ToogleMale();
            Tooggeele = false;
            M.SetActive(true);
              F.SetActive(false);
        }
        else
        {

            ToogleFeMale();
            Tooggeele = true;
             M.SetActive(false);
              F.SetActive(true);

        }	
        }
        

    }



    //settings panel data
    bool notification = true;
    public void DailyNotification()
    {
        if (notification == true)
        {
            dailyNotify.SetTrigger("on");
            //DailyButton.color = Color.red;
            notification = false;
        }
        else
        {

            dailyNotify.SetTrigger("off");
            //DailyButton.color = Color.cyan;
            notification = true;

        }
    }





    //LOGIN SCREEEN
    public void LoginScreen()
    {
        LoginPanel.SetActive(true);
    }
    //
    /// <summary>
    /// NETWORK ERROR MESSAGE
    /// </summary>
    public void NetwrokMessage()
    {
        NetworkErrorMessage.SetActive(true);
    }


    //PAYPAL AND PAYTM POPUP
    public void Paypal()
    {
        PaypalPanel.SetActive(true);
    }
    public void PaypalOff()
    {
        StartCoroutine("payapEnum");
    }
    IEnumerable payapEnum()
    {
        PaypalPanel.GetComponent<Animator>().SetTrigger("Proceed");
        yield return new WaitForSeconds(1);
        PaypalPanel.SetActive(true);
    }
    public void Paytm()
    {
        PaytmPanel.SetActive(true);
    }
    public void PaytmOff()
    {
        StartCoroutine("payapEnum");
    }
    IEnumerable PaytmEnum()
    {
        PaytmPanel.GetComponent<Animator>().SetTrigger("Proceed");
        yield return new WaitForSeconds(1);
        PaytmPanel.SetActive(true);
    }
    //FOOTER AREA WORKING
    bool alert = true;
    public void Alert()
    {

        if (alert == true)
        {
            FadedAnimation.SetActive(true);
            AlertPanel.SetActive(false);
            ProfilePanel.SetActive(false);
            GiftsPanel.SetActive(false);
            HistoryPanel.SetActive(false);
            AlertPanel.SetActive(true);
            purchasePanel.SetActive(false);
            chechiap = false;
            // ResetCOLOR();
            //  Color Yellow = new Color32(255, 199, 0, 255);
            Color Yellow1 = new Color32(255, 255, 255, 255);
            ShopRepalce.color = Yellow1;
            GiftsReplace.color = Yellow1;
            Color Yellow = new Color32(14, 102, 130, 255);
           AlertReplace.color = Yellow;
           // GetchildOfAlert.color = white;
            alert = false;
            loadingalert.SetActive(true);
            var url = AppLoad.BaseUrl + "GetActiveTrades";
            string json = "{\"email\":\"" + PlayerPrefs.GetString("email") + "\"}";
            StartCoroutine(PostRequestAlertCoroutine(url, json));
        }
        else
        {
            FadedAnimation.SetActive(false);
            GameObject parentobj2 = GameObject.Find("Canvas/MainUI/ActiveTrade/data/Container");
            if (parentobj2.transform.childCount > 0)
            {
                for (int i = 0; i < parentobj2.transform.childCount; i++)
                {
                    GameObject.Destroy(parentobj2.transform.GetChild(i).transform.gameObject);
                    //Debug.Log("deleted");
                }
               

            }

            Color white = new Color32(255, 255, 255, 255);
            AlertReplace.color = white;
          //  Color white = new Color32(255, 199, 0, 255);

           // GetchildOfAlert.color = white;
            AlertDownPanels.SetTrigger("down");
            alert = true;

        }

    }
    [SerializeField]
  //  private scrollrect Scroll;
    bool History = true;
    void OnFillItem(int index, GameObject item)
    {
        item.GetComponentInChildren<Text>().text = index.ToString();
    }

    int OnHeightItem(int index)
    {
        return 150;
    }
    int n_value = 0;
    public void Nextradehist()
    {
        GameObject parentobj3 = GameObject.Find("Canvas/MainUI/MyTrade/Container");
        if (parentobj3.transform.childCount > 0)
        {
            for (int i = 0; i < parentobj3.transform.childCount; i++)
            {
                GameObject.Destroy(parentobj3.transform.GetChild(i).transform.gameObject);
                //  Debug.Log("deleted");
            }
          
        }
        
        n_value++;
        if(n_value > -1)
        {
            if (!btnarrowleft.activeSelf)
            {
                btnarrowleft.SetActive(true);
            }
            var url = AppLoad.BaseUrl + "GetCloseTrades";
            string json = "{\"email\":\"" + PlayerPrefs.GetString("email") + "\",\"offset\":\"" + n_value + "\"}";
            StartCoroutine(PostRequestHistoryCoroutine(url, json));
        }
       
       
    }
    public void Prevtradehist()
    {
        GameObject parentobj3 = GameObject.Find("Canvas/MainUI/MyTrade/Container");
        if (parentobj3.transform.childCount > 0)
        {
            for (int i = 0; i < parentobj3.transform.childCount; i++)
            {
                GameObject.Destroy(parentobj3.transform.GetChild(i).transform.gameObject);
                //  Debug.Log("deleted");
            }
          

        }
        if (n_value<1)
        {
            n_value = 0;
        }
        else
        {
            n_value--;
        }
        if(n_value>-1)
        {
            if(!btnarrowright.activeSelf)
            {
                btnarrowright.SetActive(true);
            }
           
            var url = AppLoad.BaseUrl + "GetCloseTrades";
            string json = "{\"email\":\"" + PlayerPrefs.GetString("email") + "\",\"offset\":\"" + n_value + "\"}";
            StartCoroutine(PostRequestHistoryCoroutine(url, json));
        }
        
    }
    public void HistoryPanelView(int offset)
    {
        n_value = 0;
        if (History == true)
        {
           
            HistoryPanel.SetActive(false);
            AlertPanel.SetActive(false);
            ProfilePanel.SetActive(false);
            GiftsPanel.SetActive(false);
         
            HistoryPanel.SetActive(true);
          
            History = false;
            loading.SetActive(true);
            var url = AppLoad.BaseUrl + "GetCloseTrades";
            string json = "{\"email\":\"" + PlayerPrefs.GetString("email") + "\",\"offset\":\""+offset+"\"}";
            StartCoroutine(PostRequestHistoryCoroutine(url, json));


        }
        else
        {
            FadedAnimation.SetActive(false);
            GameObject parentobj3 = GameObject.Find("Canvas/MainUI/MyTrade/Container");
            if (parentobj3.transform.childCount > 0)
            {
                for (int i = 0; i < parentobj3.transform.childCount; i++)
                {
                    GameObject.Destroy(parentobj3.transform.GetChild(i).transform.gameObject);
                    //  Debug.Log("deleted");
                }
               

            }
         
            //GetchildOfHistory.color = white;
            HistoryDownPanel.SetTrigger("down");
            History = true;

        }

    }
    bool Gifts = true;
    public void GiftsView()
    {

        if (Gifts == true)
        {
            FadedAnimation.SetActive(true);
            GiftsPanel.SetActive(false);
            HistoryPanel.SetActive(false);
            AlertPanel.SetActive(false);
            ProfilePanel.SetActive(false);
            GiftsPanel.SetActive(true);
            purchasePanel.SetActive(false);
            chechiap = false;
            Color Yellow1 = new Color32(255, 255, 255, 255);
            AlertReplace.color = Yellow1;
            ShopRepalce.color = Yellow1;
            Color Yellow = new Color32(14, 102, 130, 255);
            
             GiftsReplace.color = Yellow;
            
            Gifts = false;
        }
        else
        {
            FadedAnimation.SetActive(false);
            Color Blue = new Color32(255, 255, 255, 255);
           GiftsReplace.color = Blue;
            //Color Yellow = new Color32(255, 199, 0, 255);
         //   Color white = new Color32(255, 199, 0, 255);
            LocakScreenContainer.SetActive(true);
          //  GetchildOfgifts.color = white;
            GiftDownPanel.SetTrigger("down");
            Gifts = true;

        }

    }
    private bool mf=false;
    public void Edit()
    {
        profilecontain[0].GetComponent<InputField>().interactable = true;
        profilecontain[1].GetComponent<InputField>().interactable = true;
        profilecontain[2].GetComponent<InputField>().interactable = true;
        profilecontain[3].GetComponent<InputField>().interactable = true;
        editbutton.SetActive(false);
        submitbuton.SetActive(true);
        mf=true;
    }
    public void ProfileView()
    {
        Color Yellow1 = new Color32(255, 255, 255, 255);
        AlertReplace.color = Yellow1;
        GiftsReplace.color = Yellow1;
        ShopRepalce.color = Yellow1;
        ProfilePanel.SetActive(false);
        ProfilePanel.SetActive(true);
        GiftsPanel.SetActive(false);
        HistoryPanel.SetActive(false);
        AlertPanel.SetActive(false);
        purchasePanel.SetActive(false);
        chechiap = false;


        if (PlayerPrefs.GetString("email") != "")
        {
            loading.SetActive(true);
            var url = AppLoad.BaseUrl + "UserDetails";
            string json = "{\"email\":\"" + PlayerPrefs.GetString("email") + "\"}";
            StartCoroutine(PostRequestCoroutine(url, json));
        }
        else
        {
            PlayerPrefs.SetInt("Login", 0);
            PlayerPrefs.SetString("email", "");
            this.gameObject.SetActive(false);
        }

    }
    IEnumerator hidenotification()
    {
        yield return new WaitForSeconds(2);
        NetworkErrorMessage.SetActive(false);
    }
    private IEnumerator PostRequestAlertCoroutine(string url, string json)
    {
        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(json);

        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();

        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";

        UnityWebRequest www =
            new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        yield return www.SendWebRequest();
        loadingalert.SetActive(false);
        if (www.isNetworkError)
        {
           
            NetworkErrorMessage.transform.GetChild(0).GetComponent<Text>().text = "Network error!";
            NetworkErrorMessage.SetActive(true);
            StartCoroutine("hidenotification");
        }
        else
        {
            
            
            if (AppLoad.TradeNumCount == 1)
            {
                final = 1;
               
            }
            else if(AppLoad.TradeNumCount == 2)
            {
                final = 2;
              
            }else
            {
                
            }
            JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());
          
            if (jsonNode["status"] == "0")
            {
               
               
                int i = 0;
                foreach (JSONNode item in jsonNode["info"])
                {
                    i++;
                    
                    if (i <= final &&  AppLoad.TradeNumCount !=0)
                    {
                       
                      
                        System.DateTime theTime;
                        string date, time;

                        if (item["actiontime"] != null)
                        {
                            theTime = System.DateTime.Parse(item["actiontime"]);
                            date = theTime.ToString("yyyy/MM/dd");
                            time = theTime.ToString("hh:mm:ss tt");
                        }
                        else
                        {
                            date = "";
                            time = "";
                        }

                      
                        GameObject obj = Instantiate(alertdata);
                       
                        parentobj1 = GameObject.Find("Canvas/MainUI/ActiveTrade/data/Container");
                        obj.transform.SetParent(parentobj1.transform);
                        obj.transform.localScale = new Vector3(1, 1, 1);
                        
                        if (item["action"] == "UP")
                        {
                            obj.transform.GetChild(1).GetComponent<Image>().sprite = upicon;
                        }
                        else
                        {
                            obj.transform.GetChild(1).GetComponent<Image>().sprite = downicon;
                        }

                        obj.transform.GetChild(0).GetComponent<Text>().text = item["startpoint"];

                        obj.transform.GetChild(2).GetComponent<Text>().text = item["invest"];

                        obj.transform.GetChild(3).GetComponent<Text>().text = date;
                        obj.transform.GetChild(4).GetComponent<Text>().text = time;
                        obj.transform.GetChild(5).GetComponent<Text>().text = item["maxminutes"];
                        if (AlertNorecord.activeSelf==true)
                        {
                            AlertNorecord.SetActive(false);
                        }

                    }
                    else
                    {
                      if(AppLoad.TradeNumCount==0)
                        {
                            if (AlertNorecord.activeSelf == false)
                            {
                                AlertNorecord.SetActive(true);
                            }
                        }
                    }



                }

            }
            else
            {
                 if(!AlertNorecord.activeSelf)
                {
                    AlertNorecord.SetActive(true);
                }
               
               
            }
            //  Debug.Log(www.downloadHandler.text);
        }

    }
    private IEnumerator PostRequestHistoryCoroutine(string url, string json)
    {
        loading.SetActive(true);
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
           

            NetworkErrorMessage.transform.GetChild(0).GetComponent<Text>().text = "Network error!";
            NetworkErrorMessage.SetActive(true);
            StartCoroutine("hidenotification");
        }
        else
        {
            HistoryItemReference.SetActive(false);
            JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());
            // Debug.Log(jsonNode["status"]+"and"+ jsonNode["message"]);
            if (jsonNode["status"] == "0")
            {
                int i = 0;
                foreach (JSONNode item in jsonNode["info"])
                {
                    i++;
                    // Debug.Log("Datetime" + item["actiontime"]);
                    if(HistoryItemReference.activeSelf)
                    {
                        HistoryItemReference.SetActive(true);
                    }
                   
                    System.DateTime theTime;
                    string date, time;

                    if (item["actiontime"] != null)
                    {
                        theTime = System.DateTime.Parse(item["actiontime"]);
                        date = theTime.ToString("yyyy/MM/dd");
                        time = theTime.ToString("hh:mm:ss tt");
                    }
                    else
                    {
                        date = "";
                        time = "";
                    }

                    //  Debug.Log("ID:"+item["id"]);
                    GameObject obj = Instantiate(Historydata);
                    // obj.SetActive(true);
                    // parentobj = GameObject.Find("Canvas/MainUI/History/ScrollView/Content");
                    parentobj = GameObject.Find("Canvas/MainUI/MyTrade/Container");
                    obj.transform.SetParent(parentobj.transform,false);
                  
                    if (item["action"] == "UP")
                    {
                        obj.transform.GetChild(2).GetComponent<Image>().sprite = upicon;
                    }
                    else
                    {
                        obj.transform.GetChild(2).GetComponent<Image>().sprite = downicon;
                    }

                    obj.transform.GetChild(0).GetComponent<Text>().text = item["startpoint"];
                    obj.transform.GetChild(1).GetComponent<Text>().text = item["endpoint"];
                    obj.transform.GetChild(3).GetComponent<Text>().text = item["invest"];
                    obj.transform.GetChild(4).GetComponent<Text>().text = item["earn"];



                    int temp = item["earn"];

                    if (temp > 0)
                    {
                        obj.transform.GetChild(4).GetComponent<Text>().color = Color.green;
                    }
                    else

                    {
                        obj.transform.GetChild(4).GetComponent<Text>().color = Color.red;
                    }
                    //Debug.Log(temp);
                    obj.transform.GetChild(5).GetComponent<Text>().text = date;
                    obj.transform.GetChild(6).GetComponent<Text>().text = time;
                    obj.transform.GetChild(7).GetComponent<Text>().text = item["maxminutes"];
                    // obj.transform.GetChild(9).GetComponent<Text>().text = item["status"];



                }

           

            }
            else
            {
              
               //n_value=n_value;
               if(n_value>1)
                {
                    btnarrowright.SetActive(false);
                }
               else if(n_value<0)
                {
                    btnarrowleft.SetActive(false);
                }
                HistoryItemReference.SetActive(true);
               
            }
            //  Debug.Log(www.downloadHandler.text);
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
        loading.SetActive(false);
        if (www.isNetworkError)
        {
           

            NetworkErrorMessage.transform.GetChild(0).GetComponent<Text>().text = "Network error!";
            NetworkErrorMessage.SetActive(true);
            StartCoroutine("hidenotification");
        }
        else
        {
            JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());
            // Debug.Log(jsonNode["status"]+"and"+ jsonNode["message"]);
            if (jsonNode["status"] == "0")
            {
                Uid.text = jsonNode["uniqueid"];
                profilecontain[0].text = jsonNode["country"];
                profilecontain[1].text = jsonNode["Name"];
                profilecontain[2].text = jsonNode["mobile"];
                profilecontain[3].text = jsonNode["dob"];
                profilecontain[4].text = jsonNode["email"];

                if (jsonNode["gender"] == "Male")
                {

                    ToogleFeMale();
                    Tooggeele = true;
                    M.SetActive(true);
                    F.SetActive(false);
                }
                else if (jsonNode["gender"] == "Female")
                {
                    ToogleMale();
                    Tooggeele = false;
                     M.SetActive(false);
                    F.SetActive(true);
                }

            }
            else
            {
               

                NetworkErrorMessage.transform.GetChild(0).GetComponent<Text>().text = "Something going wrong!";
                NetworkErrorMessage.SetActive(true);
                StartCoroutine("hidenotification");
            }
            //  Debug.Log(www.downloadHandler.text);
        }

    }
    public void ProfileViewOff()
    {
        FadedAnimation.SetActive(false);
        Color Blue = new Color32(255, 255, 255, 255);
        ProfileReplacce.color = Blue;
        ProfileDownPanel.SetTrigger("Profiledrop");


    }



    string gender;
    public void SubmitUpdate()
    {
        //  Debug.Log("Successs"+ profilecontain[1].text);
        profilecontain[0].GetComponent<InputField>().interactable = false;
        profilecontain[1].GetComponent<InputField>().interactable = false;
        profilecontain[2].GetComponent<InputField>().interactable = false;
        profilecontain[3].GetComponent<InputField>().interactable = false;
        if (Tooggeele)
        {
            
            gender = "Female";
        }
        else
        {
            gender = "Male";
        }
        if (Uid.text != null && profilecontain[1].text != null && profilecontain[2].text != null && profilecontain[3].text != null && profilecontain[0].text != null)
        {
            loading.SetActive(true);
            var url = AppLoad.BaseUrl + "SubmitUpdate";
            string json = "{\"id\":\"" + Uid.text + "\",\"email\":\"" + PlayerPrefs.GetString("email") + "\",\"name\":\"" + profilecontain[1].text + "\",\"mobile\":\"" + profilecontain[2].text + "\",\"gender\":\"" + gender + "\",\"country\":\"" + profilecontain[0].text + "\",\"dob\":\"" + profilecontain[3].text + "\"}";
            StartCoroutine(Updatepost(url, json));
        }
        else
        {
            NetworkErrorMessage.transform.GetChild(0).GetComponent<Text>().text = "Please complete your profile!";
            NetworkErrorMessage.SetActive(true);
            StartCoroutine("hidenotification");
        }
        mf=false;
    }
    private IEnumerator Updatepost(string url, string json)
    {
        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(json);

        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();

        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";

        UnityWebRequest www =
            new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        yield return www.SendWebRequest();
        loading.SetActive(false);
        editbutton.SetActive(true);
        submitbuton.SetActive(false);
        if (www.isNetworkError)
        {
           
            NetworkErrorMessage.transform.GetChild(0).GetComponent<Text>().text = "Network error!";
            NetworkErrorMessage.SetActive(true);
            StartCoroutine("hidenotification");

        }
        else
        {
            JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());
            // Debug.Log(jsonNode["status"]+"and"+ jsonNode["message"]);
            if (jsonNode["status"] == "0")
            {
               
                NetworkErrorMessage.transform.GetChild(0).GetComponent<Text>().text = "Success!";
                NetworkErrorMessage.SetActive(true);
                StartCoroutine("hidenotification");

            }
            else
            {
               
                NetworkErrorMessage.transform.GetChild(0).GetComponent<Text>().text = "Please complete your profile!";
                NetworkErrorMessage.SetActive(true);
                StartCoroutine("hidenotification");
            }

            //  Debug.Log(www.downloadHandler.text);
        }

    }





    private void ResetCOLOR()
    {
        
        // Color Blue = new Color32(11, 12, 46, 246);
        Color Blue = new Color32(14, 20, 26, 255);
        AlertReplace.color = Blue;
        //Color Blue1 = new Color32(11, 12, 46, 246);
        Color Blue1 = new Color32(14, 20, 26, 255);
        HistoryReplace.color = Blue;

        Color Blue2 = new Color32(14, 20, 26, 255);
        GiftsReplace.color = Blue;

        Color Yellow = new Color32(255, 199, 0, 255);
        GetchildOfgifts.color = Yellow;
        GetchildOfAlert.color = Yellow;
        GetchildOfHistory.color = Yellow;

    }

    public void Callingstartandendpoint()
    {
      
    }


    public void CongrateYouUnlcokApp()
    {
       /* if (LockscreenNum >= 2)
        {
            PlayerPrefs.SetInt("lockscreennum", LockscreenNum);
            CongratesScreen.SetActive(true);
            LocakPopUpFirstTimeopen.SetActive(false);
            LocakScreenContainer.SetActive(false);
            PlayerPrefs.SetInt("lockvalue", 1);
        }*/
    }



    public void fb()
    {
        Application.OpenURL("https://www.facebook.com/GameLovinStudio/");
    }
    public void twitter()
    {
        Application.OpenURL("https://twitter.com/GameLovinStudio");
    }


}
