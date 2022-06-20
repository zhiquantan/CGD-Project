using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject Bridge;
    // Start is called before the first frame update
    void Start()
    {
     Instantiate(Bridge,new Vector3(338.14f,402.84f,301.97f),Quaternion.identity);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
