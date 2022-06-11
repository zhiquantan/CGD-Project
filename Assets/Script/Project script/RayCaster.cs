using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    public GameObject Hit;
    public GameObject AnimalIcon;
    public GameObject NothingIcon;
    public GameObject MaterialIcon;
    
    void FixedUpdate()
    {
        var ray = new Ray(transform.position, this.transform.TransformDirection(Vector3.forward));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,8))
        {
            //Debug.DrawRay(transform.position, this.transform.TransformDirection(Vector3.forward)*hit.distance,Color.red);
             Hit = hit.transform.gameObject;

            if (Hit.tag == "Material")
            {

                MaterialIcon.GetComponent<SpriteRenderer>().enabled = true;
                NothingIcon.GetComponent<SpriteRenderer>().enabled = false;

                if (Input.GetMouseButtonDown(0))
                {
                   Debug.Log("M");
                }
            }

            else if (Hit.tag == "Animal")
            {

                AnimalIcon.GetComponent<SpriteRenderer>().enabled = true;
                NothingIcon.GetComponent<SpriteRenderer>().enabled = false;

                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("A");
                }
            }

            else
            {
                AnimalIcon.GetComponent<SpriteRenderer>().enabled = false;
                MaterialIcon.GetComponent<SpriteRenderer>().enabled = false;
                NothingIcon.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
