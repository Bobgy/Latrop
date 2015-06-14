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
        Debug.Log("Trigger On");
        DoorControl dc = door.GetComponent<DoorControl>();
        dc.targetPosition += Vector3.up * 3.0f;
        /*
        Transform tf = door.GetComponent<Transform>();
        tf.position += Vector3.up * 2.0f;
        */
    }

    void OnTriggerExit(Collider other) {
        Debug.Log("Trigger Off");
        DoorControl dc = door.GetComponent<DoorControl>();
        dc.targetPosition += Vector3.down * 3.0f;
        /*
        Transform tf = door.GetComponent<Transform>();
        tf.position += Vector3.down * 2.0f;
        */
    }
}
