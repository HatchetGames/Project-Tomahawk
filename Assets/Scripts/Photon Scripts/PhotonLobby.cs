using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PhotonLobby : MonoBehaviourPunCallbacks {

    public static PhotonLobby lobby;

    private void Awake()
    {
        //if (lobby == null)
        //    lobby = this;
        //else if (lobby != this)
        //    Destroy(gameObject);
        //DontDestroyOnLoad(gameObject);
        lobby = this;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        //Instantiate(localPlayer, Vector3.zero, Quaternion.identity);
    }

    public override void OnConnectedToMaster()
    {
        //PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("We are connected to master");

        PhotonNetwork.JoinRandomRoom();
    }

    private void CreateRoom()
    {
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = (byte)MultiplayerSettings.settings.maxPlayers
        };

        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOptions);
        //PhotonNetwork.CreateRoom("test", roomOptions);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("Tried to create a new room but failed, there must already be a room with the same name");
        CreateRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("Failed to join a random game. There must be no open games");
        // Make new room
        CreateRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("Failed to join game. The game must not have been created");

        CreateRoom();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("On Joined Lobby");
        //PhotonNetwork.JoinRandomRoom();
    }

    //public override void OnDisconnected(DisconnectCause cause)
    //{
    //    if(sectionView1.activeInHierarchy)
    //        sectionView1.SetActive(false);
    //    if (sectionView2.activeInHierarchy)
    //        sectionView2.SetActive(false);

    //    sectionView3.SetActive(true);

    //    Debug.Log("Disconnected from Photon");
    //}
}
