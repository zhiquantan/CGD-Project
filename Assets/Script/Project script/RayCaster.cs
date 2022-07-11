using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
using System;
using System.Linq;
using System.IO;

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
    private bool warningFind=false;
    public ParticleSystem punchfx;
    public AudioSource moosedeath;
    public AudioSource foxdeath;
    public GameObject SpeedUI;
    public GameObject DamageUI;
    public bool waiting=true;
   
    [SerializeField] public AudioSource CollectItem;
    [SerializeField] public AudioSource PowerUp;
    public bool FindWT=false;
    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        SpeedUI=GameObject.FindGameObjectsWithTag("SUI")[0];
        DamageUI=GameObject.FindGameObjectsWithTag("DUI")[0];
        currentWoodUI=GameObject.FindGameObjectsWithTag("WoodUI")[0];
        currentStoneUI=GameObject.FindGameObjectsWithTag("RockUI")[0];
        punchfx = GameObject.FindGameObjectsWithTag("punchfx")[0].GetComponent<ParticleSystem>();
        moosedeath = GameObject.Find("moosedeath").GetComponent<AudioSource>();
        foxdeath = GameObject.Find("foxdeath").GetComponent<AudioSource>();
        CollectItem = GameObject.Find("collectitem").GetComponent<AudioSource>();
        PowerUp = GameObject.Find("powerup").GetComponent<AudioSource>();
        StartCoroutine(FindBridge());
        
    }
    
    void Update()
    {
        if(FindWT==false)
        {
           StartCoroutine(FindWarningText());
             
        }
        if (!PV.IsMine)
        return;
        currentWoodUI.GetComponent<Text>().text=currentWood.ToString();
        currentStoneUI.GetComponent<Text>().text=currentStone.ToString();
        
        
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

                if (Input.GetMouseButtonDown(0) && currentStone < 5&&waiting==true)
                {
                    CollectItem.Play();
                    Debug.Log(currentStone);
                   currentStone=currentStone+1;
                   StartCoroutine(Wait());
                   currentStoneUI.GetComponent<Text>().text=currentStone.ToString();
                   Hit.gameObject.GetComponent<MaterialDestroy>().Obtained=true;
                //    PV.RPC("RPC_GetStone", RpcTarget.AllBuffered, Hit.gameObject);
                //    PhotonNetwork.Destroy(Hit.gameObject);
                //    SpawnMaterial.count1--;
                }

                else if (Input.GetMouseButtonDown(0) && currentStone >= 5&&waiting==true)
                {
                    StartCoroutine(WarningText());
                }
            }

            else if (Hit.tag == "Wood")
            {

                MaterialIcon.GetComponent<SpriteRenderer>().enabled = true;
                NothingIcon.GetComponent<SpriteRenderer>().enabled = false;
                

                if (Input.GetMouseButtonDown(0) && currentWood < 5&&waiting==true)
                {
                    CollectItem.Play();
                Debug.Log(currentWood);
                   currentWood=currentWood+1;
                   currentWoodUI.GetComponent<Text>().text=currentWood.ToString();
                   Hit.gameObject.GetComponent<MaterialDestroy>().Obtained=true;
                //    PV.RPC("RPC_GetWood", RpcTarget.AllBuffered, Hit.gameObject);
                    // PhotonNetwork.Destroy(Hit.gameObject);
                    // SpawnMaterial.count2--;
                }

                else if (Input.GetMouseButtonDown(0) && currentWood >= 5&&waiting==true)
                {
                    StartCoroutine(WarningText());
                }
            }

            else if (Hit.tag == "Animal")
            {

                AnimalIcon.GetComponent<SpriteRenderer>().enabled = true;
                NothingIcon.GetComponent<SpriteRenderer>().enabled = false;

                if (Input.GetMouseButtonDown(0)&&waiting==true)
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
                    
                    PV.RPC("RPC_BuildBridge", RpcTarget.AllBuffered,currentWood,currentStone );
                    currentWood=0;
                    currentStone=0;
                    // Bridge.GetComponent<Bridge>().currentWood=Bridge.GetComponent<Bridge>().currentWood+currentWood;
                    // Bridge.GetComponent<Bridge>().currentStone=Bridge.GetComponent<Bridge>().currentStone+currentStone;
                    
                    // Bridge.GetComponent<Bridge>().Phase();
                }

            }

            else if (Hit.tag == "StopTime")
            {

                MaterialIcon.GetComponent<SpriteRenderer>().enabled = true;
                NothingIcon.GetComponent<SpriteRenderer>().enabled = false;

                

             if (Input.GetMouseButtonDown(0))
                 {
                    PowerUp.Play();

                    PV.RPC("RPC_StopTime", RpcTarget.AllBuffered);
                    Hit.gameObject.GetComponent<DestroySupriseElement>().Obtained=true;
                    //  PhotonNetwork.Destroy(Hit.gameObject);
                    //  StartCoroutine(StopTime());
                 }
                
            }

            else if (Hit.tag == "IncreaseDamage")
            {

                MaterialIcon.GetComponent<SpriteRenderer>().enabled = true;
                NothingIcon.GetComponent<SpriteRenderer>().enabled = false;

                if (Input.GetMouseButtonDown(0))
                {
                    // PhotonNetwork.Destroy(Hit.gameObject);
                    PowerUp.Play();
                    StartCoroutine(IncreaseDamage());
                    Hit.gameObject.GetComponent<DestroySupriseElement>().Obtained=true;
                    //PV.RPC("RPC_IncreaseDamage", RpcTarget.AllBuffered, Hit);
                    
                }

            }

            else if (Hit.tag == "IncreaseSpeed")
            {

                MaterialIcon.GetComponent<SpriteRenderer>().enabled = true;
                NothingIcon.GetComponent<SpriteRenderer>().enabled = false;

                if (Input.GetMouseButtonDown(0))
                {
                   //PhotonNetwork.Destroy(Hit.gameObject);
                   PowerUp.Play();
                   StartCoroutine(InreaseSpeed());
                   Hit.gameObject.GetComponent<DestroySupriseElement>().Obtained=true;
                   //PV.RPC("RPC_IncreaseSpeed", RpcTarget.AllBuffered, Hit);
                    
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
        yield return new WaitForSeconds(4.0f);
         Bridge= GameObject.FindGameObjectsWithTag("Bridge")[0];

    }

    IEnumerator InreaseSpeed()
    {
        Player.GetComponent<SC_FPSController>().walkingSpeed = 40;
        Player.GetComponent<SC_FPSController>().runningSpeed = 60;
        SpeedUI.GetComponent<Image>().color=new Color(1,1,1,1);
        yield return new WaitForSeconds(15.0f);
        SpeedUI.GetComponent<Image>().color=new Color(1,1,1,0.4f);
        Player.GetComponent<SC_FPSController>().walkingSpeed = 20;
        Player.GetComponent<SC_FPSController>().runningSpeed = 30;
    }

    IEnumerator StopTime()
    {
        Bridge.GetComponent<Bridge>().StopTime = true;
        yield return new WaitForSeconds(15.0f);
        Bridge.GetComponent<Bridge>().StopTime = false;
    }

    IEnumerator IncreaseDamage()
    {
        AttackDamage = 2;
        DamageUI.GetComponent<Image>().color=new Color(1,1,1,1);
        yield return new WaitForSeconds(15.0f);
        DamageUI.GetComponent<Image>().color=new Color(1,1,1,0.4f);
        AttackDamage = 1;

    }

    IEnumerator FindWarningText()
    {
       WarningUI=GameObject.FindGameObjectsWithTag("WT")[0];
        yield return new WaitForSeconds(1.0f);
        FindWT=true;
        WarningUI.SetActive(false);
         

    }

    IEnumerator Wait()
    {
       waiting=false;
       yield return new WaitForSeconds(0.5f);
       waiting=true;  

    }

    [PunRPC]
    void RPC_StopTime()
    {
         
    // PhotonNetwork.Destroy(Hit.gameObject);
    StartCoroutine(StopTime());
                 
    }

    [PunRPC]
    void RPC_BuildBridge(int currentWood, int currentStone)
    {
    // Debug.Log(currentWood);
    // Debug.Log(currentStone);
    Bridge.GetComponent<Bridge>().currentWood=Bridge.GetComponent<Bridge>().currentWood+currentWood;
    Bridge.GetComponent<Bridge>().currentStone=Bridge.GetComponent<Bridge>().currentStone+currentStone;
                    
    Bridge.GetComponent<Bridge>().Phase();
                 
    }

    // [PunRPC]
    // void RPC_GetStone(GameObject Hit)
    // {
         
    // PhotonNetwork.Destroy(Hit);
    // SpawnMaterial.count1--;
                 
    // }

    // [PunRPC]
    // void RPC_GetWood(GameObject Hit)
    // {
         
    // PhotonNetwork.Destroy(Hit);
    // SpawnMaterial.count2--;
                 
    // }

    // [PunRPC]
    // void RPC_IncreaseDamage(GameObject Hit)
    // {
         
    // PhotonNetwork.Destroy(Hit.gameObject);
    
                 
    // }

    // [PunRPC]
    // void RPC_IncreaseSpeed(GameObject Hit)
    // {
         
    // PhotonNetwork.Destroy(Hit.gameObject);
    
                 
    // }



}
