using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedCount : MonoBehaviour
{
    public int Attackcount;
    public int Hp=2;
    public GameObject AnimalSpawn;

    void Start()
    {
        StartCoroutine(FindAnimalSpawnObject());
        
    }
    
    void Update()
    {
        if(Attackcount==Hp)
        {
            Destroy(gameObject);
            AnimalSpawn.GetComponent<SpawnAnimal>().count=AnimalSpawn.GetComponent<SpawnAnimal>().count-1;
        }
    }

    IEnumerator FindAnimalSpawnObject()
    {
       
        yield return new WaitForSeconds(1.0f);
         AnimalSpawn= GameObject.FindGameObjectsWithTag("AnimalSpawnObject")[0];
    }
}
