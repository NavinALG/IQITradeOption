using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gift : MonoBehaviour
{
    private int ActiveAd;
    private int lovin, unity, chartboost,ShareFriends;
    public Text ActiveAdDisplay;
    private void Start()
    {
        
        UpdateRemaningAD();

    }
    public void UpdateRemaningAD()
    {
        if(PlayerPrefs.GetInt("watch") == 0)
        {
            lovin = 1;
        }else
        {
            lovin = 0;
        }
        if (PlayerPrefs.GetInt("unitywatch") == 0)
        {
            unity = 1;
        }
        else
        {
            unity = 0;
        }
        if (PlayerPrefs.GetInt("chartboostwatch") == 0)
        {
            chartboost = 1;
        }
        else
        {
            chartboost = 0;
        }
        ShareFriends = 1;
        ActiveAd = lovin+ unity+ chartboost + ShareFriends;
        
        //Debug.Log(ActiveAd);
    }

    private void Update()
    {
        ActiveAdDisplay.text = ActiveAd.ToString();
    }
    public void GiftTimerOFF()
    {

        gameObject.SetActive(false);
    }
}
