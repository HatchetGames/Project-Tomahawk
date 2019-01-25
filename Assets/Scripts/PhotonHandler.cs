using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonHandler : MonoBehaviourPunCallbacks
{
    public static PhotonHandler pHandler;
    public PlayerMovement player;
    public PhotonButtons photonB;

    private void Awake()
    {
        if (PhotonHandler.pHandler == null)
            PhotonHandler.pHandler = this;
        else if(PhotonHandler.pHandler != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Match")
        {
            SpawnPlayer();
        }
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
        photonB = null;
        PhotonNetwork.LoadLevel("Match");
    }

    public override void OnJoinedRoom()
    {
        MoveScene();
        Debug.Log("We are connected to the room!");
    }

    private void SpawnPlayer()
    {
        PhotonNetwork.Instantiate(player.name, player.transform.position, player.transform.rotation, 0);
    }
}
