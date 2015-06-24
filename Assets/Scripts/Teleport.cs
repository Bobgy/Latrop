/*
  把脚本添加到两个门上, 对应的otherPortal设置一下
*/
using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {
	
	public Transform otherPortal;
    public AudioSource teleportSound;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log ("something hit the portal");

		Quaternion q2 = Quaternion.FromToRotation(-transform.up, otherPortal.up);

        teleportSound.Play();

		// change velocity
		if (other.GetComponent<Rigidbody>() != null) {
			other.GetComponent<Rigidbody>().velocity = otherPortal.transform.forward * other.GetComponent<Rigidbody>().velocity.magnitude;
			Debug.Log (otherPortal.transform.forward);
		}
		// change position
		other.transform.position = otherPortal.transform.position + otherPortal.transform.forward * 1;

		// change rotation
		other.transform.rotation = otherPortal.transform.rotation;
	}
}