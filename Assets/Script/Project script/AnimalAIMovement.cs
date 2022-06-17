using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAIMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject goal;
    
    // Start is called before the first frame update
    void Start()
    {
        goal = GameObject.FindGameObjectsWithTag("CollidePoint")[0];
        agent=GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        agent.destination=goal.transform.position;
    }
}
