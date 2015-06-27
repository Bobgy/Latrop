using UnityEngine;
using System.Collections;

public class Catch : MonoBehaviour {
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
        return obj.gameObject.GetComponent("CatchFlag") != null;
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
			Vector3 offset = this.transform.position - lastPos;
			offset.y = 0;
			obj.transform.position += offset;
			float dx = Input.GetAxis("Mouse X");
			float dy = Input.GetAxis("Mouse Y");
			obj.transform.position += new Vector3(0, dy, 0);
			obj.transform.LookAt(this.transform.position);
			obj.transform.Translate(Vector3.left * dx);
			lastPos = this.transform.position;
		}
	}

	void OnGUI () {
		GUI.Label (new Rect (Screen.width / 2, Screen.height / 2, 30, 30), "+");
	}
}
