using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SerializeShip : MonoBehaviourPun, IPunObservable
{
    public Ship ourShip;

    public Vector3[] angles;

    // Start is called before the first frame update
    void Start()
    {
        ourShip = GetComponent<Ship>();
        angles = new Vector3[ourShip.cannons.Count];
    }
    private void Awake()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            for (int i = 0; i < angles.Length; i++)
            {
                ourShip.cannons[i].transform.localEulerAngles = Vector3.Lerp(ourShip.cannons[i].transform.localEulerAngles, angles[i], Time.deltaTime);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            for (int i = 0; i < ourShip.cannons.Count; i++)
            {
                stream.SendNext(ourShip.cannons[i].transform.localEulerAngles);
            }
        }
        else
        {
            if (ourShip && angles.Length>0)
            {
                for (int i = 0; i < ourShip.cannons.Count; i++)
                {
                    angles[i] = (Vector3)stream.ReceiveNext();
                }
            }

        }
    }
}
