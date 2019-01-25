using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchController : MonoBehaviourPunCallbacks {

    [Header("Match Manager")]
    public PlayerMovement PlayerPrefab;

    [HideInInspector]
    public PlayerMovement localPlayer;

    private void Awake()
    {
        if(!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("Menu");
            return;
        }
    }

    private void Start()
    {
        //localPlayer = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity).GetComponent<PlayerMovement>();
        //if(PhotonNetwork.IsMasterClient)

        //PlayerMovement.RefreshInstance(ref localPlayer, PlayerPrefab);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        PlayerMovement.RefreshInstance(ref localPlayer, PlayerPrefab);
    }
}
