using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    public Transform shotPos;
    public GameObject explosion;
    public float firePower;

    private Ship ship;

    public GameController.FireType myType;
    void Start()
    {
        if (!GetComponentInParent<Photon.Pun.PhotonView>().IsMine)
        {
            this.enabled = false;
        }
        ship = GetComponentInParent<Ship>();
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {


        if (myType == ship.gameController.fireType)
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                //if(hit.transform.gameObject.tag == "cannon")
                {
                    if (Input.GetMouseButton(0))
                    {

                        // xDegrees -= Input.GetAxis("Mouse Y") * speed * friction;
                        yDegrees += Input.GetAxis("Mouse X") * speed * friction;
                        fromRotation = transform.localRotation;
                        toRotation = Quaternion.Euler(0, yDegrees, 0);
                        transform.localRotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);
                    }
                }
            }
            if (Input.GetMouseButtonDown(1) && ship.gameController.cooldownSlider.fillAmount>=1) //fire with selected FireType
            {
                    FireCannon(this.gameObject);

            }
        }


    }

    public void FireCannon(GameObject cannon)
    {
        if (myType== GameController.FireType.Side)
        {
            ship.gameController.firedSideCount++;
            if (ship.gameController.firedSideCount==3)
            {
                ship.gameController.firedSideCount = 0;
                ship.gameController.cooldownSlider.fillAmount = 0;
            }
        }
        else
        {
            ship.gameController.cooldownSlider.fillAmount = 0;
        }
        GameObject cannonBallCopy = Photon.Pun.PhotonNetwork.Instantiate("cannonBall", shotPos.position, transform.rotation) as GameObject;
        cannonballRB = cannonBallCopy.GetComponent<Rigidbody>();
        cannonballRB.AddForce(cannon.transform.forward * firePower);
        //Instantiate(explosion, shotPos.position, shotPos.rotation);
        //Photon.Pun.PhotonNetwork.InstantiateSceneObject(explosion, shotPos.position, shotPos.rotation);


    }
}
