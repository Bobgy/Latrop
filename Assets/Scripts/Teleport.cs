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

	private Matrix4x4 reflection  = Matrix4x4.zero;
	private Quaternion rotate;
	private Quaternion AxeRotation;
	private Quaternion ObjectRotation;
	private Plane PortalPlane;
	private Matrix4x4 tmpVelocityRotationMatrix;
	private float SpeedStabilisation = 0.98F;
	
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

		InitReflectionMatrixAndOtherStuff ();
		// change position
		other.transform.position = otherPortal.transform.position;// + otherPortal.transform.forward * 1;
		//other.transform.position = reflection.MultiplyPoint3x4 (other.transform.position);
		other.transform.rotation = ObjectRotation * other.transform.rotation;

		// change velocity
		if (other.GetComponent<Rigidbody> () != null) {
            Debug.Log("entering");
            Debug.Log(other.GetComponent<Rigidbody>().velocity);
			//Vector3 velocity = other.GetComponent<Rigidbody> ().velocity;
			//velocity = Vector3.Reflect (velocity, hitNormal);
			Vector3 velocity = tmpVelocityRotationMatrix.MultiplyPoint(other.GetComponent<Rigidbody>().velocity * SpeedStabilisation);
			Vector3 angularVelocity = tmpVelocityRotationMatrix.MultiplyPoint(other.GetComponent<Rigidbody>().angularVelocity);

			Debug.Log(velocity);
			/*Vector3 tmp = transform.position + velocity;
			tmp = transform.worldToLocalMatrix.MultiplyPoint (tmp);

			tmp = otherPortal.transform.localToWorldMatrix.MultiplyPoint (tmp);
			velocity = tmp - otherPortal.transform.position;
			velocity = velocity.normalized * other.GetComponent<Rigidbody> ().velocity.magnitude;
			other.GetComponent<Rigidbody> ().velocity = velocity;
		
			other.transform.rotation = Quaternion.LookRotation (tmp - otherPortal.transform.position);*/
			other.GetComponent<Rigidbody>().velocity = velocity;
			other.GetComponent<Rigidbody>().angularVelocity = angularVelocity;
		}
	}

	void InitReflectionMatrixAndOtherStuff() {	
		//calculate mirror reflection matrix
		Matrix4x4 reflection1 = CalculateReflectionMatrix(new Vector4 (transform.up.x,  transform.up.y,  transform.up.z,  0));
		Matrix4x4 reflection2 = CalculateReflectionMatrix(new Vector4 (transform.right.x,  transform.right.y,  transform.right.z,  0));
		//Calculate rotation
		rotate 			= (otherPortal.rotation) * Quaternion.Inverse(transform.rotation);
		AxeRotation 	= Quaternion.AngleAxis(180, transform.forward);
		ObjectRotation	= rotate * AxeRotation;
		//Calculate final reflection
		//Step1 Move to BEGIN OF COORDINATES  		
		reflection  = Matrix4x4.TRS((otherPortal.position + otherPortal.up*0.01F), Quaternion.identity, new Vector3(1,1,1));			
		//Step2 Rotate Object on Difference Quaternion between 2 portals
		reflection *= Matrix4x4.TRS(new Vector3(0,0,0), rotate, new Vector3(1,1,1));
		//Step3 reflect from Up and Right vectors	
		reflection *= reflection1 * reflection2;
		//Step4 Move to Other portal position 
		reflection *= Matrix4x4.TRS(-(transform.position + transform.up*0.01F), Quaternion.identity, new Vector3(1,1,1));
		//Init other variables
		PortalPlane = new Plane(transform.up, transform.position + transform.up*0.008F);
		tmpVelocityRotationMatrix = Matrix4x4.TRS(new Vector3(0,0,0), ObjectRotation, new Vector3(1,1,1));	
	}

	Matrix4x4 CalculateReflectionMatrix (Vector4 plane) {
		Matrix4x4 reflectionMat = new Matrix4x4();
		
		reflectionMat.m00 = (1F - 2F*plane[0]*plane[0]);
		reflectionMat.m01 = (    -2F*plane[0]*plane[1]);
		reflectionMat.m02 = (    -2F*plane[0]*plane[2]);
		reflectionMat.m03 = (    -2F*plane[3]*plane[0]);
		
		reflectionMat.m10 = (    -2F*plane[1]*plane[0]);
		reflectionMat.m11 = (1F - 2F*plane[1]*plane[1]);
		reflectionMat.m12 = (    -2F*plane[1]*plane[2]);
		reflectionMat.m13 = (    -2F*plane[3]*plane[1]);
		
		reflectionMat.m20 = (    -2F*plane[2]*plane[0]);
		reflectionMat.m21 = (    -2F*plane[2]*plane[1]);
		reflectionMat.m22 = (1F - 2F*plane[2]*plane[2]);
		reflectionMat.m23 = (    -2F*plane[3]*plane[2]);
		
		reflectionMat.m30 = 0F;
		reflectionMat.m31 = 0F;
		reflectionMat.m32 = 0F;
		reflectionMat.m33 = 1F;
		
		return reflectionMat;
	}
}