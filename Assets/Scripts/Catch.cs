using UnityEngine;
using System.Collections;

public class Catch : MonoBehaviour {
	bool flag = false;
	GameObject obj = null;
	Vector3 lastPos;

	// Use this for initialization

	void Start () {
		//Lock in the middle of the screen
	}
	
	// Update is called once per frame
	void Update () {
		//print (Input.mousePosition);
		//Set Key F to select obj;
		if (Input.GetKey (KeyCode.F)) {
			if (flag == false) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				GameObject hitObj = null;
				if (Physics.Raycast(ray, out hit, 100f)) {
					hitObj = hit.collider.gameObject;
					print(hitObj.name);
					obj = hitObj.transform.gameObject;
					lastPos = this.transform.position;
				}
				//lock, only Sphere can be caught
				if (obj != null && obj.name.Equals("Sphere")) flag = true; ///!
			}
			else {
				//unlock
				obj = null;
				flag = false;
			}
		}
		//move the the cursor
		if (flag == true) {
			Vector3 offset = this.transform.position - lastPos;
			obj.transform.position += offset;

			Vector3 screenPos = Camera.main.WorldToScreenPoint(obj.transform.position);
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = screenPos.z;
			Vector3 worldPos;
			worldPos.x = Camera.main.ScreenToWorldPoint(mousePos).x;
			worldPos.z = Camera.main.ScreenToWorldPoint(mousePos).z;
			worldPos.y = obj.transform.position.y;
			obj.transform.LookAt(worldPos);
			obj.transform.Translate(Vector3.forward * Time.deltaTime);

			lastPos = this.transform.position;
		}
	}
}
