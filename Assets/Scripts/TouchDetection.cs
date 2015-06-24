using UnityEngine;
using System.Collections;

public class TouchDetection : MonoBehaviour {

    public GameObject door;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        DoorControl dc = door.GetComponent<DoorControl>();
        dc.Open();
    }

    void OnTriggerExit(Collider other) {
        DoorControl dc = door.GetComponent<DoorControl>();
        dc.Close();
    }
}
