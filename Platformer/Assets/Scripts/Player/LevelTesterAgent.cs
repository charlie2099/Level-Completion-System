using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelTesterAgent : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float speed = 3.5f;
    [Header("Goal Sequence")]
    [SerializeField] private List<Goal> goals = new List<Goal>();

    private NavMeshAgent _agent;
    private Transform _target;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _agent.speed = speed;
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        /*IDestroyable hit = col.gameObject.GetComponent<IDestroyable>();
        if(hit != null)
        {
            hit.Destroy();
        }*/
    }
}