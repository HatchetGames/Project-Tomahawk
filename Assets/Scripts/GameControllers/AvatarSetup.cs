using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AvatarSetup : MonoBehaviourPun {

    public int weaponValue;
    public int powerupValue;
    private GameObject myWeapon;
    private GameObject myPowerup;
    public GameObject weaponPos;
    public string weaponPrefabFolder;
    public string weaponPrefab;

    private int playerHealth = 100;
    private int maxHealth = 100;

    private Image HPBar;

	// Use this for initialization
	void Start () {
        if(photonView.IsMine)
        {
            myWeapon = PhotonNetwork.Instantiate("Weapons/Pistol", weaponPos.transform.position, weaponPos.transform.rotation, 0);
            myWeapon.transform.parent = weaponPos.transform;
            GameObject temp = GameObject.FindGameObjectWithTag("HP");
            HPBar = temp.GetComponent<Image>();
            //photonView.RPC("RPC_AddWeapon", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedWeapon);
            //photonView.RPC("RPC_AddPowerup", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedPowerup);
        }
        else
        {

        }
	}

    public int GetHealth()
    {
        return playerHealth;
    }
    public void SetHealth(int health)
    {
        playerHealth = health;
    }

    public void ApplyDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth < 0)
            playerHealth = 0;

        if(photonView.IsMine)
        {
            RefreshHPBar(((float)playerHealth / (float)maxHealth));
        }

        if(playerHealth <= 0)
        {

        }
        else
        {

        }
    }

    void RefreshHPBar(float percentLeft)
    {
        if (percentLeft >= 0.7f)
            HPBar.color = Color.green;
        else if (percentLeft >= 0.3f)
            HPBar.color = Color.yellow;
        else
            HPBar.color = Color.red;

        HPBar.fillAmount = percentLeft;
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
