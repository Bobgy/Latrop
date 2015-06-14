/*
  把脚本添加到对应门的camera上, 设置相应的otherPortal
*/
using UnityEngine;
using System.Collections;

public class PortalCameraControl : MonoBehaviour {
	public GameObject otherPortal;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Quaternion rot = Quaternion.Inverse( otherPortal.transform.rotation ) * Camera.main.transform.rotation;
		rot = Quaternion.AngleAxis(180.0f, new Vector3(0,1,0)) * rot;
		transform.localRotation = rot;
	}
}
