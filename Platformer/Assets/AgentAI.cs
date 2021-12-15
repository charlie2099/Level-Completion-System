using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentAI : MonoBehaviour
{
    [Header("Customisation")]
    [SerializeField] private float speed = 3.5f;
    [Header("Goal Sequence")]
    [SerializeField] private List<Transform> goal_item = new List<Transform>();

    private NavMeshAgent agent;
    private Transform target;
    private int goal_index = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target = goal_item[goal_index].transform;
    }

    private void Start()
    {
        agent.speed = speed;
    }

    private void Update()
    {
        print("Goal index: " + goal_index);
        print("Goal list count: " + goal_item.Count);
        if (target == null && goal_index >= goal_item.Count) return;
        
        agent.destination = goal_item[goal_index].transform.position;
    }

    private void OnCollisionEnter(Collision col)
    {
        IDestroyable hit = col.gameObject.GetComponent<IDestroyable>();
        if(hit != null)
        {
            hit.Destroy();
            goal_index++;
        }
    }
}
