using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubblingItem : MonoBehaviour
{

    Image img;

    RectTransform rectTrans;
    public void Init()
    {
        rectTrans = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        rectTrans.sizeDelta = new Vector2(1, BubblingData.height);

    }
    public void SetBubble(int pos, int height)
    {
        rectTrans.anchoredPosition = new Vector2(pos,  height - BubblingData.height);
        img.color = Color.Lerp(Color.yellow, Color.red, (float)height / BubblingData.height);
    }
}
