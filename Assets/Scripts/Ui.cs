using UnityEngine;
using System.Collections;

public class Ui : MonoBehaviour {

	public Texture2D allon;
	public Texture2D alloff;
	public Texture2D redon;
	public Texture2D blueon;
    public bool red = true;
    public bool blue = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
        /*
		// Make a background box
		GUI.Box(new Rect(10,10,100,90), "Loader Menu");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,80,20), "Level 1")) {
			Application.LoadLevel(1);
		}
		
		// Make the second button.
		if(GUI.Button(new Rect(20,70,80,20), "Level 2")) {
			Application.LoadLevel(2);
		}
        */
	    Texture2D texture = null;
        if (red && blue)
            texture = allon;
        if (!red && !blue)
            texture = alloff;
        if (!red && blue)
            texture = blueon;
        if (red && !blue)
            texture = redon;

		int width = Screen.width / 15, height = texture.height/texture.width*width;
		Rect  rect = new Rect( (Screen.width >> 1) - (width >> 1),(Screen.height >> 1) - (height >> 1), width, height);
		
		GUI.DrawTexture(rect, texture);
	}
}
