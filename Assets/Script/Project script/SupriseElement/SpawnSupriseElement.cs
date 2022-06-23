using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    StartCoroutine(EnemyDrop());
}

IEnumerator EnemyDrop()
{
    while(SpawnObject)
    {
        xPos=Random.Range(-5,420);
        zPos=Random.Range(-50,230);
        no=Random.Range(0,15);
        

        if(no==1)
        {
            Instantiate(Booster1,new Vector3(xPos,60,zPos),Quaternion.identity);
            yield return new WaitForSeconds(5);
            
        }
        else if(no==2)
        {
            Instantiate(Booster2,new Vector3(xPos,60,zPos),Quaternion.identity);
            yield return new WaitForSeconds(5);
            
        }

        else if(no==2)
        {
            Instantiate(Booster3,new Vector3(xPos,60,zPos),Quaternion.identity);
            yield return new WaitForSeconds(5);
            
        }
        
    }
}
}
