using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class Game : MonoBehaviour
{
    public GameObject Bridge;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bridge"), new Vector3(338.14f,402.84f,301.97f), Quaternion.identity);
        }
     //Instantiate(Bridge,new Vector3(338.14f,402.84f,301.97f),Quaternion.identity);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
