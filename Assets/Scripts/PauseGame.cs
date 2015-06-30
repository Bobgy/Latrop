using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour
{
	int windowWidth = 400;
	int windowHight = 150;
	Rect windowRect ;
	int windowSwitch = 0;

	bool pause = false;

    void Continue()
    {
        pause = false;
        Time.timeScale = 1;
        windowSwitch = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

	void Finish() {
        pause = false;
        Time.timeScale = 1;
        Application.LoadLevel("MainMenu");
	}

	void Next() {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
        pause = false;
        Time.timeScale = 1;
        Application.LoadLevel("demo2");
	}

	void Update ()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause)
                Continue();
            else
                pause = true;
        }
		if (pause) {
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
		GUI.Label (new Rect (100, 50, 300, 30), "Game paused");
		
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
