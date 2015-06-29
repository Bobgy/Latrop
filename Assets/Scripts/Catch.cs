using UnityEngine;
using System.Collections;

//Add rigidbody to the catchobject object
//mass = 1
//drag = 5
//Angular Dray = 5
public class Catch : MonoBehaviour {
	public float Xspeed = 1.0f;
	public float Yspeed = 1.0f;
	public float Fspeed = 1.0f;
	public float CatchDistance = 2.0f;

	GameObject obj = null;
	bool flag = false;
	Vector3 lastPos;
	Vector3 centerPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
	void Start () {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	//Condition of Catching
	bool IsCatchable(GameObject obj) {
		//use tag, name "catchflag"
		return obj.gameObject.tag == "CatchFlag";
	}

	// Update is called once per frame
	void Update () {
		//Set Key F to select obj;
		if (Input.GetKeyDown (KeyCode.F)) {
			if (flag == false) {
				Ray ray = Camera.main.ScreenPointToRay(centerPos);
				RaycastHit hit;
				GameObject hitObj = null;
				if (Physics.Raycast(ray, out hit, CatchDistance)) {
					hitObj = hit.collider.gameObject;
					obj = hitObj.transform.gameObject;
					lastPos = this.transform.position;
					if (obj != null && IsCatchable(obj)) {
						Rigidbody rb = obj.gameObject.GetComponent<Rigidbody>();
						rb.useGravity = false;
						flag = true; 	//lock, if catch
					}
				}
			}
			else {
				Rigidbody rb = obj.gameObject.GetComponent<Rigidbody>();
				rb.useGravity = true;
				obj = null;				//unlock
				flag = false;
			}
		}
		//move the the cursor
		if (flag == true) {
			Vector3 NextPos = obj.transform.position;
			Vector3 offset = this.transform.position - lastPos;
			offset.y = 0.0f;
			NextPos += offset * Fspeed;
			float dy = Input.GetAxis("Mouse Y") * Yspeed;
			float dx = Input.GetAxis("Mouse X") * Xspeed;
			if (dy > 0.2f) {
				dy = 0.2f;
			}
			if (dy < -0.2f) {
				dy = -0.2f;
			}
			NextPos += new Vector3(0, dy, 0);
			obj.transform.position = NextPos;
			//!
			obj.transform.LookAt(this.transform.position);
			obj.transform.Translate(new Vector3(-dx, 0, 0));
			//!
			lastPos = this.transform.position;
		}
	}

	void OnGUI () {
		GUI.Label (new Rect (Screen.width / 2, Screen.height / 2, 30, 30), "+");
	}
}
