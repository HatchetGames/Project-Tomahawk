﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    public static PlayerInfo PI;

    private string myNickname;
    public int mySelectedWeapon;
    public int mySelectedPowerup;
    public int mySelectedSkin;

    public GameObject[] allWeapons;
    public GameObject[] allPowerups;
    //public GameObject[] allSkins;

    private void OnEnable()
    {
        if (PI == null)
            PI = this;
        else
        {
            if (PI != this)
            {
                Destroy(PI.gameObject);
                PI = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.HasKey("MyNickname"))
            myNickname = PlayerPrefs.GetString("MyNickname");
        else
            PlayerPrefs.SetString("MyNickname", myNickname);

        if (PlayerPrefs.HasKey("MyWeapon"))
            mySelectedWeapon = PlayerPrefs.GetInt("MyWeapon");
        else
        {
            mySelectedWeapon = 0;
            PlayerPrefs.SetInt("MyWeapon", mySelectedWeapon);
        }

        if (PlayerPrefs.HasKey("MyPowerup"))
            mySelectedPowerup = PlayerPrefs.GetInt("MyPowerup");
        else
        {
            mySelectedPowerup = 0;
            PlayerPrefs.SetInt("MyPowerup", mySelectedPowerup);
        }
    }

    public void SetNickname(string nickname)
    {
        myNickname = nickname;
        PlayerPrefs.SetString("MyNickname", myNickname);
    }

    public string GetNickname()
    {
        return myNickname;
    }
}
