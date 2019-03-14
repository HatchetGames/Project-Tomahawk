using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviourPun
{
    public enum WalkDirection { Right, Left, Up, Down, RightUp, RightDown, LeftUp, LeftDown };
    private Transform playerTransform;
    public float moveSpeed;
    private Vector3 selfPos;
    public GameObject playerCam;
    private GameObject sceneCam;

    public Animator legAnimator;
    public GameObject bodyObject;
    public Animator bodyAnimator;
    public GameObject leftHandObject;
    public GameObject rightHandObject;

    private bool isWalking;
    private bool lastWalkingStatus;

    private void Start()
    {
        isWalking = false;
        lastWalkingStatus = false;
        playerTransform = transform;
        if (photonView.IsMine)
        {
            sceneCam = GameObject.Find("Main Camera");

            if (sceneCam != null && sceneCam.activeInHierarchy)
                sceneCam.SetActive(false);

            playerCam.SetActive(true);
        }
        else
            Destroy(playerCam);
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

        if (hInput != 0 || vInput != 0)
            isWalking = true;
        else
            isWalking = false;

        if (isWalking != lastWalkingStatus)
        {
            lastWalkingStatus = isWalking;
            legAnimator.SetBool("IsWalking", isWalking);
        }

        if (isWalking)
        {
            playerTransform.Translate(hInput, vInput, 0.0f);
        }
    }
}
