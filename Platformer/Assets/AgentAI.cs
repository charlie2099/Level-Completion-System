using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentAI : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 3.5f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.speed = speed;
    }

    private void Update()
    {
        if (target == null) return;
        agent.destination = target.position;
    }

    private void OnCollisionEnter(Collision col)
    {
        IDestroyable hit = col.gameObject.GetComponent<IDestroyable>();
        if(hit != null)
        {
            hit.Destroy();
        }
    }
}
