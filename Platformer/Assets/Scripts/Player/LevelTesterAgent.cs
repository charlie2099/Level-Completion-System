using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utility;

public class LevelTesterAgent : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float speed = 3.5f;
    [Header("Goal Sequence")]
    [SerializeField] private List<Goal> goalsList = new List<Goal>();

    private NavMeshAgent _agent;
    private Transform _target;
    private int _activeGoal;
    private int _activeSubGoal;

    private void OnEnable()
    {
        EventManager.StartListening("GoalCompleted", IncrementGoalIndex);
        EventManager.StartListening("SubGoalCompleted", IncrementSubGoalIndex);
    }

    private void OnDisable()
    {
        EventManager.StopListening("GoalCompleted", IncrementGoalIndex);
        EventManager.StopListening("SubGoalCompleted", IncrementSubGoalIndex);
    }

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
        print("Active goal index: " + _activeGoal);
        print("Sub-goal index: " + _activeSubGoal);
        
        if (_activeGoal < goalsList.Count)
        {
            // For each sub-goal in the active goal, print it
            for (int i = 0; i < goalsList[_activeGoal].subGoals.Count; i++)
            {
                // If the active goals sub-goals are incomplete, 
                if (!goalsList[_activeGoal].subGoals[_activeSubGoal].isCompleted)
                {
                    //_agent.SetDestination(goalsList[_activeGoal].subGoals[_activeSubGoal].subGoalPos.position);
                    _agent.SetDestination(goalsList[_activeGoal].subGoals[_activeSubGoal].goal.transform.position);
                }
            }
        }

    }

    private void OnCollisionEnter(Collision col)
    {
        IObjective objective = col.gameObject.GetComponent<IObjective>();
        if (objective != null)
        {
            goalsList[_activeGoal].subGoals[_activeSubGoal].isCompleted = true;
            objective.Collect();
        }
    }
    
    private void IncrementGoalIndex(EventParam eventParam) 
    {
        _activeGoal++;
        _activeSubGoal = 0; // reset sub goals count for each new goal
    }
    
    private void IncrementSubGoalIndex(EventParam eventParam) 
    {
        _activeSubGoal++;
    }
}