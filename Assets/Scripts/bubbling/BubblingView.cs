using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblingView : MonoBehaviour {

    string bubblePath = "Bubbling/bubble";

    BubblingItem[] dataViews;
    public void Init(int[] data)
    {
        dataViews = new BubblingItem[data.Length];
       var go= Resources.Load<GameObject>(bubblePath);
        for (int i = 0; i < data.Length; i++)
        {
            var instance = Instantiate<GameObject>(go, this.transform);
            dataViews[i] = instance.GetComponent<BubblingItem>();
            dataViews[i].Init();
           dataViews[i].SetBubble(i, data[i]);
        }  
    }

    public void RefreshData(int[]data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            dataViews[i].SetBubble(i,data[i]);
        }
    }
}
