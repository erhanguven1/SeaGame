using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class test : MonoBehaviourPunCallbacks, IPunObservable
{
    WaterWaves waterWaves;
    public Vector3 myNigga;
    int i = 4;
    // Start is called before the first frame update
    public override void OnJoinedRoom()
    {
        waterWaves = GetComponent<WaterWaves>();
        if (!PhotonNetwork.IsMasterClient)
        {
            waterWaves.isMaster = false;
        }
    }
    void Start()
    {
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            i = Random.Range(0, 100);
            Debug.Log(i.ToString());
        }
        if (waterWaves)
        {
            myNigga = waterWaves.vertices[4];

        }

    }
    private void OnGUI()
    {
        GUILayout.Width(500);
        GUI.color = Color.red;
        GUILayout.Label(i.ToString());
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            for (int i = 0; i < waterWaves.vertices.Length/2; i++)
            {
                stream.SendNext(waterWaves.vertices[i]);
            }

        }
        else
        {
            for (int i = 0; i < waterWaves.vertices.Length/2; i++)
            {
                waterWaves.vertices[i]=(Vector3)stream.ReceiveNext();
            }
        }
    }
}
