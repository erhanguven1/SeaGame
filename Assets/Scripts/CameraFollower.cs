using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Ship ship;
    public Transform lookat;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponentInParent<Photon.Pun.PhotonView>().IsMine)
        {
            Destroy(gameObject);
        }
        else
            transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, ship.transform.position - (transform.forward) * offset.z - transform.right * offset.x, Time.deltaTime);
        transform.position = transform.position + (offset.y - transform.position.y) * Vector3.up;

        //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(21,ship.transform.eulerAngles.y+90,0),.1f);
        transform.RotateAround(ship.transform.position,Vector3.up,Input.GetAxis("Mouse X"));
    }
}
