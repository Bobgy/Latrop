/*
  把脚本添加到两个门上, 对应的otherPortal设置一下
*/
using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

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

        if (teleportSound != null) teleportSound.Play();

		otherPortal.GetComponent<Teleport> ().disableTimer = 1;
        
        FPCtrl ctrl = other.GetComponent<FPCtrl>();
        RigidbodyFirstPersonController ctrl2 = other.GetComponent<RigidbodyFirstPersonController>();
        if (ctrl != null) ctrl.setJumping();
        if (ctrl2 != null) ctrl2.setJumping();

		// change position
		other.transform.position = otherPortal.transform.position;// + otherPortal.transform.forward * 1;

		// change velocity
		if (other.GetComponent<Rigidbody> () != null) {
			Vector3 velocity = other.GetComponent<Rigidbody> ().velocity;
			velocity = Vector3.Reflect (velocity, hitNormal);

			Vector3 tmp = transform.position + velocity;
			tmp = transform.worldToLocalMatrix.MultiplyPoint (tmp);

			tmp = otherPortal.transform.localToWorldMatrix.MultiplyPoint (tmp);
			velocity = tmp - otherPortal.transform.position;
			velocity = velocity.normalized * other.GetComponent<Rigidbody> ().velocity.magnitude;
			other.GetComponent<Rigidbody> ().velocity = velocity;
		
			other.transform.rotation = Quaternion.LookRotation (tmp - otherPortal.transform.position);
		}
		else {
			other.transform.rotation = otherPortal.transform.rotation;
		}
	}
}