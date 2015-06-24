using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public GameObject blueBullet;
    public GameObject redBullet;
    public Transform bulletSpawn;
    public AudioSource shootSound;
     
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0)){
            GameObject projectile = (GameObject)Instantiate(blueBullet, bulletSpawn.transform.position, bulletSpawn.rotation);
            projectile.SetActive(true);
            shootSound.Play();
        }
        if (Input.GetMouseButtonUp(1)){
            GameObject projectile = (GameObject)Instantiate(redBullet, bulletSpawn.transform.position, bulletSpawn.rotation);
            projectile.SetActive(true);
            shootSound.Play();
        }
	}
}
