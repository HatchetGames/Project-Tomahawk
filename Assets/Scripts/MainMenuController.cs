using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button quickMatchButton;
    public Text playerNickname;
    public int maxNameLength;

    private void Start()
    {
        //Instantiate(localPlayer, Vector3.zero, Quaternion.identity);
        playerNickname.text = PlayerInfo.PI.GetNickname();
        quickMatchButton.interactable = false;
    }

    //Changes the nickname of the player
    public void UpdateNicknameText(Text newName)
    {
        string temp = newName.text;

        if (temp.Length > 0)
        {
            if (temp.Length > maxNameLength)
                temp = temp.Substring(0, maxNameLength);
        }
        else
            temp = "Player";

        playerNickname.text = temp;
        PlayerInfo.PI.SetNickname(temp);
        if (!quickMatchButton.interactable)
            quickMatchButton.interactable = true;
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
