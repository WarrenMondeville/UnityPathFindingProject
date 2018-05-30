 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour {

    private void Start()
    {
        PriorityQueue<Node> queue = new PriorityQueue<Node>();
        queue.Enqueue(new Node(1.3f));
        queue.Enqueue(new Node(1.1f));
        queue.Enqueue(new Node(1.2f));
        queue.Enqueue(new Node(1.1f));
        queue.Enqueue(new Node(1.0f));
        queue.Enqueue(new Node(1.1f));
        queue.Enqueue(new Node(1.4f));
        //    Test();
    }

    void Test()
    {
        //创建一个队列   
        Queue<string> myQ = new Queue<string>();
        myQ.Enqueue("The");//入队   
        myQ.Enqueue("quick");
        myQ.Enqueue("brown");
        myQ.Enqueue("fox");
        myQ.Enqueue(null);//添加null   
        myQ.Enqueue("fox");//添加重复的元素   

        // 打印队列的数量和值   
        Debug.Log("myQ");
        Debug.LogFormat("Count:    {0}", myQ.Count);

        // 打印队列中的所有值   
        Debug.Log("Queue values:");
        PrintValues(myQ);

        // 打印队列中的第一个元素，并移除   
        Debug.LogFormat("(Dequeue){0}", myQ.Dequeue());

        // 打印队列中的所有值   
        Debug.Log("Queue values:");
        PrintValues(myQ);

        // 打印队列中的第一个元素，并移除   
        Debug.LogFormat("(Dequeue){0}", myQ.Dequeue());

        // 打印队列中的所有值   
        Debug.Log("Queue values:");
        PrintValues(myQ);

        // 打印队列中的第一个元素   
        Debug.LogFormat("(Peek){0}", myQ.Peek());

        // 打印队列中的所有值   
        Debug.Log("Queue values:");
        PrintValues(myQ);


    }

    public static void PrintValues(IEnumerable myCollection)
    {
        foreach (var obj in myCollection)
            Debug.Log( obj);
    }
}
