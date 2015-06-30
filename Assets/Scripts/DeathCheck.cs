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
	int windowWidth = 400;
	int windowHight = 150;
	Rect windowRect ;
	int windowSwitch = 0;

	bool DeathFlag = false;

	void Finish() {
        DeathFlag = false;
        Time.timeScale = 1;
        Application.LoadLevel("MainMenu");
	}

	void Next() {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
        DeathFlag = false;
        Time.timeScale = 1;
        Application.LoadLevel("demo2");
	}

	//DeachCheck
	void OnCollisionEnter(Collision other)
	{
        Debug.Log(other.gameObject.tag);
		DeathFlag = (other.gameObject.tag == "Poison");
	}


	void Update ()
	{
		if (DeathFlag) {
			windowSwitch = 1;
            Time.timeScale = 0;
    		Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
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
	
	void OnGUI ()
	{ 
		if (windowSwitch == 1) {
			GUI.color = new Color (1, 1, 1, 1);
			windowRect = GUI.Window (0, windowRect, QuitWindow, "Quit Window");
		}
	}
	
	void QuitWindow (int windowID)
	{
		GUI.Label (new Rect (100, 50, 300, 30), "You are Dead!");
		
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
