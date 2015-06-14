using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour {
	public double sec = 100;
	public int run = 1;
	// Use this for initialization
	void Start () {
		run = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (run == 1) {
			sec -= Time.deltaTime;
		}
		if (sec < 0) {
			run = 0;
			Finish ();
		}
	}

	//Do after Count Down
	void Finish() {

	}
	void OnGUI() {
		GUI.Label (new Rect (Screen.width / 2, 50, 100, 100), "Time: " + (int)sec);
	}
}
