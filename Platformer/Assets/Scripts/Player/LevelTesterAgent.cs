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
        if (_activeGoal < goalsList.Count)
        {
            print("Goals List count: " + goalsList.Count);
            // For each sub-goal in the active goal
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
        
        print("Main: " + _activeGoal + " Sub: " + _activeSubGoal);
        //print("Destination: " + goalsList[_activeGoal].subGoals[_activeSubGoal].goal.transform.gameObject.name);
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
        //_activeSubGoal = 0; // reset sub goals count for each new goal
        //print("<color=cyan>Goal index: </color>" + _activeGoal);
    }
    
    private void IncrementSubGoalIndex(EventParam eventParam) 
    {
        if (_activeSubGoal < goalsList[_activeGoal].subGoals.Count)
        {
            _activeSubGoal++;
        }
        else
        {
            _activeSubGoal = 0;
        }
        
        /*else if(_activeGoal < goalsList.Count)
        {
            _activeSubGoal = 0;
        }*/
        //print("<color=magenta>Sub-goal index: </color>" + _activeSubGoal);
    }
}