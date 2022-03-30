using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentAI : MonoBehaviour
{
    [Header("Customisation")]
    [SerializeField] private float speed = 3.5f;
    [Header("Goal Sequence")]
    [SerializeField] private List<Transform> goalItem = new List<Transform>();

    private NavMeshAgent _agent;
    private Transform _target;
    private int _goalIndex;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _target = goalItem[_goalIndex];
    }

    private void Start()
    {
        _agent.speed = speed;
        print("<color=cyan>Total Goals: </color>" + goalItem.Count);
    }

    private void Update()
    {
        print("<color=orange>Current Goal: </color>" + goalItem[_goalIndex]);
        if (_goalIndex >= goalItem.Count-1 && _agent.remainingDistance < 0.1f)
        {
            print("<color=lime>Level Complete!</color>");
        }
        
        if (_target == null && _goalIndex >= goalItem.Count) return;
        
        _agent.destination = goalItem[_goalIndex].position;
    }

    private void OnCollisionEnter(Collision col)
    {
        IDestroyable hit = col.gameObject.GetComponent<IDestroyable>();
        if(hit != null)
        {
            hit.Destroy();
            _goalIndex++;
        }
    }
}
