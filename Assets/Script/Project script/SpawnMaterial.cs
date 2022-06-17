using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMaterial : MonoBehaviour
{
    public GameObject Material1;
    public GameObject Material2;
    public int xPos;
    public int zPos;
    public int count1;
    public int count2;
    public int no;
    
    void Start()
{
    StartCoroutine(EnemyDrop());
}

IEnumerator EnemyDrop()
{
    while(count1+count2<10)
    {
        xPos=Random.Range(-5,420);
        zPos=Random.Range(-50,230);
        no=Random.Range(0,3);
        Debug.Log(no);

        if(no==1&&count1<=5)
        {
            Instantiate(Material1,new Vector3(xPos,70,zPos),Quaternion.identity);
            yield return new WaitForSeconds(5);
            count1++;
        }
        else if(no==2&&count2<=5)
        {
            Instantiate(Material2,new Vector3(xPos,70,zPos),Quaternion.identity);
            yield return new WaitForSeconds(5);
            count2++;
        }
        
    }
}

}