using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks {

    public static PhotonRoom room;
    private PhotonView PV;

    public bool isGameLoaded;
    public string currentScene;

    private Player[] photonPlayers;
    public int playersInRoom;
    public int myNumberInRoom;
    public int playerInGame;

    private bool readyToCount;
    private bool readyToStart;
    public float startingTime;
    private float lessThanMaxPlayers;
    private float atMaxPlayers;
    private float timeToStart;

    private void Awake()
    {
        if(PhotonRoom.room == null)
        {
            PhotonRoom.room = this;
        }
        else
        {
            if(PhotonRoom.room != this)
            {
                Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    // Use this for initialization
    void Start ()
    {
        PV = GetComponent<PhotonView>();
        readyToCount = false;
        readyToStart = false;
        lessThanMaxPlayers = startingTime;
        atMaxPlayers = 10;
        timeToStart = startingTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(playersInRoom != MultiplayerSettings.settings.maxPlayers)
        {
            RestartTimer();
        }

        if(!isGameLoaded)
        {
            if(readyToStart)
            {
                atMaxPlayers -= Time.deltaTime;
                lessThanMaxPlayers = atMaxPlayers;
                timeToStart = atMaxPlayers;
            }
            else if(readyToCount)
            {
                lessThanMaxPlayers -= Time.deltaTime;
                timeToStart = lessThanMaxPlayers;
            }
            Debug.Log("Display time to start to the players: " + timeToStart);

            if (timeToStart <= 0)
                StartGame();
        }
	}

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined " + PhotonNetwork.CurrentRoom.Name);
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myNumberInRoom = playersInRoom;
        PhotonNetwork.NickName = myNumberInRoom.ToString();

        //Display to screen if necessary
        Debug.Log("Displays players in room out of max players possible (" 
            + playersInRoom + ":" + MultiplayerSettings.settings.maxPlayers + ")");

        if(playersInRoom > 1)
        {
            readyToCount = true;
        }

        if(playersInRoom == MultiplayerSettings.settings.maxPlayers)
        {
            readyToStart = true;
            if (!PhotonNetwork.IsMasterClient)
                return;
            Debug.Log("Closing " + PhotonNetwork.CurrentRoom.Name + " from OnJoinedRoom...");
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("A new player has joined the room");
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom++;

        if (playersInRoom > 1)
        {
            readyToCount = true;
        }

        if (playersInRoom == MultiplayerSettings.settings.maxPlayers)
        {
            readyToStart = true;
            if (!PhotonNetwork.IsMasterClient)
                return;
            Debug.Log("Closing " + PhotonNetwork.CurrentRoom.Name + " from OnPlayerEnteredRoom ...");
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }

    private void StartGame()
    {
        isGameLoaded = true;
        if (!PhotonNetwork.IsMasterClient)
            return;
        Debug.Log("Closing " + PhotonNetwork.CurrentRoom.Name + " from StartGame ...");
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(MultiplayerSettings.settings.matchScene);
    }

    private void RestartTimer()
    {
        lessThanMaxPlayers = startingTime;
        timeToStart = startingTime;
        atMaxPlayers = 10;
        readyToCount = false;
        readyToStart = false;
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.name;
        
        if(currentScene == MultiplayerSettings.settings.matchScene)
        {
            isGameLoaded = true;

            PV.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        playerInGame++;
        if(playerInGame == PhotonNetwork.PlayerList.Length)
        {
            PV.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonTest"), transform.position, Quaternion.identity, 0);
    }
}
