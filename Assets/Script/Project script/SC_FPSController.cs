using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using System;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public string Name;
    public float curSpeedX;
    public float curSpeedY;
    public static string rank;
    public GameObject medal1;
    public GameObject medal2;
    public GameObject medal3;
    public GameObject Over;
    PhotonView PV;
    public AudioSource walking;
    public AudioSource jumping;
    public GameObject TopEffect;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        Over=GameObject.FindGameObjectsWithTag("Over")[0];
    }

    void Start()
    {
        
        if (!PV.IsMine)
        {
            GetComponentInChildren<Camera>().enabled = false;
        }
        Name=string.Format(PV.Owner.NickName);
        
        if (PV.IsMine)
        {
            Name=string.Format(PV.Owner.NickName);
            Over.GetComponent<GameOver>().PlayerName=Name;

            PV.RPC("RPC_Medal", RpcTarget.AllBuffered, rank);
            
        }
        characterController = GetComponent<CharacterController>();

        walking = GameObject.Find("walking").GetComponent<AudioSource>();
        jumping = GameObject.Find("jump").GetComponent<AudioSource>();
        

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!PV.IsMine)
            return;
        // We are grounded, so recalculate move direction based on axes
        
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
            jumping.Play();
        }
        else
        {
            moveDirection.y = movementDirectionY;
            
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
        //walking.Play();

        // Player and Camera rotation
        if (canMove)
        {
            //walking.Play();
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    [PunRPC]
    void RPC_Medal(string rank)
    {
        if(rank=="1")
            {
                medal1.SetActive(true);
                TopEffect.SetActive(true);
            }

            else if(rank=="2")
            {
                medal2.SetActive(true);
                TopEffect.SetActive(true);
            }

            else if(rank=="3")
            {
                medal3.SetActive(true);
            }
    }
}