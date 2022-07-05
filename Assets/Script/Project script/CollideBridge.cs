using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollideBridge : MonoBehaviour
{
    public Image BridgeLife;
    public GameObject Bridge;
    public GameObject AnimalSpawn;

    void Start()
    {
        StartCoroutine(FindAnimalSpawnObject());
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Animal")
        {
            if(Bridge.GetComponent<Bridge>().CurrentPhase!=0&&Bridge.GetComponent<Bridge>().CurrentPhase!=3)
            {
                BridgeLife.fillAmount=BridgeLife.fillAmount-0.1f;
            }
            
            Destroy(other.gameObject);
            AnimalSpawn.GetComponent<SpawnAnimal>().count=AnimalSpawn.GetComponent<SpawnAnimal>().count-1;
        }
    }
    IEnumerator FindAnimalSpawnObject()
    {
       
        yield return new WaitForSeconds(1.0f);
         AnimalSpawn= GameObject.FindGameObjectsWithTag("AnimalSpawnObject")[0];
    }
    
}
