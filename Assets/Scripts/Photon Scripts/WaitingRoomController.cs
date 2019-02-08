using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingRoomController : MonoBehaviour {

	public void OnClickWeaponPick(int whichWeapon)
    {
        if(PlayerInfo.PI !=null)
        {
            PlayerInfo.PI.mySelectedWeapon = whichWeapon;
            PlayerPrefs.SetInt("MyWeapon", whichWeapon);
        }
    }

    public void OnClickPowerupPick(int whichPowerup)
    {
        if(PlayerInfo.PI != null)
        {
            PlayerInfo.PI.mySelectedPowerup = whichPowerup;
            PlayerPrefs.SetInt("MyPowerup", whichPowerup);
        }
    }
}
