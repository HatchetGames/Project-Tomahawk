using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    //public PlayerMovement localPlayer;

    private void Start()
    {
        //Instantiate(localPlayer, Vector3.zero, Quaternion.identity);
    }

    public void OnClickStartQuickMatch()
    {
        //Destroy(localPlayer);
        SceneManager.LoadScene("WaitingRoom");
    }

    public void OnClickStartSurvival()
    {
        //Destroy(localPlayer);
        SceneManager.LoadScene("WaitingRoomSurvival");
    }

    public void OnClickOpenShop()
    {
        //Destroy(localPlayer);
        SceneManager.LoadScene("Shop");
    }

    public void OnClickOpenSettings()
    {
        //Destroy(localPlayer);
        SceneManager.LoadScene("Settings");
    }
}
