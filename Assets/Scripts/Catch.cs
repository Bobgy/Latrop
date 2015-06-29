using UnityEngine;
using System.Collections;

//Add rigidbody to the catchobject object
//mass = 1
//drag = 100
//Angular Dray = 5
public class Catch : MonoBehaviour {
	public float Xspeed = 1.0f;
	public float Yspeed = 1.0f;

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
		return obj.gameObject.tag == "catchflag";
	}

	// Update is called once per frame
	void Update () {
		//Set Key F to select obj;
		if (Input.GetKeyDown (KeyCode.F)) {
			if (flag == false) {
				Ray ray = Camera.main.ScreenPointToRay(centerPos);
				RaycastHit hit;
				GameObject hitObj = null;
				if (Physics.Raycast(ray, out hit, 100f)) {
					hitObj = hit.collider.gameObject;
					obj = hitObj.transform.gameObject;
					lastPos = this.transform.position;
					if (obj != null && IsCatchable(obj)) {
						flag = true; 	//lock, if catch
					}
				}
			}
			else {
				obj = null;				//unlock
				flag = false;
			}
		}
		//move the the cursor
		if (flag == true) {
			Vector3 NextPos = obj.transform.position;
			Vector3 offset = this.transform.position - lastPos;
			offset.y = 0.0f;
			NextPos += offset * Xspeed;
			float dy = Input.GetAxis("Mouse Y") * Yspeed;
			if (dy > 0.2f) {
				dy = 0.2f;
			}
			if (dy < -0.2f) {
				dy = -0.2f;
			}
			NextPos += new Vector3(0, dy, 0);
			obj.transform.position = NextPos;
			lastPos = this.transform.position;
		}
	}

	void OnGUI () {
		//GUI.Label (new Rect (Screen.width / 2, Screen.height / 2, 30, 30), "+");
	}
}
