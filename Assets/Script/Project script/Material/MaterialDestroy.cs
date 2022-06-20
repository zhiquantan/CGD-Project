using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDestroy : MonoBehaviour
{
   
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
        
    }
}
