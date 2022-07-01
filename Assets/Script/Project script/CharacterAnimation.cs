using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
using System;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;
    public float speed;
    public GameObject FPS;
    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV.IsMine)
        {
            animator=this.GetComponent<Animator>();
        }
        
    }
    
    void Update()
    {
        if (!PV.IsMine)
            return;
        // Debug.Log(FPS.GetComponent<SC_FPSController>().curSpeedY);
        
        if(FPS.GetComponent<SC_FPSController>().curSpeedX==0&&FPS.GetComponent<SC_FPSController>().curSpeedY==0)
        {
            speed=0;
        }

        else{
            speed=1;
        }

        animator.SetFloat("Speed",speed);

        
    }
}
