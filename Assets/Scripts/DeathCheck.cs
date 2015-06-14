using UnityEngine;
using System.Collections;

public class DeathCheck : MonoBehaviour
{
	int windowWidth = 400;
	int windowHight = 150;

	//Limit of checking death
	const double LIMIT = 2;	//!

	Rect windowRect ;
	int windowSwitch = 0;
	float alpha = 0;
	bool DeathFlag = false;
	
	void GUIAlphaColor_0_To_1 ()
	{
		if (alpha < 1) {
			alpha += Time.deltaTime;
			GUI.color = new Color (1, 1, 1, alpha);
		} 
	}
	
	// Init
	void Awake ()
	{
		windowRect = new Rect (
			(Screen.width - windowWidth) / 2,
			(Screen.height - windowHight) / 2,
			windowWidth,
			windowHight);
	}

	public string ori;
	public string dir;
	bool check () {
		//!		
		//Judge by the distance to the center of the object below.
		//and by the object name: poison pool
		Vector3 Spos = this.transform.position;
		Vector3 Vpos = new Vector3(0, -0.1f, 0);
		//Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		Ray ray = new Ray (Spos, Vpos);
		ori = ray.origin.ToString ();
		dir = ray.direction.ToString ();
			RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100f)) {
			double dis = hit.distance;
			GameObject obj = hit.collider.gameObject;
			//Obj contains a token of Poison
			if (obj.GetComponent<Poison>() && dis < LIMIT) {
				return true;
			}
		}
		return false;
	}
	
	void Update ()
	{
		//!
		//Need to lock the Screen  
		DeathFlag = check ();
		if (DeathFlag) {
			windowSwitch = 1;
			alpha = 0; // Init Window Alpha Color
		}
	}
	
	void OnGUI ()
	{ 
		//Show location
		//GUI.Label (new Rect (100, 50, 300, 300), "" + transform.position);

		if (windowSwitch == 1) {
			GUIAlphaColor_0_To_1 ();
			windowRect = GUI.Window (0, windowRect, QuitWindow, "Quit Window");
		}
	}
	
	void QuitWindow (int windowID)
	{
		GUI.Label (new Rect (100, 50, 300, 30), "You are Dead!");
		
		if (GUI.Button (new Rect (80, 110, 100, 20), "Quit")) {
			Application.Quit ();
		} 
		if (GUI.Button (new Rect (220, 110, 100, 20), "Continue")) {
			//!
			//Do something to continue;
			windowSwitch = 0; 
		} 
		
		GUI.DragWindow (); 
	}	
}
