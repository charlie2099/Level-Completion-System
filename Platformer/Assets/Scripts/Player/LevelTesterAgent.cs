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
    [HideInInspector] public int activeGoal;
    [HideInInspector] public int activeSubGoal;
    [HideInInspector] public int goalsCompleted;
    [HideInInspector] public int subGoalsCompleted;
    [HideInInspector] public bool allGoalsComplete;
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
        if (activeGoal < goalsList.Count) 
        {
            // If the active goals sub-goals are incomplete, set destination to the sub-goal destination
            if (!goalsList[activeGoal].subGoals[activeSubGoal].isCompleted) 
            {
                _agent.SetDestination(goalsList[activeGoal].subGoals[activeSubGoal].goal.transform.position);
                //print("<color=lime> Goal: </color>" + goalsList[_activeGoal].name + "<color=cyan> Sub-goal: </color>" + goalsList[_activeGoal].subGoals[_activeSubGoal].goal.name);
            }
        }

        if (AllGoalsComplete() && !allGoalsComplete)
        {
            StartCoroutine(PlayLevelCompletionDialog());
        }
    }

    private IEnumerator PlayLevelCompletionDialog()
    {
        yield return new WaitForSeconds(0.5f);
        EditorUtility.DisplayDialog("Level Completion System Logger", "Level is completeable!", "Ok");
        EditorApplication.isPlaying = false;
        allGoalsComplete = true;
    }
    
    private void OnCollisionEnter(Collision col)
    {
        IGoal goal = col.gameObject.GetComponent<IGoal>();
        if (goal != null)
        {
            goalsList[activeGoal].subGoals[activeSubGoal].isCompleted = true;
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
            EditorUtility.DisplayDialog("Level Completion System Logger", 
                "[ERROR]: \n" +
                "Agent cannot reach path, path may be obstructed. \n\n" + 
                "Current [GOAL]: " + goalsList[activeGoal].name + "\n" +
                "Current [SUB-GOAL]: " + goalsList[activeGoal].subGoals[activeSubGoal].goal.name + "\n\n" +
                "Suggested fix: \n" + goalsList[activeGoal].subGoals[activeSubGoal].goal.GetComponent<ErrorLog>().solutionText,
                "Ok");
            
            LevelDebugger.Instance.WriteToFile();

                //Debug.Break();
            EditorApplication.isPlaying = false;
            _errorFound = true;
        }
    }

    private void IncrementGoalIndex(EventParam eventParam) 
    {
        activeGoal++;
        goalsCompleted++;
    }
    
    private void IncrementSubGoalIndex(EventParam eventParam) 
    {
        subGoalsCompleted++;
        
        if (activeSubGoal < goalsList[activeGoal].subGoals.Count-1)
        {
            activeSubGoal++;
        }

        else if (activeSubGoal >= goalsList[activeGoal].subGoals.Count-1)
        {
            activeSubGoal = 0;
        }
    }
}