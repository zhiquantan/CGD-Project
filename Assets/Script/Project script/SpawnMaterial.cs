using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;


public class SpawnMaterial : MonoBehaviour
{
    public GameObject Material1;
    public GameObject Material2;
    public int xPos;
    public int zPos;
    public static int count1;
    public static int count2;
    public int no;
    public static float SpawnSpeed;
    
    void Start()
{
    if (PhotonNetwork.IsMasterClient)
    {
        StartCoroutine(EnemyDrop());
    }
    
}

IEnumerator EnemyDrop()
{
    while(count1+count2<10)
    {
        xPos=Random.Range(-5,420);
        zPos=Random.Range(-50,230);
        no=Random.Range(0,3);
        

        if(no==1&&count1<=5)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Material1"), new Vector3(xPos,60,zPos), Quaternion.identity);
            //Instantiate(Material1,new Vector3(xPos,60,zPos),Quaternion.identity);
            yield return new WaitForSeconds(SpawnSpeed);
            count1++;
        }
        else if(no==2&&count2<=5)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Material2"), new Vector3(xPos,60,zPos), Quaternion.identity);
            //Instantiate(Material2,new Vector3(xPos,60,zPos),Quaternion.identity);
            yield return new WaitForSeconds(SpawnSpeed);
            count2++;
        }
        
    }
}

}