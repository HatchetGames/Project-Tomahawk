using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonButtons : MonoBehaviourPunCallbacks {

    public PhotonHandler pHandler;
    public InputField createRoomInput, joinRoomInput;

    public void OnClickCreateRoom()
    {
        pHandler.OnClickCreateRoom();
    }

    public void OnClickJoinRoom()
    {
        pHandler.OnClickJoinRoom();
    }

    public override void OnJoinedRoom()
    {
        pHandler.MoveScene();
        Debug.Log("We are connected to the room!");
    }
}
