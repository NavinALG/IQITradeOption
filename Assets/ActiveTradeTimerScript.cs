using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ActiveTradeTimerScript : MonoBehaviour
{

    public float TimeGiven;
    public float localValueGivenForTimer,LocalTimeC1,LocalTimeC2;
    // Start is called before the first frame update
    void Start()
    {

        if(TimeGiven == 30)
        {
            localValueGivenForTimer = 30;
        }else if(TimeGiven == 1)
        {
            localValueGivenForTimer = 60;
        }else if(TimeGiven == 2)
        {
            localValueGivenForTimer = 120;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (localValueGivenForTimer >= 0 && localValueGivenForTimer <= 30)
        {
            //Debug.Log(localValueGivenForTimer);
            localValueGivenForTimer -= Time.deltaTime;
            if (localValueGivenForTimer <= 9)
            {
                transform.GetChild(2).GetComponent<Text>().text = "00:" + "0" + localValueGivenForTimer.ToString("F0");
            }
            else
            {
                transform.GetChild(2).GetComponent<Text>().text = "00:" + localValueGivenForTimer.ToString("F0");
            }
            if (localValueGivenForTimer < 0 )
            {
                Destroy(gameObject);
            }
        }




        else if(localValueGivenForTimer >=0 && localValueGivenForTimer <= 60)
        {
           
            localValueGivenForTimer -= Time.deltaTime;
            //Debug.Log(localValueGivenForTimer);
            if (localValueGivenForTimer <= 9)
            {
                transform.GetChild(2).GetComponent<Text>().text = "00:" + "0" + localValueGivenForTimer.ToString("F0");
            }
            else
            {
                transform.GetChild(2).GetComponent<Text>().text = "00:" + localValueGivenForTimer.ToString("F0");
            }
           
            if (localValueGivenForTimer < 0)
            {
                Destroy(gameObject);
            }
        }







        else if (localValueGivenForTimer >= 0 && localValueGivenForTimer <= 120)
        {
           
            localValueGivenForTimer -= Time.deltaTime;
            if (localValueGivenForTimer > 61)
            {
                

                LocalTimeC1 -= Time.deltaTime;

                if (LocalTimeC1 <= 9)
                {
                    transform.GetChild(2).GetComponent<Text>().text = "01:" + "0" + LocalTimeC1.ToString("F0");
                }
                else
                {
                    transform.GetChild(2).GetComponent<Text>().text = "01:" + LocalTimeC1.ToString("F0");
                }
            }
            else
            {
               

                LocalTimeC2 -= Time.deltaTime;
                transform.GetChild(2).GetComponent<Text>().text = "00:" + LocalTimeC2.ToString("F0");
            }

            if (localValueGivenForTimer < 0)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
