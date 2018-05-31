using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadTxt : MonoBehaviour {




	void Start () {
        var fileAddress = System.IO.Path.Combine(Application.streamingAssetsPath, "Demo 1.txt");

        FileInfo fInfo0 = new FileInfo(fileAddress);

        if (fInfo0.Exists)
        {
            StreamReader r = new StreamReader(fileAddress);
            string s = r.ReadToEnd();
            //Debug.Log(s);
            while (true)
            {
                var str = r.ReadLine();
                if (string.IsNullOrEmpty(str))
                {
                    break;
                }
                Debug.Log(str);
            }
    }
    }

}
