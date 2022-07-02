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
    public int currentWood = 0;
    public int currentStone = 0;
    public GameObject currentWoodUI;
    public GameObject currentStoneUI;
    public GameObject WarningUI;
    public GameObject Bridge;
    public int AttackDamage = 1;
    public GameObject Player;
    public ParticleSystem punchfx;
    public AudioSource moosedeath;
    public AudioSource foxdeath;
    void Start()
    {
        currentWoodUI = GameObject.FindGameObjectsWithTag("WoodUI")[0];
        currentStoneUI = GameObject.FindGameObjectsWithTag("RockUI")[0];
        WarningUI = GameObject.FindGameObjectsWithTag("WarningUI")[0];
        punchfx = GameObject.FindGameObjectsWithTag("punchfx")[0].GetComponent<ParticleSystem>();
        moosedeath = GameObject.Find("moosedeath").GetComponent<AudioSource>();
        foxdeath = GameObject.Find("foxdeath").GetComponent<AudioSource>();
        WarningUI.SetActive(false);
        StartCoroutine(FindBridge());

    }
    void Update()
    {
        currentWoodUI.GetComponent<Text>().text = currentWood.ToString();
        currentStoneUI.GetComponent<Text>().text = currentStone.ToString();
    }
    void FixedUpdate()
    {
        var ray = new Ray(transform.position, this.transform.TransformDirection(Vector3.forward));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            //Debug.DrawRay(transform.position, this.transform.TransformDirection(Vector3.forward)*hit.distance,Color.red);
            Hit = hit.transform.gameObject;

            if (Hit.tag == "Stone")
            {

                MaterialIcon.GetComponent<SpriteRenderer>().enabled = true;
                NothingIcon.GetComponent<SpriteRenderer>().enabled = false;

                if (Input.GetMouseButtonDown(0) && currentStone < 5)
                {
                    currentStone++;
                    currentStoneUI.GetComponent<Text>().text = currentStone.ToString();
                    Destroy(Hit.gameObject);
                    SpawnMaterial.count1--;
                }

                else if (Input.GetMouseButtonDown(0) && currentStone >= 5)
                {
                    StartCoroutine(WarningText());
                }
            }

            else if (Hit.tag == "Wood")
            {

                MaterialIcon.GetComponent<SpriteRenderer>().enabled = true;
                NothingIcon.GetComponent<SpriteRenderer>().enabled = false;

                if (Input.GetMouseButtonDown(0) && currentWood < 5)
                {
                    currentWood++;
                    currentWoodUI.GetComponent<Text>().text = currentWood.ToString();
                    Destroy(Hit.gameObject);
                    SpawnMaterial.count2--;
                }

                else if (Input.GetMouseButtonDown(0) && currentWood >= 5)
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
                    Hit.gameObject.GetComponent<AttackedCount>().Attackcount = Hit.gameObject.GetComponent<AttackedCount>().Attackcount + AttackDamage;
                    punchfx.transform.position = hit.point;
                    punchfx.Play();
                    if (Hit.gameObject.name == "Animal2(Clone)")
                    {
                        moosedeath.transform.position = hit.point;
                        moosedeath.Play();
                    }
                    if (Hit.gameObject.name == "Animal1(Clone)")
                    {
                        foxdeath.transform.position = hit.point;
                        foxdeath.Play();
                    }
                }


            }

            else if (Hit.tag == "CollidePoint")
            {

                MaterialIcon.GetComponent<SpriteRenderer>().enabled = true;
                NothingIcon.GetComponent<SpriteRenderer>().enabled = false;

                if (Input.GetMouseButtonDown(0))
                {
                    Bridge.GetComponent<Bridge>().currentWood = Bridge.GetComponent<Bridge>().currentWood + currentWood;
                    Bridge.GetComponent<Bridge>().currentStone = Bridge.GetComponent<Bridge>().currentStone + currentStone;
                    currentWood = 0;
                    currentStone = 0;
                    Bridge.GetComponent<Bridge>().Phase();
                }

            }

            else if (Hit.tag == "StopTime")
            {

                MaterialIcon.GetComponent<SpriteRenderer>().enabled = true;
                NothingIcon.GetComponent<SpriteRenderer>().enabled = false;

                if (Input.GetMouseButtonDown(0))
                {
                    Destroy(Hit.gameObject);
                    StartCoroutine(StopTime());
                }

            }

            else if (Hit.tag == "IncreaseDamage")
            {

                MaterialIcon.GetComponent<SpriteRenderer>().enabled = true;
                NothingIcon.GetComponent<SpriteRenderer>().enabled = false;

                if (Input.GetMouseButtonDown(0))
                {
                    Destroy(Hit.gameObject);
                    StartCoroutine(IncreaseDamage());
                }

            }

            else if (Hit.tag == "IncreaseSpeed")
            {

                MaterialIcon.GetComponent<SpriteRenderer>().enabled = true;
                NothingIcon.GetComponent<SpriteRenderer>().enabled = false;

                if (Input.GetMouseButtonDown(0))
                {
                    Destroy(Hit.gameObject);
                    StartCoroutine(InreaseSpeed());
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
        Bridge = GameObject.FindGameObjectsWithTag("Bridge")[0];


    }

    IEnumerator InreaseSpeed()
    {
        Player.GetComponent<SC_FPSController>().walkingSpeed = 40;
        Player.GetComponent<SC_FPSController>().runningSpeed = 60;
        yield return new WaitForSeconds(10.0f);
        Player.GetComponent<SC_FPSController>().walkingSpeed = 20;
        Player.GetComponent<SC_FPSController>().runningSpeed = 30;
    }

    IEnumerator StopTime()
    {
        Bridge.GetComponent<Bridge>().StopTime = true;
        yield return new WaitForSeconds(20.0f);
        Bridge.GetComponent<Bridge>().StopTime = false;
    }

    IEnumerator IncreaseDamage()
    {
        AttackDamage = 2;
        yield return new WaitForSeconds(20.0f);
        AttackDamage = 1;

    }



}
