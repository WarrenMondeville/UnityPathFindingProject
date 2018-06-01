using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblingData
{
    public static int width, height;
    public int[] bubblingNum;
    public BubblingData(int num,int rang)
    {
        width = num;
        height = rang;
        bubblingNum = new int[num];
        for (int i = 0; i < bubblingNum.Length; i++)
        {
            bubblingNum[i] = Random.Range(0, rang);
        }
    }



    void Init()
    {
        for (int i = 0; i < bubblingNum.Length; i++)
        {
            bubblingNum[i] = Random.Range(0, 1000);
        }
    }
}
