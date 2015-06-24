using UnityEngine;
using System.Collections;

public class DoorControl : MonoBehaviour
{

    public Vector3 openVector;
    public float openSpeed;
    public AudioSource soundButtonOn;

    protected Vector3 targetPosition;
    // openCount <= 0 : close
    // otherwise      : open
    protected int openCount = 0;

    // Use this for initialization
    void Start()
    {
        targetPosition = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        Transform tf = GetComponent<Transform>();
        float step = openSpeed * Time.deltaTime;
        tf.position = Vector3.MoveTowards(tf.position, targetPosition, step);
    }

    // Called to open the door
    public void Open()
    {
        openCount++;
        if (openCount == 1) // close -> open
        {
            soundButtonOn.Play();
            targetPosition += openVector;
        }
    }

    // Called to close the door
    public void Close()
    {
        openCount--;
        if (openCount == 0) // open -> close
        {
            targetPosition -= openVector;
        }
    }
}
