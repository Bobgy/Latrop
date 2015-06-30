using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour {

    public GameObject light;
    public GameObject camera;
    public float step = 0.005f;
	 
    bool win = false;
	// Use this for initialization
	void Start () {
		RenderSettings.ambientIntensity = 1;
		light.GetComponent<Light>().intensity = 1;
        //win = true;
	}

	void OnTriggerEnter(Collider other) {
        Debug.Log("hit win");
        if (other.gameObject.tag == "Player")
        {
            RenderSettings.skybox = null;
            camera.GetComponent<Camera>().backgroundColor = Color.black;
            win = true;
            Debug.Log("player hit");
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(win)
        {
            RenderSettings.ambientIntensity -= step;
            light.GetComponent<Light>().intensity -= step;
            if (RenderSettings.ambientIntensity < 0.01f)
                Application.LoadLevel("about");
        }
	}
}
