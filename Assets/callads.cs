using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class callads : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject fbAudiance;
    public ChartBoost chartbst;
    public static int rd = 0;
    void Start()
    {
       // fbAudiance = GameObject.Find("Canvas/MainUI/GraphHolder");
       
    }
    public void ShowChartbstinterstitial()
    {
        if(rd==0)
        {
            chartbst.ChartboostInterstitial();
            rd = 1;
        }else
        {
            rd = 0;
        }
        
    }
    public void showinterstitialads()
    {

      //  fbAudiance.GetComponent<InterstitialAdScene>().ShowInterstitial();
       
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }


}
