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

public class MaterialDestroy : MonoBehaviour
{
    public bool Obtained=false;
   
   PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Destroy", 20,20f);
    }

    public void Destroy()
    {
        if(gameObject.tag=="Stone")
        {
             Destroy(gameObject);
             SpawnMaterial.count1--;
        }

        else if(gameObject.tag=="Wood")
        {
             Destroy(gameObject);
             SpawnMaterial.count2--;
        }
       
    }

    

    // Update is called once per frame
    void Update()
    {
        if(Obtained==true)
        {
            PV.RPC("RPC_KillMaterial", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void RPC_KillMaterial()
    {
    if(gameObject.tag=="Stone")
        {
            PhotonNetwork.Destroy(gameObject);
            SpawnMaterial.count1--;
        }
    
    else if(gameObject.tag=="Wood")
    {
        PhotonNetwork.Destroy(gameObject);
        SpawnMaterial.count2--;
    }
                 
    }
}
