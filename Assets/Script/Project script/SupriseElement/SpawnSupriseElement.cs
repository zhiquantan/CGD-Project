using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class SpawnSupriseElement : MonoBehaviour
{
    public GameObject Booster1;
    public GameObject Booster2;
    public GameObject Booster3;
    public int xPos;
    public int zPos;
    public int no;
    public bool SpawnObject=true;
    
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(EnemyDrop());
        }
        
    }

    IEnumerator EnemyDrop()
    {
        while(SpawnObject)
        {
            xPos=Random.Range(-5,420);
            zPos=Random.Range(-50,230);
            no=Random.Range(0,4);
            

            if(no==1)
            {
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Booster1"), new Vector3(xPos,60,zPos), Quaternion.identity);
                //Instantiate(,new Vector3(xPos,60,zPos),Quaternion.identity);
                yield return new WaitForSeconds(15);
                
            }
            else if(no==2)
            {
                 PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Booster2"), new Vector3(xPos,60,zPos), Quaternion.identity);
                //Instantiate(Booster2,new Vector3(xPos,60,zPos),Quaternion.identity);
                yield return new WaitForSeconds(15);
                
            }

            else if(no==3)
            {
                 PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Booster3"), new Vector3(xPos,60,zPos), Quaternion.identity);
                //Instantiate(Booster3,new Vector3(xPos,60,zPos),Quaternion.identity);
                yield return new WaitForSeconds(15);
                
            }
            else{
                 yield return new WaitForSeconds(3);
            }
            
        }
    }
    }
