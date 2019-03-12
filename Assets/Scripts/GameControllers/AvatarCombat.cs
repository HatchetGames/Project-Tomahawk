using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class AvatarCombat : MonoBehaviourPun
{

    private AvatarSetup avatarSetup;

    //public Text health;
    //public Text clientHealth;
    public GameObject bulletSpawnPoint;
    public float waitTime;

    public float timeCounter = 0.0f;

    // Use this for initialization
    void Start()
    {
        avatarSetup = GetComponent<AvatarSetup>();
        //hostHealth = GameObject.Find("healthText").GetComponent<Text>();

        //if(health == null)
        //{
        //    Debug.LogError("Cant find health object");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;

        //float weaponX = 0.0f;
        //float weaponY = 0.0f;
        Vector2 direction;
        float horizontal = CrossPlatformInputManager.GetAxis("horizontal");
        float vertical = CrossPlatformInputManager.GetAxis("vertical");

        direction = new Vector2(
            horizontal,
            vertical
            );

        if (direction.x != 0 || direction.y != 0)
        {
            //transform.Rotate(new Vector3(0f, 0f, horizontal));

            if (direction.magnitude > 1)
            {
                direction.Normalize();
            }

            //photonView.RPC("RPC_Shooting", RpcTarget.All, direction);
            Shoot(direction);
        }
    }

    void Shoot(Vector2 direction)
    {
        photonView.RPC("RPC_Shooting", RpcTarget.All, direction);
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
                        //health.text = hit.transform.gameObject.GetComponent<AvatarSetup>().playerHealth.ToString();
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
