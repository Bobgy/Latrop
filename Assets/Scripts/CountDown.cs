using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour {
	public float sec = 23;
	public int run = 1;

	GUIStyle myStyle;

	int windowWidth = 400;
	int windowHight = 150;
	Rect windowRect ;
	int windowSwitch = 0;

	bool TimeFlag = false;

	// Use this for initialization
	void Start () {
		run = 1;
		myStyle = new GUIStyle();
		Font myFont = (Font)Resources.Load("VintageOne", typeof(Font));
		myStyle.font = myFont;
	}
	
	//Do after Count Down
	void TimeOut()
	{
		TimeFlag = true;
	}

	void Finish() 
	{
		TimeFlag = false;
		Time.timeScale = 1;
		Application.LoadLevel("MainMenu");
	}

	void Next() 
	{
		TimeFlag = false;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		Time.timeScale = 1;
		Application.LoadLevel("demo2");
	}

	void Update() {
		if (TimeFlag) {
			windowSwitch = 1;
			Time.timeScale = 0;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (run == 1) {
			sec -= Time.deltaTime;
		}
		if (sec < 0) {
			run = 0;
			TimeOut ();
		}
	}

	void Awake ()
	{
		windowRect = new Rect (
			(Screen.width - windowWidth) / 2,
			(Screen.height - windowHight) / 2,
			windowWidth,
			windowHight);
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

		//!
		if (windowSwitch == 1) {
			GUI.color = new Color (1, 1, 1, 1);
			windowRect = GUI.Window (0, windowRect, QuitWindow, "Quit Window");
		}
	}
	
	void QuitWindow (int windowID)
	{
		GUI.Label (new Rect (100, 50, 300, 30), "Time is out!");
		
		if (GUI.Button (new Rect (80, 110, 100, 20), "Back to menu")) {
			Finish();
		} 
		if (GUI.Button (new Rect (220, 110, 100, 20), "Restart")) {
			Next();
			windowSwitch = 0; 
		} 
		
		GUI.DragWindow (); 
	}	
}
