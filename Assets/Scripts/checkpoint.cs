using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using ChartAndGraph;

class checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    private int id;
    private float send;
    float time;
    bool check;
   // public GameObject msg;
   private float amount=0;
    public float OurTimeLenght,FeedTimer;
    public GameObject TradeLostAndEarn;
    public float newValueeeeeeeeeeee;

    //use this when result show then feed this value when reult popu display
    public float Timewhentradeapply;

    private string Action;
    private GameObject parentobj;
    private GameObject UiReference;
    public static int rateno=2;
    private float per=80,tamount;
   
    private IEnumerator PostgetsettingCoroutine(string url, string json)
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

            Debug.Log("Network Fail");
        }
        else
        {
            JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());
            // Debug.Log(jsonNode["status"]+"and"+ jsonNode["message"]);
            if (jsonNode["trade"] != "")
            {
                if (PlayerPrefs.GetInt("crown") == 1)
                {
                    per = float.Parse(jsonNode["crown_per"]);
                }
                else
                {
                    per = float.Parse(jsonNode["trade"]);
                }

            }
            else
            {
                per = 60;
            }
            //  Debug.Log(www.downloadHandler.text);
        }




    }
    Ui ui;
    void Start()
    {

        ui = GameObject.Find("HANDLER").GetComponent<Ui>();
        
        string url5 = AppLoad.BaseUrl + "getsettings";
        string json = "{\"response\":\"Get\"}";
        StartCoroutine(PostgetsettingCoroutine(url5, json));
        id = AppLoad.id;
        Action = AppLoad.clickvalue;
        tamount = AppLoad.value;
         UiReference = GameObject.Find("HANDLER");
        // Debug.Log(GameObject.Find("Canvas/MainUI/TradeLostEarn").gameObject.name);
        check = true;
      
       if(AppLoad.time==30)
        {
            time = AppLoad.time;
        }
       else
        {
            time = AppLoad.time * 60;
        }
       if(AppLoad.at==0)
        {
            send = AppLoad.send2;
        }
       else
        {
            send = AppLoad.send1;
        }
        
     
        StartCoroutine(tradupd());
    }
    private void OnEnable()
    {
        
    }
    IEnumerator hidenotify()
    {
        yield return new WaitForSeconds(2);
      
    }
    private  IEnumerator tradupd()
    {
      
        yield return new WaitForSeconds(time);
        newValueeeeeeeeeeee = GraphChart.Yvalue;
        var url = AppLoad.BaseUrl + "UpdatemyTrades";
        float compare = newValueeeeeeeeeeee.CompareTo(send);
        if (Action == "UP")
            {
           
            // if (newValueeeeeeeeeeee > send)
            if (compare>0)
                {
               
                amount = (tamount * per) / 100;
               
                PlayerPrefs.SetFloat("Total", PlayerPrefs.GetFloat("Total") + amount+ tamount);
                //amount = AppLoad.value + AppLoad.value;
                Debug.Log("Upearn>:" + amount);
                    string json = "{\"id\":\"" + id + "\",\"endpoint\":\"" + newValueeeeeeeeeeee + "\",\"earn\":\"" + (amount+ tamount) + "\"}";
                    StartCoroutine(PostRequestCoroutine(url, json));

                //GameObject tost = GameObject.Find("Canvas/MainUI/TradeLostEarn");
                GameObject tost = Instantiate(TradeLostAndEarn);
                parentobj = GameObject.Find("Canvas/MainUI");
                tost.transform.SetParent(parentobj.transform);
                tost.transform.localScale = new Vector3(1, 1, 1);
                tost.transform.localPosition = new Vector3(0, 0, 0);
                RectTransform rt = tost.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(0, 0);
                tost.SetActive(true);
                tost.transform.gameObject.SetActive(true);
                tost.transform.GetChild(1).GetChild(6).GetComponent<Text>().text =  (amount + tamount).ToString();
                Color green = new Color32(25, 163, 3, 255);
                tost.transform.GetChild(1).GetChild(6).GetComponent<Text>().color = green;
                if (Timewhentradeapply > 2)
                {
                    tost.transform.GetChild(1).GetChild(7).GetComponent<Text>().text = "30 sec";
                }
                else if (Timewhentradeapply == 1)
                {
                    tost.transform.GetChild(1).GetChild(7).GetComponent<Text>().text = "1 min";
                }
                else if (Timewhentradeapply == 2)
                {
                    tost.transform.GetChild(1).GetChild(7).GetComponent<Text>().text = "2 min";
                }
                tost.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Congratulation, Your forecast is correct your profit is";
                tost.transform.GetComponent<AudioSource>().Play();
                Destroy(tost.gameObject, 10);
                AppLoad.TradeNumCount -= 1;
                // StartCoroutine("hidenotify");
               if(mainui.tcount==0)
                {
                    ui.OpenStarterkit();
                    mainui.tcount++;
                }
               


            }
              //  else if (newValueeeeeeeeeeee == send)
               else if (compare == 0)
                {
                
                amount = tamount;
                    Debug.Log("Upearn=:" + amount);
                    string json = "{\"id\":\"" + id + "\",\"endpoint\":\"" + GraphChart.Yvalue + "\",\"earn\":\"" + amount + "\"}";
                    StartCoroutine(PostRequestCoroutine(url, json));


                PlayerPrefs.SetFloat("Total", PlayerPrefs.GetFloat("Total") + amount);
                GameObject tost = Instantiate(TradeLostAndEarn);
                parentobj = GameObject.Find("Canvas/MainUI");
                tost.transform.SetParent(parentobj.transform);
                tost.transform.localScale = new Vector3(1, 1, 1);
                tost.transform.localPosition = new Vector3(0, 0, 0);
                RectTransform rt = tost.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(0, 0);
                tost.SetActive(true);
                tost.transform.gameObject.SetActive(true);
                tost.transform.GetChild(1).GetChild(6).GetComponent<Text>().text = amount.ToString();
                Color green = new Color32(25, 163, 3, 255);
                tost.transform.GetChild(1).GetChild(6).GetComponent<Text>().color = green;
                if (Timewhentradeapply > 2)
                {
                    tost.transform.GetChild(1).GetChild(7).GetComponent<Text>().text = "30 sec";
                }
                else if (Timewhentradeapply == 1)
                {
                    tost.transform.GetChild(1).GetChild(7).GetComponent<Text>().text = "1 min";
                }
                else if (Timewhentradeapply == 2)
                {
                    tost.transform.GetChild(1).GetChild(7).GetComponent<Text>().text = "2 min";
                }
                tost.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Congratulation, Your forecast is correct your profit is";
                tost.transform.GetComponent<AudioSource>().Play();
                Destroy(tost.gameObject, 10);
                AppLoad.TradeNumCount -= 1;
                // StartCoroutine("hidenotify");
                if (mainui.tcount == 0)
                {
                    ui.OpenStarterkit();
                    mainui.tcount++;
                }


            }
                else
                {
                    amount = 0;
                    //Debug.Log("Upearn:" + amount);
                    string json = "{\"id\":\"" + id + "\",\"endpoint\":\"" + GraphChart.Yvalue + "\",\"earn\":\"" + amount + "\"}";
                    StartCoroutine(PostRequestCoroutine(url, json));

                GameObject tost = Instantiate(TradeLostAndEarn);
                parentobj = GameObject.Find("Canvas/MainUI");
                tost.transform.SetParent(parentobj.transform);
                tost.transform.localScale = new Vector3(1, 1, 1);
                tost.transform.localPosition = new Vector3(0, 0, 0);
                RectTransform rt = tost.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(0, 0);
                tost.SetActive(true);
                tost.transform.gameObject.SetActive(true);
                tost.transform.GetChild(1).GetChild(6).GetComponent<Text>().text = amount.ToString() ;
                Color red = new Color32(204, 4, 4, 255);
                tost.transform.GetChild(1).GetChild(6).GetComponent<Text>().color = red;
                if (Timewhentradeapply > 2)
                {
                    tost.transform.GetChild(1).GetChild(7).GetComponent<Text>().text = "30 sec";
                }
                else if (Timewhentradeapply == 1)
                {
                    tost.transform.GetChild(1).GetChild(7).GetComponent<Text>().text = "1 min";
                }
                else if (Timewhentradeapply == 2)
                {
                    tost.transform.GetChild(1).GetChild(7).GetComponent<Text>().text = "2 min";
                }
                tost.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Sorry !   Your   Forecast  is  incorrect   Your   profit  is";
                Destroy(tost.gameObject, 10);
                AppLoad.TradeNumCount -= 1;
               
              

            }

        }
            else if (Action == "Down")
            {
               // if (newValueeeeeeeeeeee < send)
                if (compare < 0)
                {
                
                amount = 0;
                amount = (tamount * per) / 100;
                PlayerPrefs.SetFloat("Total", PlayerPrefs.GetFloat("Total") + amount + tamount);
                //amount = AppLoad.value + AppLoad.value;
                Debug.Log("Upearn>:" + amount);
                string json = "{\"id\":\"" + id + "\",\"endpoint\":\"" + GraphChart.Yvalue + "\",\"earn\":\"" + (amount + tamount) + "\"}";

                StartCoroutine(PostRequestCoroutine(url, json));
                GameObject tost = Instantiate(TradeLostAndEarn);
                parentobj = GameObject.Find("Canvas/MainUI");
                tost.transform.SetParent(parentobj.transform);
                tost.transform.localScale = new Vector3(1, 1, 1);
                tost.transform.localPosition = new Vector3(0, 0, 0);
                RectTransform rt = tost.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(0, 0);
                tost.SetActive(true);
                tost.transform.gameObject.SetActive(true);
                tost.transform.GetChild(1).GetChild(6).GetComponent<Text>().text =  (amount + tamount).ToString();
                Color green = new Color32(25, 163, 3, 255);
                tost.transform.GetChild(1).GetChild(6).GetComponent<Text>().color = green;
                if (Timewhentradeapply > 2)
                {
                    tost.transform.GetChild(1).GetChild(7).GetComponent<Text>().text = "30 sec";
                }
                else if (Timewhentradeapply == 1)
                {
                    tost.transform.GetChild(1).GetChild(7).GetComponent<Text>().text = "1 min";
                }
                else if (Timewhentradeapply == 2)
                {
                    tost.transform.GetChild(1).GetChild(7).GetComponent<Text>().text = "2 min";
                }
                tost.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Congratulation, Your forecast is correct your profit is";
                tost.transform.GetComponent<AudioSource>().Play();
                Destroy(tost.gameObject, 10);
                AppLoad.TradeNumCount -= 1;
                //StartCoroutine("hidenotify");

                if (mainui.tcount == 0)
                {
                    ui.OpenStarterkit();
                    mainui.tcount++;
                }

            }
               // else if (newValueeeeeeeeeeee == send)
               else if (compare ==0)
                {
                
                     amount = 0;
                    amount = tamount;
                    Debug.Log("Upearn=:" + amount);
                    string json = "{\"id\":\"" + id + "\",\"endpoint\":\"" + GraphChart.Yvalue + "\",\"earn\":\"" + amount + "\"}";
                    StartCoroutine(PostRequestCoroutine(url, json));

                PlayerPrefs.SetFloat("Total", PlayerPrefs.GetFloat("Total") + amount);
                GameObject tost = Instantiate(TradeLostAndEarn);
                parentobj = GameObject.Find("Canvas/MainUI");
                tost.transform.SetParent(parentobj.transform);
                tost.transform.localScale = new Vector3(1, 1, 1);
                tost.transform.localPosition = new Vector3(0, 0, 0);
                RectTransform rt = tost.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(0, 0);
                tost.SetActive(true);
                tost.transform.gameObject.SetActive(true);
                tost.transform.GetChild(1).GetChild(6).GetComponent<Text>().text =  amount.ToString() ;
                Color green = new Color32(25, 163, 3, 255);
                tost.transform.GetChild(1).GetChild(6).GetComponent<Text>().color = green;
                if (Timewhentradeapply > 2)
                {
                    tost.transform.GetChild(1).GetChild(7).GetComponent<Text>().text = "30 sec";
                }
                else if (Timewhentradeapply == 1)
                {
                    tost.transform.GetChild(1).GetChild(7).GetComponent<Text>().text = "1 min";
                }
                else if (Timewhentradeapply == 2)
                {
                    tost.transform.GetChild(1).GetChild(7).GetComponent<Text>().text = "2 min";
                }
                tost.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Congratulation, Your forecast is correct your profit is";
                tost.transform.GetComponent<AudioSource>().Play();
                Destroy(tost.gameObject, 10);
                AppLoad.TradeNumCount -= 1;
                //StartCoroutine("hidenotify");
                if (mainui.tcount == 0)
                {
                    ui.OpenStarterkit();
                    mainui.tcount++;
                }

            }
                else
                {
                    amount = 0;
                    Debug.Log("Upearn:" + amount);
                    string json = "{\"id\":\"" + id + "\",\"endpoint\":\"" + GraphChart.Yvalue + "\",\"earn\":\"" + amount + "\"}";
                    StartCoroutine(PostRequestCoroutine(url, json));

                GameObject tost = Instantiate(TradeLostAndEarn);
                parentobj = GameObject.Find("Canvas/MainUI");
                tost.transform.SetParent(parentobj.transform);
                tost.transform.localScale = new Vector3(1, 1, 1);
                tost.transform.localPosition = new Vector3(0, 0, 0);
                RectTransform rt = tost.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(0, 0);

                tost.SetActive(true);
                tost.transform.gameObject.SetActive(true);
                tost.transform.GetChild(1).GetChild(6).GetComponent<Text>().text =  amount.ToString();
                Color red = new Color32(204, 4, 4, 255);
                tost.transform.GetChild(1).GetChild(6).GetComponent<Text>().color = red;

                if (Timewhentradeapply > 2)
                {
                    tost.transform.GetChild(1).GetChild(7).GetComponent<Text>().text = "30 sec";
                }else if(Timewhentradeapply == 1)
                {
                    tost.transform.GetChild(1).GetChild(7).GetComponent<Text>().text = "1 min";
                }else if(Timewhentradeapply == 2)
                {
                    tost.transform.GetChild(1).GetChild(7).GetComponent<Text>().text = "2 min";
                }
                
                tost.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Sorry !   Your   Forecast  is  incorrect   Your   profit  is";
                Destroy(tost.gameObject, 10);
                AppLoad.TradeNumCount -= 1;

               

            }


            

        }
      
        Destroy(this.gameObject);
       
    }

   
    // Update is called once per frame
   private void Update()
    {
        // time -= Time.deltaTime;
        this.transform.position = new Vector2(this.transform.position.x-Time.deltaTime/ 2, this.transform.position.y);
       


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
                Debug.LogError(string.Format("{0}: {1}", www.url, www.error));
          
            Debug.Log("Network Fail");
        }
            else
            {
                JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());
                // Debug.Log(jsonNode["status"]+"and"+ jsonNode["message"]);
                if (jsonNode["status"] == "0")
                {
              
                Debug.Log("Success");
                }
                else
                {
              
                Debug.Log("Fail");
            }
                //  Debug.Log(www.downloadHandler.text);
            }
        

      

    }
}
