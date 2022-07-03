using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
using System;
using System.Linq;
using System.IO;

public class DestroySupriseElement : MonoBehaviour
{
    public bool Obtained=false;
   
   PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    
    void Start()
    {
        InvokeRepeating("Destroy", 10,10f);
    }

    public void Destroy()
    {
             Destroy(gameObject);
       
    }

    void Update()
    {
        if(Obtained==true)
        {
            PV.RPC("RPC_KillSupriseElement", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void RPC_KillSupriseElement()
    {
            PhotonNetwork.Destroy(gameObject);
            SpawnMaterial.count1--;    
    }
}

