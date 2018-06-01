using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorLerpTest : MonoBehaviour {


    public Image img;

    public float t;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        img.color = Color.Lerp(Color.red, Color.yellow, t);
	}
}
