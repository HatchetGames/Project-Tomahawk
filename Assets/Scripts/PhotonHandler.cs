using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonHandler : MonoBehaviourPunCallbacks
{

    public PhotonButtons photonB;

    private void Awake()
    {
        DontDestroyOnLoad(this.transform);
    }

    public void OnClickCreateRoom()
    {
        PhotonNetwork.CreateRoom(photonB.createRoomInput.text, new RoomOptions() { MaxPlayers = 2 }, null);
    }

    public void OnClickJoinRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;

        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 2
        };
        PhotonNetwork.JoinOrCreateRoom(photonB.joinRoomInput.text, roomOptions, TypedLobby.Default);
    }

    public void MoveScene()
    {
        PhotonNetwork.LoadLevel("Match");
    }

    public override void OnJoinedRoom()
    {
        MoveScene();
        Debug.Log("We are connected to the room!");
    }
}
