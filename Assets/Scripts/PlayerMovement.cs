using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviourPun, IPunObservable {

    public bool devTesting = false;
    public float moveSpeed;
    private Vector3 selfPos;
    public GameObject playerCam;
    private GameObject sceneCam;

    private void Awake()
    {
        if (!devTesting && photonView.IsMine)
        {
            //sceneCam = GameObject.Find("Main Camera");
            //sceneCam.SetActive(false);
            playerCam.SetActive(true);
        }
        //else if (!photonView.IsMine && GetComponent<PlayerMovement>() != null)
        //    Destroy(GetComponent<PlayerMovement>());
            
    }

    // Update is called once per frame
    private void Update()
    {
        if (!devTesting)
        {
            if (photonView.IsMine)
                CheckInput();
            else
            {
                SmoothNetMovement();
            }
        }
        else
            CheckInput();
    }

    private void CheckInput()
    {
        float hInput = CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float vInput = CrossPlatformInputManager.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(hInput, vInput, 0.0f);
    }

    private void SmoothNetMovement()
    {
        transform.position = Vector3.Lerp(transform.position, selfPos, Time.deltaTime * 2);
    }

    public static void RefreshInstance(ref PlayerMovement player, PlayerMovement prefab)
    {
        var position = Vector3.zero;
        var rotation = Quaternion.identity;

        if(player != null)
        {
            position = player.transform.position;
            rotation = player.transform.rotation;
            PhotonNetwork.Destroy(player.gameObject);
        }

        player = PhotonNetwork.Instantiate(prefab.gameObject.name, position, rotation).GetComponent<PlayerMovement>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position.x);
            stream.SendNext(transform.position.y);
        }
        else
        {
            selfPos.x = (float)stream.ReceiveNext();
            selfPos.y = (float)stream.ReceiveNext();
        }
    }
}
