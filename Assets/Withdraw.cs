using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Withdraw : MonoBehaviour
{
    // Start is called before the first frame update
    int coins,id;
    public GameObject loading, msg;
    public InputField t_id;

    void Start()
    {
        
    }
    public void cancel()
    {
        this.gameObject.SetActive(false);
    }
    public void process(string name)
    {
        if(t_id.text!="")
        {
           
            loading.SetActive(true);
            var url = "https://gamelovin.com/tradeoption/Trading/Userprocess/insertredeemrequest";
            // string json = "{\"id\":\"" + PlayerPrefs.GetString("uniqueid") + "\"}";
            string json = "{\"id\":\"" + this.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text + "\",\"user_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"t_type\":\"" + name + "\",\"t_id\":\"" + t_id.text + "\",\"coins\":\"" + this.transform.GetChild(0).transform.GetChild(5).GetComponent<Text>().text + "\"}";
            StartCoroutine(PostRequestinsertCoroutine(url, json));
        }
        else
        {
            if(name=="PayPal")
            {
                msg.transform.GetChild(0).GetComponent<Text>().text = "Plese enter valid paypal email id!";
            }
            else
            {
                msg.transform.GetChild(0).GetComponent<Text>().text = "Plese enter valid paytm number!";
            }
           
            msg.SetActive(true);
            StartCoroutine("hidenotification");
        }
       
       
    }
    IEnumerator hidenotification()
    {
        yield return new WaitForSeconds(2);
        msg.SetActive(false);
    }
    private IEnumerator PostRequestinsertCoroutine(string url, string json)
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
          
            if (jsonNode["status"] == "0")
            {
                int coins = int.Parse(this.transform.GetChild(0).transform.GetChild(5).GetComponent<Text>().text);
                PlayerPrefs.SetFloat("Total", PlayerPrefs.GetFloat("Total") - coins);
                msg.transform.GetChild(0).GetComponent<Text>().text = "Success";
                msg.SetActive(true);
                StartCoroutine("hidenotification");
                this.gameObject.SetActive(false);


            }
            else
            {
               
                msg.transform.GetChild(0).GetComponent<Text>().text = "User profile not completed!";
                msg.SetActive(true);
                StartCoroutine("hidenotification");

            }
           
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
