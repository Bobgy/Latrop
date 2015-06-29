using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour {
	public float sec = 110;
	public int run = 1;
	GUIStyle myStyle;
	// Use this for initialization
	void Start () {
		run = 1;
		myStyle = new GUIStyle();
		Font myFont = (Font)Resources.Load("VintageOne", typeof(Font));
		myStyle.font = myFont;
	}
	
	//Do after Count Down
	void Finish() {
		//!
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (run == 1) {
			sec -= Time.deltaTime;
		}
		if (sec < 0) {
			run = 0;
			Finish ();
		}
	}

	void OnGUI() {
		if (sec < 20) {
			float alpha = 0.6f + (20 - sec) * 0.02f;
			myStyle.normal.textColor = new Color(1.0f, 0.0f, 0.3f, alpha);
		} 
		else {
			myStyle.normal.textColor = Color.black;
		}

		myStyle.fontSize = 50;

		if (sec > 10) {
			GUI.Label (new Rect (Screen.width / 2 - 100, 50, 100, 100), "Time: " + (int)sec, myStyle);
		} 
		else {
			GUI.Label (new Rect (Screen.width / 2 - 100, 50, 100, 100), "Time:   " + (int)sec, myStyle);
		}
	}
}
