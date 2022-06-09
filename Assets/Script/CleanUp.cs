using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < Ruuning.doodPos.z - 2)
        {
            Destroy(gameObject);
        }
    }
}
