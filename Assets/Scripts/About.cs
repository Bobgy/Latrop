using UnityEngine;
using System.Collections;

public class About : MonoBehaviour {

    void Start()
    {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
    }

	void OnGUI() {
		const int buttonWidth = 84;
		const int buttonHeight = 30;
		Rect AboutButton = new Rect(30, Screen.height - 40, buttonWidth, buttonHeight);
		if (GUI.Button(AboutButton, "BACK")) {
			Application.LoadLevel("MainMenu");
		}
	}
}
