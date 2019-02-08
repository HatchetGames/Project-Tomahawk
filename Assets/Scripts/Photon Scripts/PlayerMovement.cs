using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviourPun
{
    private CharacterController myCC;
    public float moveSpeed;
    private Vector3 selfPos;
    public GameObject playerCam;
    private GameObject sceneCam;

    private void Start()
    {
        myCC = GetComponent<CharacterController>();
        if (photonView.IsMine)
        {
            sceneCam = GameObject.Find("Main Camera");

            if (sceneCam != null && sceneCam.activeInHierarchy)
                sceneCam.SetActive(false);

            playerCam.SetActive(true);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (photonView.IsMine)
        {
            GetMovement();
        }
    }

    private void GetMovement()
    {
        float hInput = CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float vInput = CrossPlatformInputManager.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        //transform.Translate(hInput, vInput, 0.0f);
        myCC.Move(new Vector3(hInput, vInput));
    }
}
