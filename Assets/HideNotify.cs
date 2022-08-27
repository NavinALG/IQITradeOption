using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideNotify : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(hide());
    }
    IEnumerator hide()
    {
        yield return new WaitForSeconds(4);
        if(this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
       
    }
    private void OnEnable()
    {
        StartCoroutine(hide());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
