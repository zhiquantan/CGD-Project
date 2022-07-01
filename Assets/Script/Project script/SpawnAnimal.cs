using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class SpawnAnimal : MonoBehaviour
{
    public GameObject Animal1;
    public GameObject Animal2;
    public int xPos;
    public int zPos;
    public int count;
    public int no;
    
    void Start()
{
    if (PhotonNetwork.IsMasterClient)
    {
        StartCoroutine(EnemyDrop());
    }
    
}

IEnumerator EnemyDrop()
{
    while(count<20)
    {
        xPos=Random.Range(-51,423);
        zPos=Random.Range(-62,-22);
        no=Random.Range(0,3);
       
        if(no==1)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Animal1"), new Vector3(xPos,92,zPos), Quaternion.identity);
            //Instantiate(Animal1,new Vector3(xPos,92,zPos),Quaternion.identity);
            yield return new WaitForSeconds(5);
            count++;
        }
        else if(no==2)
        {
             PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Animal2"), new Vector3(xPos,83,zPos), Quaternion.identity);
           // Instantiate(Animal2,new Vector3(xPos,83,zPos),Quaternion.identity);
            yield return new WaitForSeconds(5);
            count++;
        }
        
    }
}

}

