using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 5, 0);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        {
            if (other.tag == "dood")
            {
                // gameflow.totalCoins += 1;
                // Debug.Log(gameflow.totalCoins);
                Destroy(gameObject);
            }
        }
    }
}
