using System;
using System.Collections;
using System.Collections.Generic;
using Level.Completeability;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Utility;

public class LevelTesterAgent : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float speed = 3.5f;
    
    [Header("Goal Sequence")]
    public List<Goal> goalsList = new List<Goal>();
    
    [Header("GUI")] 
    [SerializeField] private GameObject levelDebugDialogGui;
    [SerializeField] private Text titleDialogText;
    [SerializeField] private Text levelCompleteableDialogText;
    [SerializeField] private Text errorDialogText;

    [HideInInspector] public int activeGoal;
    [HideInInspector] public int activeSubGoal;
    [HideInInspector] public int goalsCompleted;
    [HideInInspector] public int subGoalsCompleted;
    [HideInInspector] public bool allGoalsComplete;

    private NavMeshAgent _agent;
    //private LineRenderer _lineRenderer;
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
        //_lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        levelDebugDialogGui.GetComponent<Image>().enabled = false;
        titleDialogText.gameObject.SetActive(false);
        levelCompleteableDialogText.gameObject.SetActive(false);
        errorDialogText.gameObject.SetActive(false);
        
        _agent.speed = speed;
        InvokeRepeating(nameof(CheckIfAgentIsObstructed), 1.0f, 3.0f);
        
        /*NavMeshPath path = new NavMeshPath();
        _agent.CalculatePath(goalsList[activeGoal].subGoals[activeSubGoal].goal.transform.position, path);
        if (path.status == NavMeshPathStatus.PathPartial) 
        {
            print("Cannot reach destination");
        }
        else
        {
            print("Destination viable");
        }*/

        //StartCoroutine(GetPath());
    }
    
    /*private IEnumerator GetPath()
    {
        _lineRenderer.SetPosition(0, transform.position); 

        _agent.SetDestination(goalsList[activeGoal].subGoals[activeSubGoal].goal.transform.position);

        yield return new WaitForSeconds(1.0f); 

        DrawPath(_agent.path);

        _agent.isStopped = true;
    }

    private void DrawPath(NavMeshPath path)
    {
        if(path.corners.Length < 2) 
            return;

        _lineRenderer.SetVertexCount(path.corners.Length);

        for(var i = 1; i < path.corners.Length; i++)
        {
            _lineRenderer.SetPosition(i, path.corners[i]); 
        }
    }*/

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
        if (activeGoal < goalsList.Count) 
        {
            // If the active goals sub-goals are incomplete, set destination to the sub-goal destination
            if (!goalsList[activeGoal].subGoalsList[activeSubGoal].isCompleted) 
            {
                /*Vector3 target = goalsList[activeGoal].subGoals[activeSubGoal].goal.transform.position;
                NavMeshPath navMeshPath = new NavMeshPath();
                if (_agent.CalculatePath(target, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
                {
                    _agent.SetPath(navMeshPath);
                }
                else
                {
                    print("Fail condition!");
                }*/

                _agent.SetDestination(goalsList[activeGoal].subGoalsList[activeSubGoal].goal.transform.position);
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

        #if UNITY_EDITOR
        EditorUtility.DisplayDialog("Level Completion System Logger", "Level is completeable!", "Ok");
        #endif
        
        levelDebugDialogGui.SetActive(true);
        errorDialogText.gameObject.SetActive(false);
        
        levelDebugDialogGui.GetComponent<Image>().enabled = true;
        titleDialogText.gameObject.SetActive(true);
        levelCompleteableDialogText.gameObject.SetActive(true); 

        #if !UNITY_EDITOR
        yield return new WaitForSeconds(3.0f);
        #endif
        
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
        
        Application.Quit();
        allGoalsComplete = true;
    }
    
    private void OnCollisionEnter(Collision col)
    {
        IGoal goal = col.gameObject.GetComponent<IGoal>();
        if (goal != null)
        {
            goalsList[activeGoal].subGoalsList[activeSubGoal].isCompleted = true;
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
            #if UNITY_EDITOR
            EditorUtility.DisplayDialog("Level Completion System Logger", 
                "[ERROR]: \n" + "Agent cannot reach path, path may be obstructed. \n\n" + 
                "Current [GOAL]: " + goalsList[activeGoal].name + "\n" +
                "Current [SUB-GOAL]: " + goalsList[activeGoal].subGoalsList[activeSubGoal].goal.name + "\n\n" +
                "Suggested fix: \n" + goalsList[activeGoal].subGoalsList[activeSubGoal].goal.GetComponent<ErrorLog>().solutionText,
                "Ok");
            #endif

            LevelDebugger.Instance.WriteToFile();

            #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            #endif
            
            Application.Quit();

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
        
        if (activeSubGoal < goalsList[activeGoal].subGoalsList.Count-1)
        {
            activeSubGoal++;
        }

        else if (activeSubGoal >= goalsList[activeGoal].subGoalsList.Count-1)
        {
            activeSubGoal = 0;
        }
    }
}