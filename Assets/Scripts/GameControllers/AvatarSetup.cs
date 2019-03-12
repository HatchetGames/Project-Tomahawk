using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSetup : MonoBehaviourPun {

    public int weaponValue;
    public int powerupValue;
    public GameObject myWeapon;
    public GameObject myPowerup;

    public int playerHealth;
    public int playerDamage;



	// Use this for initialization
	void Start () {
        if(photonView.IsMine)
        {
            //photonView.RPC("RPC_AddWeapon", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedWeapon);
            //photonView.RPC("RPC_AddPowerup", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedPowerup);
        }
        else
        {

        }
	}
	
	[PunRPC]
    void RPC_AddWeapon(int whichWeapon)
    {
        weaponValue = whichWeapon;
        //myWeapon = Instantiate(PlayerInfo.PI.allWeapons[whichWeapon], transform.position, transform.rotation, transform);
    }

    [PunRPC]
    void RPC_AddPowerup(int whichPowerup)
    {
        powerupValue = whichPowerup;
        //myPowerup = Instantiate(PlayerInfo.PI.allPowerups[whichPowerup], transform.position, transform.rotation, transform);
    }
}
