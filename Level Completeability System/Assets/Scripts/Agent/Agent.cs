using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class Agent : MonoBehaviour
{
    /*struct Objective { Task, }*/

    private enum Task { COLLECT_KEY, OPEN_DOOR };
    private Task current_task;
    
    private NavMeshAgent agent_;
    [SerializeField] private Transform target_;

    private void Start()
    {
        agent_ = GetComponent<NavMeshAgent>();
        agent_.updateRotation = false;
        agent_.updateUpAxis = false;
        current_task = Task.OPEN_DOOR;
    }

    private void Update()
    {
        if(current_task == Task.COLLECT_KEY)
        {
            // collect key
               // open door
        }

        if(current_task == Task.OPEN_DOOR)
        {
            // open door IF conditions are met (i.e. 2 keys collected, x powerup acquired) 
            if(target_ != null)
            {
                agent_.SetDestination(target_.position);
            }
        }
    }
}
