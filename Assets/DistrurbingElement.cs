using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrurbingElement : MonoBehaviour
{
    
    RectTransform rectTransform;
    public float angle;
    public AudioSource music;
    private void Start()
    {

         rectTransform = GetComponent<RectTransform>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DisturbungElement"))
        {
            music.volume = PlayerPrefs.GetFloat("music");
            music.Play();
            
            rectTransform.Rotate(new Vector3(0, 0, angle));
            //Debug.Log("in");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DisturbungElement"))
        {
          
            rectTransform.Rotate(new Vector3(0, 0, -angle));
            //Debug.Log("out");
        }
    }
}
