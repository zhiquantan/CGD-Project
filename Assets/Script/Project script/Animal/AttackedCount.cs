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

public class AttackedCount : MonoBehaviour
{
    public int Attackcount;
    public static int Hp=2;
    public GameObject AnimalSpawn;
    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        StartCoroutine(FindAnimalSpawnObject());
        
    }
    
    void Update()
    {
        if(Attackcount==Hp)
        {
            PV.RPC("RPC_KillAnimal", RpcTarget.AllBuffered);
            //PhotonNetwork.Destroy(gameObject);
            // Destroy(gameObject);
            // AnimalSpawn.GetComponent<SpawnAnimal>().count=AnimalSpawn.GetComponent<SpawnAnimal>().count-1;
        }
    }

    IEnumerator FindAnimalSpawnObject()
    {
       
        yield return new WaitForSeconds(1.0f);
         AnimalSpawn= GameObject.FindGameObjectsWithTag("AnimalSpawnObject")[0];
    }

    [PunRPC]
    void RPC_KillAnimal()
    {
        if (PhotonNetwork.IsMasterClient)
    {
        PhotonNetwork.Destroy(gameObject);
        AnimalSpawn.GetComponent<SpawnAnimal>().count=AnimalSpawn.GetComponent<SpawnAnimal>().count-1;
    }
                 
    }
}
