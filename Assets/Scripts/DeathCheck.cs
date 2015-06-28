using UnityEngine;
using System.Collections;

/*
 * script on person entity
 * need to finish Finish() && Next();
 * need to add box collider and rigidbody
 * need to add "poison" tag and box collider to poison pool
 */ 

public class DeathCheck : MonoBehaviour
{
	GameObject obj = null;

	int windowWidth = 400;
	int windowHight = 150;

	//Limit of checking death
	const double LIMIT = 2;	//!

	Rect windowRect ;
	int windowSwitch = 0;
	float alpha = 0;
	bool DeathFlag = false;

	void Finish() {
		//to be done
	}

	void Next() {
		//to be done
	}
	
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

	//DeachCheck
	void OnCollisionEnter(Collision other)
	{
		DeathFlag = (other.gameObject.tag == "poison");
	}


	void Update ()
	{
		if (DeathFlag) {
			windowSwitch = 1;
			alpha = 0; // Init Window Alpha Color
		}
	}
	
	void OnGUI ()
	{ 
		if (obj != null) GUI.Label (new Rect (100, 300, 300, 300), "name: " + obj);

		if (windowSwitch == 1) {
			GUIAlphaColor_0_To_1 ();
			windowRect = GUI.Window (0, windowRect, QuitWindow, "Quit Window");
		}
	}
	
	void QuitWindow (int windowID)
	{
		GUI.Label (new Rect (100, 50, 300, 30), "You are Dead!");
		
		if (GUI.Button (new Rect (80, 110, 100, 20), "Quit")) {
			Finish();
		} 
		if (GUI.Button (new Rect (220, 110, 100, 20), "Continue")) {
			Next();
			windowSwitch = 0; 
		} 
		
		GUI.DragWindow (); 
	}	
}
