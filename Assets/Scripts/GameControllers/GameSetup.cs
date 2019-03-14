﻿using Photon.Pun;
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

    public GameObject endGameScreen;
    public Text winnerOrLoserText;

    private Text timerText;
    public float roundDuration = 60;
    private float timeLeft = 0;

    private bool roundInProgress = false;

    private void Start()
    {
        endGameScreen.SetActive(false);
        timerText = GameObject.FindGameObjectWithTag("RoundTimer").GetComponent<Text>();
        timeLeft = roundDuration;
        roundInProgress = true;
    }

    private new void OnEnable()
    {
        if(GS == null)
        {
            GS = this;
        }
    }

    private void Update()
    {
        if(roundInProgress && timeLeft > 0.0f)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = ((int)timeLeft).ToString();
        }
        
        if(timeLeft <= 0.0f)
        {
            roundInProgress = false;
            timeLeft = roundDuration;
            RestartRound();
        }

        if (currentRound == maxRounds)
            EndMatch();
    }

    public void RestartRound()
    {
        //Respawn players, restart timer
        if (PhotonNetwork.IsMasterClient)
            transform.position = spawnPoints[0].position;
        else
            transform.position = spawnPoints[1].position;

        roundInProgress = true;
        timeLeft = roundDuration;
        ++currentRound;
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
