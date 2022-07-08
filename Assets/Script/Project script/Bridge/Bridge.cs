using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bridge : MonoBehaviour
{
    public static float  HpPerSecond=0.01f;
    public Image BridgeLife;
    public GameObject BridgeLifeUI;
    public int currentWood;
    public int currentStone;
    public static string difficulty;
    public int requireWood;
    public int requireStone;
    public int CurrentPhase;
    public int time;
    public GameObject Phase1;
    public GameObject Phase2;
    public GameObject Phase3;
    public Text currentStoneUI;
    public Text currentWoodUI;
    public Text RequireStoneUI;
    public Text RequiretWoodUI;
    public Text CurrentPhaseUI;
    public Text TimeUI;
    public bool StopTime=false;
    public GameObject portal;
    public GameObject portal2;
    // Start is called before the first frame update
    private void Start()
    {
        if(difficulty=="easy")
        {
            requireWood=10;
            requireStone=10;
        }

        else if(difficulty=="normal")
        {
            requireWood=15;
            requireStone=15;
        }

        else if(difficulty=="hard")
        {
            requireWood=20;
            requireStone=20;
        }
        
        CurrentPhase=0;
        InvokeRepeating("HpReduce", 0, 1f);
         InvokeRepeating("Time", 0, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        currentStoneUI.text=currentStone.ToString();
        currentWoodUI.text=currentWood.ToString();
        RequireStoneUI.text="/"+requireStone.ToString();
        RequiretWoodUI.text="/"+requireWood.ToString();
       

        if(BridgeLife.fillAmount==0)
        {
            if(CurrentPhase==1)
            {
                CurrentPhase=0;
                CurrentPhaseUI.text="Phase 0";
                requireStone=requireStone-5;
                requireWood=requireWood-5;
                Phase1.SetActive(false);
                Phase2.SetActive(false);
                Phase3.SetActive(false);
            }

            else if(CurrentPhase==2)
            {
                CurrentPhase=1;
                CurrentPhaseUI.text="Phase 1";
                requireStone=requireStone-5;
                requireWood=requireWood-5;
                Phase2.SetActive(false);
                Phase3.SetActive(false);
            }
        }
    }

    public void HpReduce()
    {
        if(CurrentPhase!=0&&CurrentPhase!=3)
        {
            BridgeLife.fillAmount=BridgeLife.fillAmount-HpPerSecond;
        }
    }

    public void Time()
    {
        if(!StopTime)
        {
            time++;
            TimeUI.text=time.ToString();
        }
        
    }

    public void GoBackPreivousPhase()
    {
        
    }

    public void Phase()
    {
        if(currentWood>=requireWood&&currentStone>=requireStone)
        {
            if(CurrentPhase==0)
            {
                Debug.Log("Checking");
                Phase1.SetActive(true);
                BridgeLifeUI.SetActive(true);
                BridgeLife.fillAmount=1;
                CurrentPhase=1;
                currentWood=0;
                currentStone=0;
                requireStone=requireStone+5;
                requireWood=requireWood+5;
                CurrentPhaseUI.text="Phase 1";
                
            }

            else if(CurrentPhase==1)
            {
                BridgeLife.fillAmount=1;
                Phase1.SetActive(true);
                Phase2.SetActive(true);
                CurrentPhase=2;
                currentWood=0;
                currentStone=0;
                requireStone=requireStone+5;
                requireWood=requireWood+5;
                CurrentPhaseUI.text="Phase 2";
                 
            }

            else if(CurrentPhase==2)
            {
                BridgeLife.fillAmount=1;
                Phase1.SetActive(true);
                Phase2.SetActive(true);
                Phase3.SetActive(true);
                CurrentPhaseUI.text="Finished";
                CurrentPhase=3;
                portal.SetActive(false);
                portal2.SetActive(true);
               
            }
        }
    }
}
