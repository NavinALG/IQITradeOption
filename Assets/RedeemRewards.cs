using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RedeemRewards : MonoBehaviour
{
    // Start is called before the first frame update
   
    void Start()
    {
        
    }
    private IEnumerator PostRequestCoroutine(string url, string json,string name)
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


            /* msg.transform.GetChild(0).GetComponent<Text>().text = "Network error!";
             msg.SetActive(true);
             StartCoroutine("hidenotification");*/
        }
        else
        {
            JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());

            if (jsonNode["status"] == "0")
            {
                float tamount = float.Parse(jsonNode["iap"]) + float.Parse(jsonNode["tradecredits"]);
                Debug.Log("Earningfromtd:"+tamount+"andmin:"+ Ui.minearn);
               if (tamount>=Ui.minearn)
                {
                    if (name == "PayPal")
                    {

                        GameObject popup = GameObject.Find("Canvas/MainUI/Withdraw PayPal");
                        popup.SetActive(true);
                        popup.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = this.transform.GetChild(2).GetComponent<Text>().text;
                        popup.transform.GetChild(0).transform.GetChild(5).GetComponent<Text>().text = this.transform.GetChild(1).GetComponent<Text>().text;

                    }
                    else
                    {
                        GameObject popup = GameObject.Find("Canvas/MainUI/Withdraw Paytm");
                        popup.transform.GetChild(0).transform.GetChild(5).GetComponent<Text>().text = this.transform.GetChild(1).GetComponent<Text>().text;
                        popup.SetActive(true);
                        popup.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = this.transform.GetChild(2).GetComponent<Text>().text;
                    }
                }
               else
                {
                    GameObject.Find("Canvas/Notify/NetworkErrorMessage").transform.GetChild(0).GetComponent<Text>().text = "You  have earn minimun"+Ui.minearn+" from trade";
                    GameObject.Find("Canvas/Notify/NetworkErrorMessage").SetActive(true);
                    StartCoroutine("hidenotification");
                }

            }
            else
            {
                GameObject.Find("Canvas/Notify/NetworkErrorMessage").transform.GetChild(0).GetComponent<Text>().text = "Somthing going wrong!";
                GameObject.Find("Canvas/Notify/NetworkErrorMessage").SetActive(true);
                StartCoroutine("hidenotification");

            }

        }

    }
    public void redeem(string name)
    {
      // Debug.Log("Paypal");
       // int value = int.Parse(this.transform.GetChild(0).GetComponent<Text>().text);
        int value = int.Parse(this.gameObject.transform.GetChild(1).GetComponent<Text>().text);
        
        if (PlayerPrefs.GetFloat("Total") >= value)
        {
            var url = "https://gamelovin.com/tradeoption/Trading/Userprocess/walletDetails";
            string json = "{\"email\":\"" + PlayerPrefs.GetString("email") + "\"}";
            StartCoroutine(PostRequestCoroutine(url, json,name));

           
        }
        else
        {
        
            GameObject.Find("Canvas/Notify/NetworkErrorMessage").transform.GetChild(0).GetComponent<Text>().text = "Not enough coins!";
            GameObject.Find("Canvas/Notify/NetworkErrorMessage").SetActive(true);
            StartCoroutine("hidenotification");
        }
          
    }
    IEnumerator hidenotification()
    {
        yield return new WaitForSeconds(2);
        GameObject.Find("Canvas/Notify/NetworkErrorMessage").SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
