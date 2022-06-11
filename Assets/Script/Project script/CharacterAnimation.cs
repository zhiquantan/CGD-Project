using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;
    public float speed;
    public GameObject FPS;

    void Start()
    {
        animator=GetComponent<Animator>();
    }
    
    void Update()
    {
        // Debug.Log(FPS.GetComponent<SC_FPSController>().curSpeedY);
        
        if(FPS.GetComponent<SC_FPSController>().curSpeedX==0&&FPS.GetComponent<SC_FPSController>().curSpeedY==0)
        {
            speed=0;
        }

        else{
            speed=1;
        }

        animator.SetFloat("Speed",speed);

        
    }
}
