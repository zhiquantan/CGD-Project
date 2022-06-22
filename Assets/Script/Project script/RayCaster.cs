using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCaster : MonoBehaviour
{
    public GameObject Hit;
    public GameObject AnimalIcon;
    public GameObject NothingIcon;
    public GameObject MaterialIcon;
    public int currentWood=0;
    public int currentStone=0;
    public Text currentWoodUI;
    public Text currentStoneUI;
    public GameObject WarningUI;
    public GameObject Bridge;
    void Start()
    {
        StartCoroutine(FindBridge());
        
    }
    void Update()
    {
        currentWoodUI.text=currentWood.ToString();
        currentStoneUI.text=currentStone.ToString();
    }
    void FixedUpdate()
    {
        var ray = new Ray(transform.position, this.transform.TransformDirection(Vector3.forward));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,60))
        {
            //Debug.DrawRay(transform.position, this.transform.TransformDirection(Vector3.forward)*hit.distance,Color.red);
             Hit = hit.transform.gameObject;

            if (Hit.tag == "Stone")
            {

                MaterialIcon.GetComponent<SpriteRenderer>().enabled = true;
                NothingIcon.GetComponent<SpriteRenderer>().enabled = false;

                if (Input.GetMouseButtonDown(0)&&currentStone<5)
                {
                   currentStone++;
                   currentStoneUI.text=currentStone.ToString();
                   Destroy(Hit.gameObject);
                   SpawnMaterial.count1--;
                }

                else if(Input.GetMouseButtonDown(0)&&currentStone>=5)
                {
                    StartCoroutine(WarningText());
                }
            }

            else if (Hit.tag == "Wood")
            {

                MaterialIcon.GetComponent<SpriteRenderer>().enabled = true;
                NothingIcon.GetComponent<SpriteRenderer>().enabled = false;

                if (Input.GetMouseButtonDown(0)&&currentWood<5)
                {
                   currentWood++;
                   currentWoodUI.text=currentWood.ToString();
                    Destroy(Hit.gameObject);
                    SpawnMaterial.count2--;
                }

                 else if(Input.GetMouseButtonDown(0)&&currentWood>=5)
                {
                     StartCoroutine(WarningText());
                }
            }

            else if (Hit.tag == "Animal")
            {

                AnimalIcon.GetComponent<SpriteRenderer>().enabled = true;
                NothingIcon.GetComponent<SpriteRenderer>().enabled = false;

                if (Input.GetMouseButtonDown(0))
                {
                    Destroy(Hit.gameObject);
                    Debug.Log("Animal Die");
                }

                
            }

            else if (Hit.tag == "CollidePoint")
            {

                MaterialIcon.GetComponent<SpriteRenderer>().enabled = true;
                NothingIcon.GetComponent<SpriteRenderer>().enabled = false;

            if (Input.GetMouseButtonDown(0))
                {
                    Bridge.GetComponent<Bridge>().currentWood=Bridge.GetComponent<Bridge>().currentWood+currentWood;
                    Bridge.GetComponent<Bridge>().currentStone=Bridge.GetComponent<Bridge>().currentStone+currentStone;
                    currentWood=0;
                    currentStone=0;
                    Bridge.GetComponent<Bridge>().Phase();
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
    IEnumerator WarningText()
    {
        WarningUI.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        WarningUI.SetActive(false);
    }

    IEnumerator FindBridge()
    {
       
        yield return new WaitForSeconds(1.0f);
         Bridge= GameObject.FindGameObjectsWithTag("Bridge")[0];
    }

   
}
