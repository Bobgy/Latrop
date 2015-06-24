using UnityEngine;
using System.Collections;

public class CollsionSound : MonoBehaviour {

    public AudioSource collisionSound;

    // When relative speed's sqrMagnitude >= threshold, collision sound will play
    public double threshold;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("haha");
        float speed = collision.relativeVelocity.sqrMagnitude;
        Debug.Log(speed);
        if (speed >= threshold)
        {
            collisionSound.Play();
        }
    }

}
