using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerSettings : MonoBehaviour {

    public static MultiplayerSettings settings;

    public int maxPlayers;
    public string menuScene;
    public string matchScene;
    public string endgameScene;

    private void Awake()
    {
        if(MultiplayerSettings.settings == null)
        {
            MultiplayerSettings.settings = this;
        }
        else
        {
            if(MultiplayerSettings.settings != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
