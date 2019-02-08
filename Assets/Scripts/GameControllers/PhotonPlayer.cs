﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour {

    private PhotonView PV;
    public GameObject playerAvatar;

	// Use this for initialization
	void Start () {
        PV = GetComponent<PhotonView>();

        if(PV.IsMine)
        {
            playerAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"), 
                GameSetup.GS.spawnPoints[PhotonRoom.room.myNumberInRoom].position, 
                GameSetup.GS.spawnPoints[PhotonRoom.room.myNumberInRoom].rotation, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
