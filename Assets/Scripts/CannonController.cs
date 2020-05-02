using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    //Cannon Rotation Variables
    public int speed;
    public float friction;
    public float lerpSpeed;
    float xDegrees;
    float yDegrees;
    Quaternion fromRotation;
    Quaternion toRotation;
    Camera camera;

    //Cannon Firing Variables
    public GameObject cannonBall;
    Rigidbody cannonballRB;
    public Transform shotPost;
    public GameObject explosion;
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            //if(hit.transform.gameObject.tag == "cannon")
            {
                if(Input.GetMouseButton(0))
                {
                    Debug.Log("lalala");
                    xDegrees -= Input.GetAxis("Mouse Y") * speed * friction;
                    yDegrees += Input.GetAxis("Mouse X") * speed * friction;
                    fromRotation = transform.localRotation;
                    toRotation = Quaternion.Euler(xDegrees, yDegrees, 0);
                    transform.localRotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);
                }
            }
        }
    }

    public void FireCannon()
    {

    }
}
