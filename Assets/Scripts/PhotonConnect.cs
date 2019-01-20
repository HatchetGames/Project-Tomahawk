using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PhotonConnect : MonoBehaviourPunCallbacks {

    public GameObject sectionView1, sectionView2, sectionView3;
    public string gameVersion;

    private void Awake()
    {
        //PhotonNetwork.NickName = "Hatchet";
        //PhotonNetwork.GameVersion = gameVersion;

        PhotonNetwork.ConnectUsingSettings();

        Debug.Log("Connecting to Photon...");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("We are connected to master");
    }

    public override void OnJoinedLobby()
    {
        sectionView1.SetActive(false);
        sectionView2.SetActive(true);
        Debug.Log("On Joined Lobby");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if(sectionView1.activeInHierarchy)
            sectionView1.SetActive(false);
        if (sectionView2.activeInHierarchy)
            sectionView2.SetActive(false);

        sectionView3.SetActive(true);

        Debug.Log("Disconnected from Photon");
    }
}
