using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonConnect : MonoBehaviourPunCallbacks
{
    
    // Start is called before the first frame update
    void Start()
    {
        Connect();
    }

    void Connect() 
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = "v1.0";
        }
    }

    public override void OnConnectedToMaster()
    {
        print("connected to master");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("cant join room!");

        PhotonNetwork.CreateRoom(null);
    }
    public override void OnJoinedRoom()
    {
        print("joined room!");

        //spawn ship
        GameObject ship = PhotonNetwork.Instantiate("Ship", Vector3.up*15+10*Vector3.forward*(PhotonNetwork.CurrentRoom.PlayerCount), Quaternion.identity, 0);
        
    }
}
