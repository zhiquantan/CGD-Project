using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideBridge : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Animal")
        {
            Debug.Log("sian");
            Destroy(other.gameObject);
        }
    }
}
