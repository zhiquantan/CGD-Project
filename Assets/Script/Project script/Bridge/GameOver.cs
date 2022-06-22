using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverUIFinder;
    public GameObject Bridge;
    public GameObject[] Player;

    void Start()
    {
        StartCoroutine(FindGameOverUI());
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {   
             GameOverUIFinder.GetComponent<GameOverUIFinder>().enabled=true;
             Bridge.GetComponent<Bridge>().StopTime=true;
             Bridge.GetComponent<Bridge>().enabled=false;
             Cursor.visible = true;
             Cursor.lockState = CursorLockMode.Confined;

             for(int i=0;i<Player.Length;i++)
             {
                Player[i].GetComponent<SC_FPSController>().enabled=false;
             }
             
        }
       
    }

    IEnumerator FindGameOverUI()
    {
       
        yield return new WaitForSeconds(1.0f);
        GameOverUIFinder= GameObject.FindGameObjectsWithTag("GameOverUIFinder")[0];
        Player=GameObject.FindGameObjectsWithTag("Player");
    }

}
