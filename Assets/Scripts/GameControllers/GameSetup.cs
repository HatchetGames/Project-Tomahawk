using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetup : MonoBehaviourPunCallbacks {

    public static GameSetup GS;

    public Transform[] spawnPoints;

    public int maxRounds = 3;
    public int currentRound;

    public float roundTimer;

    public GameObject endGameScreen;
    public Text winnerOrLoserText;

    private void Start()
    {
        endGameScreen.SetActive(false);
    }

    private new void OnEnable()
    {
        if(GS == null)
        {
            GS = this;
        }
    }

    public void RestartRound()
    {
        //Respawn players, restart timer
    }

    public void EndMatch()
    {
        //Take player(s) to post-match screen
        endGameScreen.SetActive(true);
    }

    public void OnReturnButtonClicked()
    {
        DisconnectPlayer();
    }

    public void DisconnectPlayer()
    {
        StartCoroutine(DisconnectAndLoad());
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom)
        {
            yield return null;
        }
        if (PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();
        SceneManager.LoadScene(MultiplayerSettings.settings.homeScene);
    }
}
