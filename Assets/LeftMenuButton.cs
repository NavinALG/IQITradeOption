using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using VoxelBusters;
using VoxelBusters.NativePlugins;
public class LeftMenuButton : MonoBehaviour
{


    public GameObject HowToTrade, Withdraw, MyTrade, Settings, LoadingScreen, Support ,Paytmredeemreward, Paypalredeemreward,walletPanel,
        Logout, ReferAndEarn, WithdrawHistory, PendingContiner, RecevingContiner, EarningPanel,redeemdata;
    public GameObject Fadedhide,PendingColorButton,RecvColorButton, loading, msg, datafield, Referralearndata,PremiumPanel, PurchasePanel;
    GameObject parentobj, parentobj1;
    public Sprite upicon, downicon;
    public Text Nametxt, UserIdtxt,ReferralCodetxt;
    private string name1, userid;
    public InputField emailsuport,title,smsg;
   //public Admob ads;
    public Animator AnimLeftMenu;
  
    void Start()
    {
        

       // AllPanelClose();
        if (PlayerPrefs.GetString("email") != "")
        {
            ReferralCodetxt.text = PlayerPrefs.GetString("uniqueid");
            emailsuport.text = PlayerPrefs.GetString("email");
             var url =AppLoad.BaseUrl+"UserDetails";
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
  
    private IEnumerator PostRequestCoroutine(string url, string json)
    {
        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(json);

        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();

        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";

        UnityWebRequest www =new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        yield return www.SendWebRequest();
      //  loading.SetActive(false);
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
                PlayerPrefs.SetString("name", jsonNode["Name"]);
                ReferralCodetxt.text = jsonNode["uniqueid"];

               PlayerPrefs.SetString("uniqueid",jsonNode["uniqueid"]);

            }
            else
            {
              
                msg.transform.GetChild(0).GetComponent<Text>().text = "Something going wrong!";
                msg.SetActive(true);
                StartCoroutine("hidenotification");

            }
            //  Debug.Log(www.downloadHandler.text);
        }

    }
    private void Update()
    {
        Nametxt.text = PlayerPrefs.GetString("name");
        UserIdtxt.text = PlayerPrefs.GetString("email");
    }
    public void OpenWalletpanel()
    {
       // ads.hidebanner();
        AllPanelClose();
        walletPanel.SetActive(true);
        loading.SetActive(true);
        var url =AppLoad.BaseUrl+"walletDetails";
        string json = "{\"email\":\"" + PlayerPrefs.GetString("email") + "\"}";
        StartCoroutine(GetwalletCoroutine(url, json));

    }
    private IEnumerator GetwalletCoroutine(string url, string json)
    {
        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(json);

        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();

        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";

        UnityWebRequest www = new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        yield return www.SendWebRequest();
          loading.SetActive(false);
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
               
                 walletPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = jsonNode["tradecredits"];
                 walletPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = jsonNode["tradedebits"];
                 walletPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = (float.Parse(jsonNode["tradecredits"])- float.Parse(jsonNode["tradedebits"])).ToString();

                 walletPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = jsonNode["videocredits"];
               //  walletPanel.transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = jsonNode["tradedebits"];
                 walletPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = jsonNode["videocredits"];

                 walletPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = jsonNode["referreebonus"];
                 walletPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = jsonNode["referralbonus"];
                 walletPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = (float.Parse(jsonNode["referreebonus"]) + float.Parse(jsonNode["referralbonus"])).ToString();

                 walletPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = jsonNode["signupbonus"];
                 walletPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = jsonNode["iap"];
                 walletPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text =(float.Parse( jsonNode["signupbonus"])+float.Parse( jsonNode["iap"])).ToString();

                 walletPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(4).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = ((float.Parse(jsonNode["referreebonus"]) + float.Parse(jsonNode["referralbonus"])+ float.Parse(jsonNode["videocredits"]) + float.Parse(jsonNode["iap"]) + float.Parse(jsonNode["signupbonus"])) + (float.Parse(jsonNode["tradecredits"]) - float.Parse(jsonNode["tradedebits"]))).ToString();
                  walletPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(4).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = jsonNode["withdraw"];
                 walletPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(4).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = jsonNode["total"];
                 
                

            }
            else
            {

                msg.transform.GetChild(0).GetComponent<Text>().text = "Something going wrong!";
                msg.SetActive(true);
                StartCoroutine("hidenotification");

            }
            //  Debug.Log(www.downloadHandler.text);
        }

    }


    public void sendmsge()
    {
        if(title.text!=""&&smsg.text!="")
        {
            var url = AppLoad.BaseUrl+"addmsg";
            string json = "{\"unique_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"email\":\"" + PlayerPrefs.GetString("email") + "\",\"title\":\"" + title.text + "\",\"message\":\"" + smsg.text + "\"}";
            StartCoroutine(PostmsgCoroutine(url, json));
          
            title.text = "";
            smsg.text = "";
        }
        else
        {
            msg.transform.GetChild(0).GetComponent<Text>().text = "Please describe the issue!";
            msg.SetActive(true);
            StartCoroutine("hidenotification");
        }
       
    }
    private IEnumerator PostmsgCoroutine(string url, string json)
    {
        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(json);

        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();

        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";

        UnityWebRequest www = new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        yield return www.SendWebRequest();
        //  loading.SetActive(false);
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
                 msg.transform.GetChild(0).GetComponent<Text>().text = "Your message has been  successfully sent.";
            msg.SetActive(true);
            StartCoroutine("hidenotification");

            }
            else
            {
                
                msg.transform.GetChild(0).GetComponent<Text>().text = "Something going wrong!";
                msg.SetActive(true);
                StartCoroutine("hidenotification");

            }
            //  Debug.Log(www.downloadHandler.text);
        }

    }
    public void sharesheet()
    {
        ShareSheet _sharesheet = new ShareSheet();
        _sharesheet.Text = "Hi, I am earning free Paypal cash/Paytm Cash upto 1000$ daily. You can also earn, Download this amazing app & Use my referral ID " + ReferralCodetxt.text + " and Get Free Bonus Coins, Download app now ";
       _sharesheet.URL = "http://tradeoption.gamelovin.com/ref/?" + ReferralCodetxt.text;
        NPBinding.Sharing.ShowView(_sharesheet, finishshare);

       

    }
    private void finishshare(eShareResult _result)
    {
        Debug.Log(_result);
    }

    public void HowToTradeOpen()
    {
       // ads.hidebanner();
        AllPanelClose();
        
        HowToTrade.SetActive(true);
    }
    public void Rateus()
    {
        AllPanelClose();
        PlayerPrefs.SetInt("Rateus", 1);
        Application.OpenURL("market://details?id=com.algmedia.tradeoption");
      
    }
    public void WithdrawOpen()
    {
        // ads.hidebanner();
       // this.transform.GetComponent<Animator>().SetTrigger("leftmenuclose");
       
        AllPanelClose();
       // AnimLeftMenu.SetTrigger("leftmenuclose");
        Withdraw.SetActive(true);
        loading.SetActive(true);
        var url =AppLoad.BaseUrl+"getredeemrewards";
       // string json = "{\"id\":\"" + PlayerPrefs.GetString("uniqueid") + "\"}";
        string json = "{ }";
        StartCoroutine(PostRequestrewardsCoroutine(url, json));
    }

    private IEnumerator PostRequestrewardsCoroutine(string url, string json)
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

                int i = 0;
                foreach (JSONNode item in jsonNode["info"])
                {

                   
                    //  Debug.Log("ID:"+item["id"]);
                    if(item["title"]=="Paytm")
                    {
                        GameObject obj = Instantiate(Paytmredeemreward);
                        // obj.SetActive(true);
                        parentobj1 = GameObject.Find("Canvas/MainUI/WithDraw/Container");
                        obj.transform.SetParent(parentobj1.transform);
                        obj.transform.localScale = new Vector3(1,1,1);
                        obj.transform.GetChild(2).GetComponent<Text>().text = item["id"];
                       obj.transform.GetChild(0).transform.GetComponent<Text>().text = "₹" + item["amount"];
                        obj.transform.GetChild(1).transform.GetComponent<Text>().text = item["points"];
                    }
                    else
                    {
                        GameObject obj = Instantiate(Paypalredeemreward);
                        // obj.SetActive(true);
                        parentobj1 = GameObject.Find("Canvas/MainUI/WithDraw/Container");
                        obj.transform.SetParent(parentobj1.transform);
                        obj.transform.localScale = new Vector3(1,1,1);
                        obj.transform.GetChild(2).GetComponent<Text>().text = item["id"];
                      
                        obj.transform.GetChild(0).GetComponent<Text>().text = "$" + item["amount"];
                        obj.transform.GetChild(1).GetComponent<Text>().text = item["points"];
                    }
                   
                  




                }

            }
            else
            {
              
               // msg.transform.GetChild(0).GetComponent<Text>().text = "No record found";
               // msg.SetActive(true);
              //  StartCoroutine("hidenotification");

            }
            //  Debug.Log(www.downloadHandler.text);
        }

    }

    public void MyTradeOpen()
    {
      
        AllPanelClose();
      
        MyTrade.SetActive(true);
        getTrades();
     //   ads.hidebanner();
    }
    public void SettingsOpen()
    {

        AllPanelClose();
     
        Settings.SetActive(true);
       // ads.hidebanner();
    }
    public void PremiumOpen(string name)
    {
       
        

        if (PurchasePanel.activeSelf==true)
        {
            PurchasePanel.SetActive(false);
        }
       
        if (this.gameObject.activeSelf==false)
        {
            this.gameObject.SetActive(true);
        }
        if(name=="Subscribe")
        {
            PremiumPanel.transform.GetChild(1).gameObject.SetActive(false);
            PremiumPanel.transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            PremiumPanel.transform.GetChild(1).gameObject.SetActive(true);
            PremiumPanel.transform.GetChild(2).gameObject.SetActive(false);
        }
        AllPanelClose();
        Ui.chechiap = true;

        PremiumPanel.SetActive(true);
        
    }
    public void LoadingScreenOpen()
    {

        AllPanelClose();
       
        LoadingScreen.SetActive(true);
       // ads.hidebanner();
    }
    public void SupportOpen()
    {

        AllPanelClose();
        Support.SetActive(true);
      //  ads.hidebanner();
    }
    public void LogoutOpen()
    {

        AllPanelClose();
      
        Logout.SetActive(true);
     //   ads.hidebanner();
    }



    public void ReferAndFriend()
    {
       
        AllPanelClose();
        ReferAndEarn.SetActive(true);
        // ads.hidebanner();
        if (this.gameObject.activeSelf == false)
        {
            this.gameObject.SetActive(true);
        }

    }
    public void WithdrawHistoryFunction()
    {
        loading.SetActive(true);
        this.transform.GetComponent<Animator>().SetTrigger("leftmenuclose");
        var url =AppLoad.BaseUrl+"getwidrawalpending";
        string json = "{\"id\":\"" + PlayerPrefs.GetString("uniqueid") + "\"}";
        StartCoroutine(PostRequestredeemdataCoroutine(url, json, "Pending"));
        // AllPanelClose();
        WithdrawHistory.SetActive(false);
        WithdrawHistory.SetActive(true);
      //  ads.hidebanner();
    }
    IEnumerator hidenotification()
    {
        yield return new WaitForSeconds(2);
        msg.SetActive(false);
    }
    public void ReferEarnngPanel()
    {
        AllPanelClose();
        EarningPanel.SetActive(true);
        // loading.SetActive(true);
        //var url = "http://gamelovinstudio.com/Trading/Userprocess/getReferralsearning";
        // string json = "{\"id\":\"" + PlayerPrefs.GetString("uniqueid") + "\"}";
        // StartCoroutine(PostRequestReferralsCoroutine(url, json));
        // ads.hidebanner();
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
            
                msg.transform.GetChild(0).GetComponent<Text>().text = "No record found";
                msg.SetActive(true);
                StartCoroutine("hidenotification");

            }
            //  Debug.Log(www.downloadHandler.text);
        }

    }

    //INTERNAL FUNCTION OF LEFT PANEL
    //WithdrawalHistory
    public void Pending()
    {
        parentobj1 = GameObject.Find("Canvas/MainUI/Withdraw  History/PendingData/BG");
        if (parentobj1.transform.childCount > 0)
        {
            for (int i = 0; i < parentobj1.transform.childCount; i++)
            {
                Destroy(parentobj1.transform.GetChild(i).transform.gameObject);
                //  Debug.Log("deleted");
            }
           

        }
        parentobj1 = GameObject.Find("Canvas/MainUI/Withdraw  History/RcvData/BG");
        if (parentobj1.transform.childCount > 0)
        {
            for (int i = 0; i < parentobj1.transform.childCount; i++)
            {
               Destroy(parentobj1.transform.GetChild(i).transform.gameObject);
                //  Debug.Log("deleted");
            }
          

        }
        PendingContiner.SetActive(true);
        RecevingContiner.SetActive(false);
        //Color cyannn = new Color32(250, 201, 8, 255);
       // Color white = new Color32(29, 41, 54, 255);
       // PendingColorButton.GetComponent<Image>().color = cyannn;
       // RecvColorButton.GetComponent<Image>().color = white;
        loading.SetActive(true);
      //  ads.hidebanner();
        var url = AppLoad.BaseUrl+"getwidrawalpending";
        string json = "{\"id\":\"" + PlayerPrefs.GetString("uniqueid") + "\"}";
        StartCoroutine(PostRequestredeemdataCoroutine(url, json,"Pending"));
       

    }
    private IEnumerator PostRequestredeemdataCoroutine(string url, string json,string name)
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
                if (GameObject.Find("Canvas/MainUI/Withdraw  History/Nostory").activeSelf)
                {
                    GameObject.Find("Canvas/MainUI/Withdraw  History/Nostory").SetActive(false);
                }
                int i = 0;
                foreach (JSONNode item in jsonNode["info"])
                {
                    //  Debug.Log("ID:"+item["id"]);
                    GameObject obj = Instantiate(redeemdata);
                    // obj.SetActive(true);
                    if(name=="Pending")
                    {
                        parentobj1 = GameObject.Find("Canvas/MainUI/Withdraw  History/PendingData/BG");
                    }
                    else
                    {
                        parentobj1 = GameObject.Find("Canvas/MainUI/Withdraw  History/RcvData/BG");
                    }
                   
                    obj.transform.SetParent(parentobj1.transform);
                    obj.transform.localScale = new Vector3(1, 1, 1);
                    obj.transform.GetChild(0).GetComponent<Text>().text = item["transaction_type"];
                    obj.transform.GetChild(1).GetComponent<Text>().text = item["email"] +""+item["mobile"];
                    obj.transform.GetChild(2).GetComponent<Text>().text = item["points_amount"];
                    obj.transform.GetChild(3).GetComponent<Text>().text = item["amount"];
                   
                }

            }
            else
            {
                if (!GameObject.Find("Canvas/MainUI/Withdraw  History/Nostory").activeSelf)
                {
                    GameObject.Find("Canvas/MainUI/Withdraw  History/Nostory").SetActive(true);
                }
               
                

            }
            //  Debug.Log(www.downloadHandler.text);
        }

    }

    public void receving()
    {
        parentobj1 = GameObject.Find("Canvas/MainUI/Withdraw  History/PendingData/BG");
        if (parentobj1.transform.childCount > 0)
        {
            for (int i = 0; i < parentobj1.transform.childCount; i++)
            {
                Destroy(parentobj1.transform.GetChild(i).transform.gameObject);
                //  Debug.Log("deleted");
            }
          

        }
        parentobj1 = GameObject.Find("Canvas/MainUI/Withdraw  History/RcvData/BG");
        if (parentobj1.transform.childCount > 0)
        {
            for (int i = 0; i < parentobj1.transform.childCount; i++)
            {
                Destroy(parentobj1.transform.GetChild(i).transform.gameObject);
                //  Debug.Log("deleted");
            }
          

        }
        PendingContiner.SetActive(false);
        RecevingContiner.SetActive(true);
       
        loading.SetActive(true);
       // ads.hidebanner();
        var url = AppLoad.BaseUrl+"getwidrawalApproved";
        string json = "{\"id\":\"" + PlayerPrefs.GetString("uniqueid") + "\"}";
        StartCoroutine(PostRequestredeemdataCoroutine(url, json, "Approved"));
    }
   
    void AllPanelClose()
    {
        HowToTrade.SetActive(false);
        Withdraw.SetActive(false);
        MyTrade.SetActive(false);
        Settings.SetActive(false);
        LoadingScreen.SetActive(false);
        Support.SetActive(false);
        Logout.SetActive(false);
        ReferAndEarn.SetActive(false);
        WithdrawHistory.SetActive(false);
        EarningPanel.SetActive(false);
        walletPanel.SetActive(false);
        PremiumPanel.SetActive(false);
        PurchasePanel.SetActive(false);
       
    }
    public void getTrades()
    {
        loading.SetActive(true);

        var url = AppLoad.BaseUrl+"GetMyTrades";
        string json = "{\"email\":\"" + PlayerPrefs.GetString("email") + "\"}";
        StartCoroutine(filltrads(url, json));

    }
    
    private IEnumerator filltrads(string url, string json)
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
                int i = 0;
                foreach (JSONNode item in jsonNode["info"])
                {
                    i++;
                    Debug.Log("Datetime" + item["actiontime"]);
                    System.DateTime theTime;
                    string date, time;

                    if (item["actiontime"] != null)
                    {
                        theTime = System.DateTime.Parse(item["actiontime"]);
                        date = theTime.ToString("yyyy-MM-dd");
                        time = theTime.ToString("hh:mm:ss tt");
                    }
                    else
                    {
                        date = "";
                        time = "";
                    }

                    //  Debug.Log("ID:"+item["id"]);
                    GameObject obj = Instantiate(datafield);
                    // obj.SetActive(true);
                    parentobj = GameObject.Find("Canvas/MainUI/MyTrade/Container");
                    obj.transform.SetParent(parentobj.transform);
                    obj.transform.localScale = new Vector3(1, 1, 1);
                    obj.transform.GetChild(0).GetComponent<Text>().text = i.ToString();
                    // obj.transform.GetChild(1)
                    if (item["action"] == "UP")
                    {
                          obj.transform.GetChild(1).GetComponent<Image>().sprite = upicon;
                    }
                    else
                    {
                       obj.transform.GetChild(1).GetComponent<Image>().sprite = downicon;
                    }

                    obj.transform.GetChild(2).GetComponent<Text>().text = item["startpoint"];
                    obj.transform.GetChild(3).GetComponent<Text>().text = item["endpoint"];
                    obj.transform.GetChild(4).GetComponent<Text>().text = item["invest"];
                    obj.transform.GetChild(5).GetComponent<Text>().text = item["earn"];
                    obj.transform.GetChild(6).GetComponent<Text>().text = date;
                    obj.transform.GetChild(7).GetComponent<Text>().text = time;
                    obj.transform.GetChild(8).GetComponent<Text>().text = item["maxminutes"];
                    obj.transform.GetChild(9).GetComponent<Text>().text = item["status"];



                }

            }
            else
            {
              
             //   msg.transform.GetChild(0).GetComponent<Text>().text = "No record!";
             //   msg.SetActive(true);
              //  StartCoroutine("hidenotification");
            }
            //  Debug.Log(www.downloadHandler.text);
        }

    }
    public void AllPanelCloseWithAnim()
    {
        if(this.gameObject.activeSelf==false)
        {
            this.gameObject.SetActive(true);
        }
        parentobj = GameObject.Find("Canvas/MainUI/MyTrade/Container");
        if (parentobj.transform.childCount > 0)
        {
            for (int i = 0; i < parentobj.transform.childCount; i++)
            {
                GameObject.Destroy(parentobj.transform.GetChild(i).transform.gameObject);
                //  Debug.Log("deleted");
            }
          

        }
         parentobj1 = GameObject.Find("Canvas/MainUI/ReferalEarning/Container");
        if (parentobj1.transform.childCount > 0)
        {
            for (int i = 0; i < parentobj1.transform.childCount; i++)
            {
                GameObject.Destroy(parentobj1.transform.GetChild(i).transform.gameObject);
                //  Debug.Log("deleted");
            }
           

        }
        parentobj1 = GameObject.Find("Canvas/MainUI/Withdraw  History/PendingData/BG");
        if (parentobj1.transform.childCount > 0)
        {
            for (int i = 0; i < parentobj1.transform.childCount; i++)
            {
                GameObject.Destroy(parentobj1.transform.GetChild(i).transform.gameObject);
                //  Debug.Log("deleted");
            }
           

        }
        parentobj1 = GameObject.Find("Canvas/MainUI/Withdraw  History/RcvData/BG");
        if (parentobj1.transform.childCount > 0)
        {
            for (int i = 0; i < parentobj1.transform.childCount; i++)
            {
                GameObject.Destroy(parentobj1.transform.GetChild(i).transform.gameObject);
                //  Debug.Log("deleted");
            }
           

        }
        parentobj1 = GameObject.Find("Canvas/MainUI/WithDraw/Container");
        if (parentobj1.transform.childCount > 0)
        {
            for (int i = 0; i < parentobj1.transform.childCount; i++)
            {
                GameObject.Destroy(parentobj1.transform.GetChild(i).transform.gameObject);
                //  Debug.Log("deleted");
            }
          

        }
        StartCoroutine("finalclose");
      
    }

    IEnumerator finalclose()
    {
        Fadedhide.SetActive(false);
        HowToTrade.GetComponent<Animator>().SetTrigger("close");
        Withdraw.GetComponent<Animator>().SetTrigger("close");
        MyTrade.GetComponent<Animator>().SetTrigger("close");
        Settings.GetComponent<Animator>().SetTrigger("close");
        LoadingScreen.GetComponent<Animator>().SetTrigger("close");
        Support.GetComponent<Animator>().SetTrigger("close");
        Logout.GetComponent<Animator>().SetTrigger("close");
        WithdrawHistory.GetComponent<Animator>().SetTrigger("close");
        ReferAndEarn.GetComponent<Animator>().SetTrigger("close");
        EarningPanel.GetComponent<Animator>().SetTrigger("close");
        walletPanel.GetComponent<Animator>().SetTrigger("close");
        PremiumPanel.GetComponent<Animator>().SetTrigger("close");
        yield return new WaitForSeconds(0.5f);
        AllPanelClose();
    }
}
