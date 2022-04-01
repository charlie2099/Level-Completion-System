using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelDebugger : MonoBehaviour
{
    public static LevelDebugger Instance; 
    /*private class ErrorLogData
    {
        public int      ObjectivesCompleted;
        public string[] CollisionIssues;
        public string[] FallingIssues;
        public string[] TimeToCompleteIssues;
        public string CompletionStatus;
    }*/
    
    private class ErrorLogData
    {
        public string ActiveGoal;
        public string ActiveSubGoal;
        public string Error;
        public string Solution;
        public string CompletionStatus;
    }

    [SerializeField] private LevelTesterAgent agent;
    [SerializeField] private string filepath;
    [SerializeField] private Text goalText;
    [SerializeField] private Text subGoalText;
    [SerializeField] private Text goalsCompletedText;
    [SerializeField] private Text subGoalsCompletedText;
    private string _levelData;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (agent.activeGoal < agent.goalsList.Count)
        {
            if (agent.goalsList[agent.activeGoal] != null)
            {
                goalText.text = "Goal: <color=lime>" + agent.goalsList[agent.activeGoal].name + "</color>";
            }
        
            if (agent.goalsList[agent.activeGoal].subGoals[agent.activeSubGoal].goal != null)
            {
                subGoalText.text = "Sub-Goal: <color=cyan>" + agent.goalsList[agent.activeGoal].subGoals[agent.activeSubGoal].goal.name + "</color>";
            }
        }

        goalsCompletedText.text    = "Goals completed: <color=lime>" + agent.goalsCompleted + "</color>";
        subGoalsCompletedText.text = "Sub-Goals completed: <color=cyan>" + agent.subGoalsCompleted + "</color>";
    }

    public void WriteToFile()
    {
        ErrorLogData errorLogData = new ErrorLogData();
        errorLogData.ActiveGoal       = agent.goalsList[agent.activeGoal].name;
        errorLogData.ActiveSubGoal    = agent.goalsList[agent.activeGoal].subGoals[agent.activeSubGoal].goal.name;
        errorLogData.Error            = "Agent's path may be obstructed";
        errorLogData.Solution         = agent.goalsList[agent.activeGoal].subGoals[agent.activeSubGoal].goal.GetComponent<ErrorLog>().solutionText;
        errorLogData.CompletionStatus = "Level Is Not Completable";

        _levelData = JsonUtility.ToJson(errorLogData, true);

        //string path = Application.persistentDataPath + filepath + "/ErrorLog.json";
        string path = Application.dataPath + filepath + "/ErrorLog.json";
        File.WriteAllText(path, _levelData);

        
        
        
        /*ErrorLogData errorLogData = new ErrorLogData();
        errorLogData.CollisionIssues = new string[1];
        errorLogData.FallingIssues = new string[1];
        errorLogData.TimeToCompleteIssues = new string[1];

        errorLogData.ObjectivesCompleted     = 0;
        errorLogData.CollisionIssues[0]      = "No Issues Found!";
        errorLogData.FallingIssues[0]        = "No Issues Found!";
        errorLogData.TimeToCompleteIssues[0] = "No Issues Found!";
        errorLogData.CompletionStatus        = "Level Completed";

        _levelData = JsonUtility.ToJson(errorLogData, true);

        //string path = Application.persistentDataPath + filepath + "/ErrorLog.json";
        string path = Application.dataPath + filepath + "/ErrorLog.json";
        File.WriteAllText(path, _levelData);*/
    }
}
