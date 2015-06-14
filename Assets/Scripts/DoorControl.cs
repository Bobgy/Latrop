using UnityEngine;
using System.Collections;

public class DoorControl : MonoBehaviour {

    public Vector3 targetPosition;

	// Use this for initialization
	void Start () {
        targetPosition = GetComponent<Transform>().position;
	}
	
	// Update is called once per frame
	void Update () {
        Transform tf = GetComponent<Transform>();
        tf.position = (tf.position * 99.0f + targetPosition) / 100.0f;
	}
}
