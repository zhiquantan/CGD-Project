using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canMove : MonoBehaviour
{
    public Vector3 CameraSpeed;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0.7f);
        CameraSpeed = GetComponent<Rigidbody>().velocity;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = CameraSpeed;
    }
}
