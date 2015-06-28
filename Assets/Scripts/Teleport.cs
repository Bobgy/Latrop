/*
  把脚本添加到两个门上, 对应的otherPortal设置一下
*/
using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {
	
	public Transform otherPortal;
    public AudioSource teleportSound;
	public Vector3 hitNormal;
	float disableTimer = 0;
	
	// Use this for initialization
	void Start () {
		disableTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (disableTimer > 0) {
			disableTimer -= Time.deltaTime;
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (disableTimer > 0) {
			return;
		}
		Debug.Log ("something hit the portal");

		Quaternion q2 = Quaternion.FromToRotation(-transform.up, otherPortal.up);

        if (teleportSound != null) teleportSound.Play();

		otherPortal.GetComponent<Teleport> ().disableTimer = 1;
		
		// change object velocity
		if (other.GetComponent<Rigidbody>() != null) {
			other.GetComponent<Rigidbody>().velocity = q2 * other.GetComponent<Rigidbody>().velocity;
			Debug.Log (otherPortal.transform.forward);
		}
		// change position
		other.transform.position = otherPortal.transform.position;// + otherPortal.transform.forward * 1;
		
		if (other.GetComponent<CharacterController> () != null) {
			Vector3 velocity = other.GetComponent<CharacterController> ().velocity;
			velocity = Vector3.Reflect (velocity, hitNormal);
			
			Vector3 tmp = transform.position + velocity;
			tmp = transform.worldToLocalMatrix.MultiplyPoint(tmp);
			
			tmp = otherPortal.transform.localToWorldMatrix.MultiplyPoint(tmp);
			
			other.transform.rotation = Quaternion.LookRotation(tmp - otherPortal.transform.position);
		}
		else if (other.GetComponent<Rigidbody>() == null) {
			// change rotation
			other.transform.rotation = otherPortal.transform.rotation;
		}
	}
}