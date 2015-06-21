using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float speed = 0.5f;
    public float DestroyTime = 1.0f;
    private Vector3 velocity;
    public GameObject portal;
    public GameObject ui;
    public bool whichPortal = true;

	// Use this for initialization
	void Start () {
        velocity = -transform.forward * speed;
        Destroy (gameObject, DestroyTime);
	}
	
	// Update is called once per frame
	void Update () {
	}
    
    void FixedUpdate ()
    {
        transform.position += velocity * Time.deltaTime;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Portal")
            return;
        Ray ray = new Ray(transform.position, velocity);
        RaycastHit hit;
        if((collider.gameObject.tag == "Floor" || collider.gameObject.tag == "Portalable") && Physics.Raycast(ray, out hit))
        {
            Quaternion hitObjectRotation = Quaternion.LookRotation(hit.normal);
            portal.transform.position = transform.position;
            if (collider.gameObject.tag == "Floor")
                portal.transform.eulerAngles = new Vector3(hitObjectRotation.eulerAngles.x, transform.eulerAngles.y, hitObjectRotation.eulerAngles.z);
            else
                portal.transform.rotation = hitObjectRotation;
            portal.SetActive(true);
        }
        if (whichPortal)
            ui.GetComponent<Ui>().blue = false;
        else
            ui.GetComponent<Ui>().red = false;
        Destroy(gameObject);
    }
}
