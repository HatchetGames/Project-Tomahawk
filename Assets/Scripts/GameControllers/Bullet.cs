using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviourPun
{
    public float speed = 1.0f;
    public int damage = 20;
    private float maxDistance;

    private AvatarSetup triggeringEnemy;
    private Rigidbody2D bulletRB;

	// Use this for initialization
	void Start () {
        bulletRB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        bulletRB.velocity = transform.up * speed;
        maxDistance += 1 * Time.deltaTime;

        if (maxDistance >= 5)
            PhotonNetwork.Destroy(gameObject);
	}

    void ProjectileDie()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            triggeringEnemy = collision.GetComponent<AvatarSetup>();
            triggeringEnemy.ApplyDamage(damage);
            ProjectileDie();
        }
        else
            ProjectileDie();
    }

    [PunRPC]
    void RPC_Bullet()
    {

    }
}
