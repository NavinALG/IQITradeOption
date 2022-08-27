using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class mainui : MonoBehaviour
{
    public GameObject login, circle, msg, referralpanel, LocakPopUpFirstTimeopen,loadrefresh,UserbanPanel,Sale;
    // Start is called before the first frame update
    public Image logoicon,leftmenuprofileicon, profileicon;
    public Sprite[] icon;
    public Image[] Profile_Pic;
    public static int sale=1;
    public GameObject[] saleon;
    public static int tcount;
    public GameObject Wheel;
    private IEnumerator PostRequestuserCoroutine(string url, string json)
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
                PlayerPrefs.SetInt("crown", int.Parse(jsonNode["crown"]));
                if (jsonNode["is_lock"] == "Y")
                {
                    PlayerPrefs.SetInt("lockvalue", 0);
                    LocakPopUpFirstTimeopen.SetActive(false);
                }
                else
                {
                    PlayerPrefs.SetInt("lockvalue", 1);
                    LocakPopUpFirstTimeopen.SetActive(false);
                    
                }
                tcount = int.Parse(jsonNode["tradecount"]);
              

            }

            else
            {
              

            }
            //  Debug.Log(www.downloadHandler.text);
        }

    }
    private IEnumerator PostRefreshCoroutine(string url, string json)
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
            loadrefresh.SetActive(false);
        }
        else
        {

            JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());
            // Debug.Log(jsonNode["status"]+"and"+ jsonNode["message"]);
            if (jsonNode["status"] == "0")
            {
              
                 PlayerPrefs.SetFloat("Total", jsonNode["total"]);
                loadrefresh.SetActive(false);

            }

            else
            {
                loadrefresh.SetActive(false);

            }
            //  Debug.Log(www.downloadHandler.text);
        }

    }   
    public void refresh()
    {
        loadrefresh.SetActive(true);
        var url1 = AppLoad.BaseUrl + "UserDetails";
        string json1 = "{\"email\":\"" + PlayerPrefs.GetString("email") + "\"}";
        StartCoroutine(PostRefreshCoroutine(url1, json1));
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
                PlayerPrefs.SetInt("tcount",int.Parse(jsonNode["count"]));
               // Debug.Log("TradeCount:"+PlayerPrefs.GetInt("tcount"));
              

            }

            else
            {


            }
            
        }

    }
    public void checkcrown()
    {
        if (PlayerPrefs.GetInt("crown") == 1)
        {
            Ui.tradeno = 3;
            logoicon.sprite = icon[0];
            leftmenuprofileicon.sprite = icon[2];
           
            profileicon.sprite = icon[4];
           
        }
        else
        {
            logoicon.sprite = icon[1];
            leftmenuprofileicon.sprite = icon[3];
           
            profileicon.sprite = icon[5];
            Ui.tradeno = 1;
        }
       
        if (PlayerPrefs.GetInt("Referralsvalue") == 0)
        {
            referralpanel.SetActive(true);
        }
        else
        {

            referralpanel.SetActive(false);

        }
    }
   public IEnumerator GetTexture()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(PlayerPrefs.GetString("profileicon"));
        yield return www.SendWebRequest();
       
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            for (int i = 0; i < Profile_Pic.Length; i++)
            {
                Profile_Pic[i].sprite = Sprite.Create(myTexture, new Rect(0, 0,myTexture.width,myTexture.height), new Vector2(0.5f, 0.5f));
            }
        }
    }
    void Start()
    {
        if (PlayerPrefs.GetInt("crown") == 1)
        {
           Ui.tradeno = 3;
            logoicon.sprite = icon[0];
            leftmenuprofileicon.sprite = icon[2];
           
            profileicon.sprite = icon[4];
           
        }
        else
        {
            logoicon.sprite = icon[1];
            leftmenuprofileicon.sprite = icon[3];
          
            profileicon.sprite = icon[5];
            Ui.tradeno = 1;
        }

        var url = AppLoad.BaseUrl + "walletDetails";
        string json = "{\"email\":\"" + PlayerPrefs.GetString("email") + "\"}";
        StartCoroutine(PostRequestCoroutine(url, json));

       var url1 = AppLoad.BaseUrl + "UserDetails";
        string json1 = "{\"email\":\"" + PlayerPrefs.GetString("email") + "\"}";
        StartCoroutine(PostRequestuserCoroutine(url1, json1));
      
        var url2 = AppLoad.BaseUrl + "tradecountlimit";
        string json2 = "{\"unique_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"date\":\""+ System.DateTime.Now.ToString("MM/dd/yyyy") + "\"}";
        StartCoroutine(PostRequestcounttradeCoroutine(url2, json2));

        if (PlayerPrefs.GetString("profileicon") != "")
        {
            StartCoroutine(GetTexture());
        }
        var url3 = AppLoad.BaseUrl + "GetInappSale";
        string json3 = "{\"name\":\"Sale\"}";
        StartCoroutine(PostRequestSaleCoroutine(url3, json3));

    }
    public void PayTm(int amount)
    {
        Application.OpenURL("http://gamelovin.com/Trading/paytm/PaytmKit/TxnTest.php?uniq=" + PlayerPrefs.GetString("uniqueid") + "&amount=" + amount);

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
                DateTime lastdate =DateTime.Parse(jsonNode["end"]);
                DateTime todaydate =DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                int comp = DateTime.Compare(lastdate, todaydate);
                Debug.Log("CompareDate:"+comp+"And The Date:"+ lastdate+"current:"+todaydate);
                if(comp>=0)
                {
                    Sale.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Sale Period:" + jsonNode["start"] + " To " + jsonNode["end"];
                    Sale.SetActive(true);
                    sale = 2;
                    for(int i=0;i<saleon.Length;i++)
                    {
                        saleon[i].SetActive(true);
                    }
                }
                else
                {
                    for (int i = 0; i < saleon.Length; i++)
                    {
                        saleon[i].SetActive(false);
                    }
                }
               


            }

            else
            {


            }
            //  Debug.Log(www.downloadHandler.text);
        }

    }
    public void OpenSale()
    {
        Sale.SetActive(true);
    }
    public void OpenWheel()
    {
        Wheel.SetActive(true);
    }
    public void logout()
    {
        PlayerPrefs.DeleteAll();
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
        // circle.SetActive(false);
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
                //this.gameObject.SetActive(true);
                login.SetActive(false);
                PlayerPrefs.SetFloat("Total", jsonNode["total"]);
                PlayerPrefs.SetInt("spin", jsonNode["spincount"]);
                PlayerPrefs.SetInt("totalspin", jsonNode["totalspin"]);
               

            }
            else
            {
                PlayerPrefs.DeleteAll();
                this.gameObject.SetActive(false);
                UserbanPanel.SetActive(true);
                login.SetActive(true);
              
            }
           
        }

    }
    IEnumerator hidenotification()
    {
        yield return new WaitForSeconds(2);
        msg.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
      
    }
}
