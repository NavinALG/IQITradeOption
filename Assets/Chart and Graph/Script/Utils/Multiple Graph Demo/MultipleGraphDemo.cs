using UnityEngine;
using System.Collections;
using ChartAndGraph;
using System.Collections.Generic;
using UnityEngine.UI;
public class MultipleGraphDemo : MonoBehaviour
{

    public GraphChart Graph;
    public GraphAnimation Animation;
    public int TotalPoints = 5;
    float lastTime = 0f;
    float lastX = 0f;
    public float OurTime;
    public Text Data;
    public GameObject DynamicDataFollow;
    public GraphChart TransformTaken;
    void Start()
    {
        if (Graph == null) // the ChartGraph info is obtained via the inspector
            return;
        List<Vector2> animationPoints = new List<Vector2>();
        float x = 0f;
        Graph.HorizontalValueToStringMap.Add(10, "Ten");
        Graph.VerticalValueToStringMap.Add(10, "$$");
        //Graph.DataSource.StartBatch(); // calling StartBatch allows changing the graph data without redrawing the graph for every change
        //Graph.DataSource.ClearCategory("Player 1"); // clear the "Player 1" category. this category is defined using the GraphChart inspector
        Graph.DataSource.ClearCategory("Player 1"); // clear the "Player 2" category. this category is defined using the GraphChart inspector
        for (int i = 0; i < TotalPoints; i++)  //add random points to the graph
        {
            //Graph.DataSource.AddPointToCategory("Player 1", x, Random.value * 20f + 10f); // each time we call AddPointToCategory 
            //animationPoints.Add(new Vector2(x, Random.value * 10f));
            Graph.DataSource.AddPointToCategory("Player 1", x, Random.value ); // each time we call AddPointToCategory 
            x += Random.value * 3f;
            lastX = x;
        }
        // Graph.DataSource.EndBatch(); // finally we call EndBatch , this will cause the GraphChart to redraw itself
        // if (Animation != null)
        //{
        //   Animation.Animate("Player 2",animationPoints,3f);
        //}
    
       
       
    }
    public static float value;
    void Update()
    {
        float time = Time.time;
        if (lastTime + OurTime < time)
        {
            lastTime = time;
            lastX += Random.value * 3f;
            value = Random.value;
            //Debug.Log(value);
            //Graph.DataSource.AddPointToCategoryRealtime("Player 1", lastX, Random.value * 20f + 10f, 1f); // each time we call AddPointToCategory 
            Graph.DataSource.AddPointToCategoryRealtime("Player 1", lastX, value, 1f); // each time we call AddPointToCategory
            //Debug.Log(value);
            Data.text = value.ToString("F1");
        }
       
    }

    public void Click()
    {
      /*  for (int i = 0; i < TransformTaken.mTransformed.Count-1; i++)
        {
           
        }
        Vector2 dyanamic = new Vector2(DynamicDataFollow.transform.position.x, DynamicDataFollow.transform.position.y);
        Vector2 myLocation = new Vector2((TransformTaken.mTransformed[TransformTaken.mTransformed.Count - 1]).x, (TransformTaken.mTransformed[TransformTaken.mTransformed.Count - 1]).y);
        dyanamic.y = myLocation.y;
        DynamicDataFollow.transform.position = dyanamic;
        Debug.Log(dyanamic.y);
        Debug.Log(value);*/
    }
}
