using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySupriseElement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Destroy", 10,10f);
    }

    public void Destroy()
    {
             Destroy(gameObject);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
