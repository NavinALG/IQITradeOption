using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;
using SimpleJSON;

public class SpinWheel : MonoBehaviour
{

    public GameObject SpinPanel, Buy2btn, Buy10btn;
    private bool _isStarted;
    private float[] _sectorsAngles;
    private float _finalAngle;
    private float _startAngle = 0;
    private float _currentLerpRotationTime;
    public Button TurnButton;
    public GameObject Circle;           // Rotatable Object with rewards
    public GameObject Arrow;
    bool IsReady;
    public Text RewaredDisplay;
    private float RewardedAmount;
    public GameObject Won;
   
   
    //LIGHTNING DATA
   // public GameObject Rainbow;
    public float RaibowSpeed;
    public GameObject GlowMiddle, Reward;
 
    public GameObject[] LightInMiddle;
    public float TimeForMiddleLight;
    public GameObject BackWheel;
    public float BackWheelSpeed;
    public GameObject ShouldbeOffWhenPlay;
    //RFERENCES
   

    //COIN GOES UP
   
    public GameObject CoinHolder;
    public float currentTime = 0.0f;
    public Transform SpawnPOint;
    public float timeToSpawn = 5.0f;
    //REWARD LIGHT DATA
    public GameObject CoinParticle,TresureBox,GlowStarLOOP,GlowStarInstnant;
    public GameObject MiddleElipsed,PopUp;
    public float SpeedElipsed;

    public Text totalSpintxt,PanelSpinTxt;
    public Text[] wheelamounttext;
    public GameObject SpinBtn,SpinWheelPanel;
    private void Awake()
    {
       
    }

    private IEnumerator PostRequestSpinamountCoroutine(string url, string json)
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



        }
        else
        {

            JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());
            // Debug.Log(jsonNode["status"]+"and"+ jsonNode["message"]);
            if (jsonNode["status"] == "0")
            {
                for(int i=0;i< wheelamounttext.Length;i++)
                {
                    wheelamounttext[i].text = jsonNode["r" + (i+1)];
                }
                SpinBtn.SetActive(true);
                SpinWheelPanel.SetActive(true);
            }
            else
            {
                SpinWheelPanel.SetActive(false);
                SpinBtn.SetActive(false);

            }

        }

    }

    private void Start()
    {
        var url =AppLoad.BaseUrl+"GetSpin";
        string json = "{\"name\":\"Spin\"}";
        StartCoroutine(PostRequestSpinamountCoroutine(url, json));
        RaibowSpeed = 4f;
        IsReady = true;
        Arrow.SetActive(true);
       
        Won.SetActive(false);
      //  Rainbow.SetActive(true);
        GlowMiddle.SetActive(false);
        MiddleElipsed.SetActive(true);
        CoinParticle.SetActive(false);
        GlowStarLOOP.SetActive(false); 
        TresureBox.SetActive(false);
        GlowStarInstnant.SetActive(false);
        InvokeRepeating("MiddleLightFunction", TimeForMiddleLight, TimeForMiddleLight);
        
        CoinHolder.SetActive(false);
       

    }

    int NextLight;
    public void MiddleLightFunction()
    {
        NextLight += 1;
        if (NextLight >= 16)
        {
            NextLight = 0;
          

        }

        for (int i = 0; i < LightInMiddle.Length; i++)
        {
            LightInMiddle[i].SetActive(false);
            LightInMiddle[NextLight].SetActive(true);
            
        }

    }


    public void TurnWheel()
    {


        if(PlayerPrefs.GetInt("spin")>0)
        { 
        Debug.Log("clicked");

        Arrow.SetActive(false);
        TurnButton.interactable = false;
        _currentLerpRotationTime = 0f;
      
        GlowMiddle.SetActive(true);
        RaibowSpeed = 10f;

        ShouldbeOffWhenPlay.SetActive(false);

        _sectorsAngles = new float[] { 36, 72, 108, 144, 180, 216, 252, 288, 324, 360 };

        int fullCircles = 5;
        int Rno = UnityEngine.Random.Range(0, 200);
        int Rno1;
        if (Rno < 198)
        {
            Rno1 = UnityEngine.Random.Range(0, 9);
            if (Rno1 == 1 || Rno == 7)
            {
                if (Rno > 100)
                {
                    Rno1 = UnityEngine.Random.Range(5, 6);
                }
                else if (Rno >= 50 && Rno <= 100)
                {
                    Rno1 = UnityEngine.Random.Range(2, 4);
                }
                else
                {
                    Rno1 = UnityEngine.Random.Range(8, 9);
                }

            }
        }
        else if (Rno >= 198 && Rno < 200)
        {
            Rno1 = 0;
        }
        else
        {
              if(spincount>=30)
                {
                    Rno1 = 1;
                    spincount = 0;
                }
                else
                {
                    Rno1= UnityEngine.Random.Range(3, 6);
                }
           
        }
            
           //float randomFinalAngle = _sectorsAngles[UnityEngine.Random.Range(0, _sectorsAngles.Length)];
           float randomFinalAngle = _sectorsAngles[Rno1];

        _finalAngle = -(fullCircles * 360 + randomFinalAngle);
        _isStarted = true;

        IsReady = false;
        Reward.SetActive(false);


        StartCoroutine(HideCoinsDelta());
        }
    }
   
    public float maxLerpRotationTime = 4f;
    void Update()
    {
        totalSpintxt.text = PlayerPrefs.GetInt("spin").ToString();
        PanelSpinTxt.text=PlayerPrefs.GetInt("spin").ToString();
        if (PlayerPrefs.GetInt("spin")<=0)
        {
            SpinPanel.SetActive(true);
            if(PlayerPrefs.GetInt("totalspin")==0)
            {
                Buy2btn.SetActive(true);
                Buy10btn.SetActive(false);
            }
            else
            {
                Buy2btn.SetActive(false);
                Buy10btn.SetActive(true);
            }
        }else
        {
            SpinPanel.SetActive(false);
            Buy2btn.SetActive(false);
            Buy10btn.SetActive(false);
        }

      
        //Rainbow.transform.Rotate(Vector3.back * RaibowSpeed);
        BackWheel.transform.Rotate(Vector3.forward * BackWheelSpeed * Time.deltaTime);
        MiddleElipsed.transform.Rotate(Vector3.forward * SpeedElipsed);
        if (!_isStarted)
            return;
        
        _currentLerpRotationTime += Time.deltaTime;
        if (_currentLerpRotationTime > maxLerpRotationTime || Circle.transform.eulerAngles.z == _finalAngle)
        {
            _currentLerpRotationTime = maxLerpRotationTime;
            _isStarted = false;
            _startAngle = _finalAngle % 360;

            GiveAwardByAngle();
            StartCoroutine(HideCoinsDelta());
        }

        float t = _currentLerpRotationTime / maxLerpRotationTime;

        t = t * t * t * (t * (6f * t - 15f) + 10f);

        float angle = Mathf.Lerp(_startAngle, _finalAngle, t);
        Circle.transform.eulerAngles = new Vector3(0, 0, angle);


     
     
    }

    private void GiveAwardByAngle()
    {
        // Here you can set up rewards for every sector of wheel
        switch ((int)_startAngle)
        {

            case 0:
                RewardCoins(int.Parse(wheelamounttext[1].text));
                break;
            case -324:
                RewardCoins(int.Parse(wheelamounttext[2].text));
                break;
            case -288:
                RewardCoins(int.Parse(wheelamounttext[3].text));
                break;
            case -252:
                RewardCoins(int.Parse(wheelamounttext[4].text));
                break;
            case -216:
                RewardCoins(int.Parse(wheelamounttext[5].text));
                break;
            case -180:
                RewardCoins(int.Parse(wheelamounttext[6].text));
                break;
            case -144:
                RewardCoins(int.Parse(wheelamounttext[7].text));
                break;
            case -108:
                RewardCoins(int.Parse(wheelamounttext[8].text));
                break;
            case -72:
                RewardCoins(int.Parse(wheelamounttext[9].text));
                break;
            case -36:
                RewardCoins(int.Parse(wheelamounttext[0].text));
                break;
            default:
                RewardCoins(1000);
                break;
        }
    }
    private IEnumerator PostRequestSpincreditsCoroutine(string url, string json,float amount)
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
                PlayerPrefs.SetInt("spin", PlayerPrefs.GetInt("spin") - 1);
                PlayerPrefs.SetFloat("Total", PlayerPrefs.GetFloat("Total")+amount);
                spincount++;
            }
            else
            {
              

            }

        }

    }
    public static int spincount;
    private void RewardCoins(int awardCoins)
    {
       
        
        StartCoroutine("wait");
        RewardedAmount = awardCoins;
       
        GlowMiddle.SetActive(false);
        RewaredDisplay.text = RewardedAmount.ToString("F0")+" Coins";
        TurnButton.interactable = true;
        ShouldbeOffWhenPlay.SetActive(true);

        StartCoroutine("CoinParticleEffectFinish");

        var url2 =AppLoad.BaseUrl+"SpinUpdate";
        string json2 = "{\"unique_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"coins\":\""+ awardCoins + "\"}";
        StartCoroutine(PostRequestSpincreditsCoroutine(url2, json2, awardCoins));
        
      



    }

    IEnumerator HidePoup()
    {
        yield return new WaitForSeconds(2);
        PopUp.SetActive(false);
    }
    public float CustomTime;
    IEnumerator wait()
    {
        Debug.Log("call up");
        RaibowSpeed = 4f;
        Reward.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("music");
        Reward.SetActive(true);
        Reward.GetComponent<AudioSource>().Play();
        Won.SetActive(true);
        yield return new WaitForSeconds(CustomTime);
        Won.SetActive(false);
        Arrow.SetActive(true);
        IsReady = true;
    }
    private IEnumerator HideCoinsDelta()
    {
        yield return new WaitForSeconds(1f);
       // CoinsDeltaText.gameObject.SetActive(false);
    }


    public void showArrow()
    {
        Arrow.SetActive(true);
    }

   
    
    public IEnumerator CoinParticleEffectFinish()
    {
        yield return new WaitForSeconds(1);
        CoinParticle.SetActive(true);
        yield return new WaitForSeconds(1.5f);
       
        TresureBox.SetActive(true);
        GlowStarInstnant.SetActive(false);
        CoinParticle.SetActive(false); GlowStarLOOP.SetActive(true);
        yield return new WaitForSeconds(1);
       
        GlowStarInstnant.SetActive(false);
        StartCoroutine("coinenum");
       





    }
   
    IEnumerator coinenum()
    {
        yield return new WaitForSeconds(0.2f);
       
        CoinHolder.SetActive(true);
       
        yield return new WaitForSeconds(1);
        //CancelInvoke();
        

    }
  

    public void Reset()
    {

        TresureBox.SetActive(false);
        GlowStarInstnant.SetActive(false);
        GlowStarLOOP.SetActive(false);
        CoinHolder.SetActive(true);
        CoinParticle.SetActive(false);
       
    }
}