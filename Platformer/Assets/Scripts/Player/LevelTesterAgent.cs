using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Utility;

public class LevelTesterAgent : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float speed = 3.5f;
    [Header("Goal Sequence")]
    public List<Goal> goalsList = new List<Goal>();

    private NavMeshAgent _agent;
    private Transform _target;
    [HideInInspector] public int _activeGoal;
    [HideInInspector] public int _activeSubGoal;
    [HideInInspector] public int goalsCompleted;
    [HideInInspector] public int subGoalsCompleted;
    [HideInInspector] public bool _allGoalsComplete;
    private bool _errorFound;

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
        InvokeRepeating(nameof(CheckIfAgentIsObstructed), 1.0f, 3.0f);
    }

    private void Update()
    {
        if (_activeGoal < goalsList.Count)
        {
            //print("Goals List count: " + goalsList.Count);
            //print("Sub-goals count: " + goalsList[_activeGoal].subGoals.Count);
            
            // If the active goals sub-goals are incomplete, set destination to the sub-goal destination
            if (!goalsList[_activeGoal].subGoals[_activeSubGoal].isCompleted)
            {
                _agent.SetDestination(goalsList[_activeGoal].subGoals[_activeSubGoal].goal.transform.position);
                /*var path = goalsList[_activeGoal].subGoals[_activeSubGoal].goal.transform.position;
                if (AgentCanReachPosition(path))
                {
                    _agent.SetDestination(path);
                }
                else
                {
                    print("Agent can't reach goal");
                }*/
                //print("<color=lime> Goal: </color>" + goalsList[_activeGoal].name + "<color=cyan> Sub-goal: </color>" + goalsList[_activeGoal].subGoals[_activeSubGoal].goal.name);
            }
        }

        if (AllGoalsComplete() && !_allGoalsComplete)
        {
            EditorUtility.DisplayDialog("Level Completion System Logger", "Level is completeable!", "Ok");
            EditorApplication.isPlaying = false;
            _allGoalsComplete = true;
        }
    }
    
    private void OnCollisionEnter(Collision col)
    {
        IGoal goal = col.gameObject.GetComponent<IGoal>();
        if (goal != null)
        {
            goalsList[_activeGoal].subGoals[_activeSubGoal].isCompleted = true;
            goal.Complete();
        }
    }

    private bool AllGoalsComplete()
    {
        for (int i = 0; i < goalsList.Count; ++i) 
        {
            if (goalsList[i].isCompleted == false)
            {
                return false;
            }
        }
        return true;
    }

    private void CheckIfAgentIsObstructed()
    {
        if (_agent.velocity.magnitude < 0.05f && !_errorFound)
        {
            /*EditorUtility.DisplayDialog("Level Completion System Logger", 
                "[ERROR]: \n" +
                "Agent cannot reach path, path may be obstructed. \n\n" + 
                "Current [GOAL]: " + goalsList[_activeGoal].name + "\n" +
                "Current [SUB-GOAL]: " + goalsList[_activeGoal].subGoals[_activeSubGoal].goal.name + "\n\n" +
                "Suggested fix: ", "Ok");*/
            
            EditorUtility.DisplayDialog("Level Completion System Logger", 
                "[ERROR]: \n" +
                "Agent cannot reach path, path may be obstructed. \n\n" + 
                "Current [GOAL]: " + goalsList[_activeGoal].name + "\n" +
                "Current [SUB-GOAL]: " + goalsList[_activeGoal].subGoals[_activeSubGoal].goal.name + "\n\n" +
                "Suggested fix: \n" + goalsList[_activeGoal].subGoals[_activeSubGoal].goal.GetComponent<ErrorLog>().solutionText,
                "Ok");
            
            LevelDebugger.Instance.WriteToFile();

                //Debug.Break();
            EditorApplication.isPlaying = false;
            _errorFound = true;
        }
    }

    private bool AgentCanReachPosition(Vector3 position)
    {
        NavMeshPath path = new NavMeshPath();
        _agent.CalculatePath(position, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }

    private void IncrementGoalIndex(EventParam eventParam) 
    {
        _activeGoal++;
        goalsCompleted += _activeGoal;
        //_activeSubGoal = 0; // reset sub goals count for each new goal
        //print("<color=cyan>Goal index: </color>" + _activeGoal);
    }
    
    private void IncrementSubGoalIndex(EventParam eventParam) 
    {
        subGoalsCompleted ++;
        
        if (_activeSubGoal < goalsList[_activeGoal].subGoals.Count)
        {
            _activeSubGoal++;
        }

        else if (_activeSubGoal >= goalsList[_activeGoal].subGoals.Count)
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