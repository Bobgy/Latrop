/*
在对应的scene里面设置好currentLevel和nextLevel
然后把脚本添加到player上
*/

using UnityEngine;
using System.Collections;

public class LevelMenuScript : MonoBehaviour {
	public string currentLevel;
	public string nextLevel;

	void OnGUI() {
		const int buttonWidth = 84;
		const int buttonHeight = 30;
		Rect RetryButton = new Rect(Screen.width - 270, 10, buttonWidth, buttonHeight);
		Rect NextButton = new Rect(Screen.width - 180, 10, buttonWidth, buttonHeight);
		Rect ReturnButton = new Rect(Screen.width - 90, 10, buttonWidth, buttonHeight);
		if (currentLevel != "" && GUI.Button(RetryButton, "RETRY")) {
			Application.LoadLevel(currentLevel);
		}
		if (nextLevel != "" && GUI.Button(NextButton, "NEXT")) {
			Application.LoadLevel(nextLevel);
		}
		if (GUI.Button(ReturnButton, "BACK")) {
			Application.LoadLevel("MainMenu");
		}
	}
}
