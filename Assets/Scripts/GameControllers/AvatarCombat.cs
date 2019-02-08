using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class AvatarCombat : MonoBehaviourPun
{

    private AvatarSetup avatarSetup;
    private Text health;

    // Use this for initialization
    void Start()
    {
        avatarSetup = GetComponent<AvatarSetup>();
        health = GameObject.Find("healthText").GetComponent<Text>();

        if(health == null)
        {
            Debug.LogError("Cant find heath object");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;

        Vector2 direction;

        direction = new Vector2(
            CrossPlatformInputManager.GetAxis("horizontal"),
            CrossPlatformInputManager.GetAxis("vertical")
            );

        if(direction.magnitude > 1)
        {
            direction.Normalize();
        }

        photonView.RPC("RPC_Shooting", RpcTarget.All, direction);

        //if (CrossPlatformInputManager.GetAxis("vertical") > 0)
        //{
        //    photonView.RPC("RPC_Shooting", RpcTarget.All, Vector2.up);
        //}
        //else if (CrossPlatformInputManager.GetAxis("vertical") < 0)
        //{
        //    photonView.RPC("RPC_Shooting", RpcTarget.All, Vector2.down);
        //}
    }

    [PunRPC]
    void RPC_Shooting(Vector2 direction)
    {
        RaycastHit hit;

        if (direction.x != 0 || direction.y != 0)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, 1000))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(direction) * hit.distance, Color.red);
                Debug.Log("We Hit" + hit.transform.name, hit.transform);

                if (hit.transform.tag == "Avatar")
                {
                    if (photonView.IsMine)
                    {
                        hit.transform.gameObject.GetComponent<AvatarSetup>().playerHealth -= avatarSetup.playerDamage;
                        health.text = hit.transform.gameObject.GetComponent<AvatarSetup>().playerHealth.ToString();
                    }
                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(direction) * 1000, Color.white);
                Debug.Log("Miss!");
            }
        }
    }
}
