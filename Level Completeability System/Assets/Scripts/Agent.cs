using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class Agent : MonoBehaviour
{
    NavMeshAgent agent;
   
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Door"))
        {
            Debug.Log("DumbleDOOR");
        }
    }

 
    void Update()
    {
        //agent.SetDestination()
    }
}
