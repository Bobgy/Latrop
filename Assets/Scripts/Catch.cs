using UnityEngine;
using System.Collections;

//Add rigidbody to the catchobject object
//mass = 1
//drag = 5
//Angular Dray = 5
public class Catch : MonoBehaviour {
	public float CatchDistance = 2.0f;
	
	GameObject obj = null;
	bool flag = false;
	Vector3 centerPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
	
	float myDistance;
	
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
					myDistance = Vector3.Distance(this.transform.position, obj.transform.position);
					Debug.Log(myDistance);
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
	}
	
	void FixedUpdate() {
		//move the the cursor
		if (flag == true) {
			obj.transform.LookAt(this.transform.position);
			Ray ray = Camera.main.ScreenPointToRay(centerPos);
			Vector3 JumpPos = ray.origin + ray.direction * myDistance;
			Rigidbody rb = obj.gameObject.GetComponent<Rigidbody>();
			rb.velocity = (JumpPos - obj.transform.position) / Time.deltaTime;
		}
	}
	
	void OnGUI () {
		GUI.Label (new Rect (Screen.width / 2, Screen.height / 2, 30, 30), "+");
	}
}
