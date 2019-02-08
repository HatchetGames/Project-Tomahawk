using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSetup : MonoBehaviour {

    public static GameSetup GS;

    public Transform[] spawnPoints;

    private void OnEnable()
    {
        if(GS == null)
        {
            GS = this;
        }
    }

    public void DisconnectPlayer()
    {
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        //PhotonNetwork.Disconnect();
        PhotonNetwork.LeaveRoom();
        while(PhotonNetwork.InRoom)
        {
            yield return null;
        }

        SceneManager.LoadScene(MultiplayerSettings.settings.menuScene);
    }
}
