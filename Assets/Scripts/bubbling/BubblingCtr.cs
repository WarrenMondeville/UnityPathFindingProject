using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CalcMethod {
    BubblingSort1,
    BubblingSort2,
    SelectionSort,
    InsertionSort,
    ShellSort,
    MergeSort,

    CountingSort,
}
public class BubblingCtr : MonoBehaviour {

    public CalcMethod method;
    public BubblingView bubblingView;
    BubblingData bubblingData;
    public  int height, width;
	void Start () {
       var canvas= GetComponent<Canvas>();
        height = (int)canvas.pixelRect.height;
        width = (int)canvas.pixelRect.width;
        bubblingData = new BubblingData(width,height);
        bubblingView.Init(bubblingData.bubblingNum);
        //  LogDatas(bubblingData.bubblingNum);
        StartCalc();
    }

    void StartCalc()
    {
        switch (method)
        {
            case CalcMethod.BubblingSort1:
                StartCoroutine(BubblingCal1(bubblingData.bubblingNum));
                break;
            case CalcMethod.BubblingSort2:
                StartCoroutine(BubblingCal2(bubblingData.bubblingNum));
                break;
            case CalcMethod.SelectionSort:
              StartCoroutine(SelectionSort(bubblingData.bubblingNum));
                break;
            case CalcMethod.InsertionSort:
                StartCoroutine(InsertionSort(bubblingData.bubblingNum));
                break;
            case CalcMethod.ShellSort:
                StartCoroutine(ShellSort(bubblingData.bubblingNum));
                break;
            case CalcMethod.CountingSort:
                StartCoroutine(CountingSort(bubblingData.bubblingNum,height));
                break;
            default:
                break;
        }
    }
    
    IEnumerator BubblingCal1(int[] data)
    {
        for (int i = 0; i < data.Length - 1; i++)
        {
            for (int j = 0; j < data.Length - 1 - i; j++)// j开始等于0，  
            {
                if (data[j] < data[j + 1])
                {
                    int temp = data[j];
                    data[j] = data[j + 1];
                    data[j + 1] = temp;
                    bubblingView.RefreshData(data);
                    yield return null;
                }
            }
            yield return null;

        }
    }

    IEnumerator BubblingCal2(int[] data)
    {
        for (int i = 0; i < data.Length - 1; i++)
        {
            for (int j = (data.Length - 2); j >= i; j--)
            {
                if (data[j] < data[j + 1])
                {
                    int temp = data[j];
                    data[j] = data[j + 1];
                    data[j + 1] = temp;
                    bubblingView.RefreshData(data);
                    yield return null;
                }
            }
            yield return null;// new WaitForEndOfFrame();
        }
    }


    IEnumerator SelectionSort(int[] arr)
    {
        var len = arr.Length;
        int minIndex, temp;
        for (var i = 0; i < len - 1; i++)
        {
            minIndex = i;
            for (var j = i + 1; j < len; j++)
            {
                if (arr[j] < arr[minIndex])
                {     // 寻找最小的数
                    minIndex = j;                 // 将最小数的索引保存
                }
            }
            temp = arr[i];
            arr[i] = arr[minIndex];
            arr[minIndex] = temp;
            bubblingView.RefreshData(arr);
            yield return null;
        }

    }

    IEnumerator InsertionSort(int[] arr)
    {
        var len = arr.Length;
        int preIndex, current;
        for (var i = 1; i < len; i++)
        {
            preIndex = i - 1;
            current = arr[i];
            while (preIndex >= 0 && arr[preIndex] > current)
            {
                arr[preIndex + 1] = arr[preIndex];
                preIndex--;
                bubblingView.RefreshData(arr);
                yield return null;
            }
            yield return null;
            arr[preIndex + 1] = current;
        }
       
    }

   IEnumerator  ShellSort(int[] arr)
    {

        int d = arr.Length;//gap的值  

        while (true)
        {

            d = d / 2;//每次都将gap的值减半  

            for (int x = 0; x < d; x++)
            {//对于gap所分的每一个组  

                for (int i = x + d; i < arr.Length; i = i + d)
                {      //进行插入排序  

                    int temp = arr[i];

                    int j;

                    for (j = i - d; j >= 0 && arr[j] > temp; j = j - d)
                    {

                        arr[j + d] = arr[j];

                    }

                    arr[j + d] = temp;
                    bubblingView.RefreshData(arr);
                    yield return null;
                }

            }

            if (d == 1)
            {//gap==1，跳出循环  

                break;

            }

        }

    }

    List<int> MergeSort(List<int> x)
    {

        List<int> left = new List<int>();
        List<int> right = new List<int>();
        List<int> together = new List<int>();
        if (x.Count < 2)
        {
            return x;
        }
        else
        {
            int middle = x.Count / 2;
            for (int i = 0; i < middle; i++)
            {
                left.Add(x[i]);
            }
            for (int i = middle; i < x.Count; i++)
            {
                right.Add(x[i]);
            }
            List<int> a = MergeSort(left);
            List<int> b = MergeSort(right);
            together = merge(a, b);
        }
        return together;
    }
     List<int> merge(List<int> left, List<int> right)
    {
        List<int> result = new List<int>();
        int i = 0, j = 0;
        while (i < left.Count && j < right.Count)
        {
            if (left[i] <= right[j])
            {
                result.Add(left[i]);
                i++;
            }
            else
            {
                result.Add(right[j]);
                j++;
            }
        }
        while (i < left.Count)
        {
            result.Add(left[i]);
            i++;
        }
        while (j < right.Count)
        {
            result.Add(right[j]);
            j++;
        }
        return result;
    }

    IEnumerator CountingSort(int[] arr, int maxValue)
    {
        var bucket = new int[maxValue + 1];
           int sortedIndex = 0;
        int arrLen = arr.Length,
        bucketLen = maxValue + 1;

        for (var i = 0; i < arrLen; i++)
        {
            if (bucket[arr[i]]==0)
            {
                bucket[arr[i]] = 0;
            }
            bucket[arr[i]]++;
        }

        for (var j = 0; j < bucketLen; j++)
        {
            while (bucket[j] > 0)
            {
                arr[sortedIndex++] = j;
                bucket[j]--;
                bubblingView.RefreshData(arr);
                yield return null;
            }
        }

      
    }


    void LogDatas(int[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            Debug.Log(data[i]);
        }
    }


}
