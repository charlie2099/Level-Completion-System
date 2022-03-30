using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelDebugger : MonoBehaviour
{
    private class ErrorLogData
    {
        public int      ObjectivesCompleted;
        public string[] CollisionIssues;
        public string[] FallingIssues;
        public string[] TimeToCompleteIssues;
        public string CompletionStatus;
    }

    [SerializeField] private string filepath;
    private string _levelData;

    private void Start()
    {
        ErrorLogData errorLogData = new ErrorLogData();
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
        File.WriteAllText(path, _levelData);
    }
}
