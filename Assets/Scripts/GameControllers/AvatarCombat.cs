using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class AvatarCombat : MonoBehaviourPunCallbacks, IPunObservable
{

    private AvatarSetup avatarSetup;

    public GameObject bulletSpawnPoint;
    public GameObject bullet;
    public float fireRate = 0.25f;
    public int magCapacity = 5;
    public float reloadTime = 1.0f;

    public float timeCounter = 0.0f;

    private GameObject bulletSpawned;
    private bool isReloading;
    private float timeToReload;
    private int curAmmoCount;

    private float timeToFire;
    private bool canAttack;
    private bool isAttacking;

    // Use this for initialization
    void Start()
    {
        if (photonView.IsMine)
        {
            avatarSetup = GetComponent<AvatarSetup>();
            bulletSpawnPoint = GameObject.FindGameObjectWithTag("BulletSpawnPoint");
            isReloading = false;
            timeToReload = reloadTime;
            curAmmoCount = magCapacity;
            timeToFire = fireRate;
            canAttack = true;
            isAttacking = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;

        Vector2 direction;
        float horizontal = CrossPlatformInputManager.GetAxis("horizontal");
        float vertical = CrossPlatformInputManager.GetAxis("vertical");

        direction = new Vector2(horizontal, vertical);
        Rotate(direction);

        //Reloading time
        if(isReloading && timeToReload > 0.0f)
        {
            timeToReload -= Time.deltaTime;
        }

        if(timeToReload <= 0.0f)
        {
            isReloading = false;
            timeToReload = reloadTime;
            curAmmoCount = magCapacity;
        }

        //Fire rate time
        if (!canAttack && timeToFire > 0.0f)
            timeToFire -= Time.deltaTime;

        if(timeToFire <= 0.0f)
        {
            canAttack = true;
            timeToFire = fireRate;
        }

        if (direction.x != 0.0f || direction.y != 0.0f)
        {
            isAttacking = true;

            if (curAmmoCount > 0)
            {
                //Shoot
                if (canAttack)
                    Shoot();
            }
            else
            {
                //Out of ammo
                if (isReloading == false)
                    isReloading = true;
                timeToReload = reloadTime;
            }
        }
        else
            isAttacking = false;
    }

    void Rotate(Vector2 direction)
    {
        //if (direction.x == 0f && direction.y == 0f)
        //{
        //    Vector3 curRot = transform.eulerAngles;
        //    Vector3 homeRot;

        //    if (curRot.z > 180f)
        //    {
        //        homeRot = new Vector3(0f, 0f, 359.999f);
        //    }
        //    else
        //        homeRot = Vector3.zero;

        //    transform.eulerAngles = Vector3.Slerp(curRot, homeRot, Time.deltaTime * 2);
        //}
        //else
        if(direction.x != 0f || direction.y != 0f)
            transform.eulerAngles = new Vector3(0f, 0f, -(Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg));
    }

    void Shoot()
    {
        --curAmmoCount;
        canAttack = false;
        photonView.RPC("RPC_Shooting", RpcTarget.All);
    }

    [PunRPC]
    void RPC_Shooting()
    {
        /*bulletSpawned = */PhotonNetwork.Instantiate("Projectiles/Bullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation, 0);
        //bulletSpawned.transform.parent = bulletSpawnPoint.transform;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(isAttacking);
            stream.SendNext(avatarSetup.GetHealth());
        }
        else
        {
            this.isAttacking = (bool)stream.ReceiveNext();
            avatarSetup.SetHealth((int)stream.ReceiveNext());
        }
    }
}
