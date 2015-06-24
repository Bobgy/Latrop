/*
  把脚本添加到两个门上, 对应的otherPortal设置一下
*/
using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {
	
	public Transform otherPortal;
	public float TeleportOffset;
	
	// Use this for initialization
	void Start () {
		TeleportOffset = 2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log ("something hit the portal");

		// change Rigidbody
		if (other.GetComponent<Rigidbody> () != null) {
			Debug.Log(other.transform.forward);
			Vector3 relPoint = transform.InverseTransformPoint(other.transform.position);
			Vector3 relVelocity = -transform.InverseTransformDirection (other.GetComponent<Rigidbody> ().velocity);
			other.GetComponent<Rigidbody> ().velocity = otherPortal.transform.TransformDirection (relVelocity);
			other.transform.position = otherPortal.transform.TransformPoint (relPoint) + (other.GetComponent<Rigidbody> ().velocity.normalized * TeleportOffset)
				+ otherPortal.transform.forward * 1;
			Debug.Log(other.transform.forward);
		} else {
			other.transform.position = otherPortal.transform.position + otherPortal.transform.forward * TeleportOffset;
			other.transform.rotation = otherPortal.transform.rotation * Quaternion.Inverse(otherPortal.transform.rotation * Quaternion.Euler(0,0,180)) * other.transform.rotation;
		}
	}
}