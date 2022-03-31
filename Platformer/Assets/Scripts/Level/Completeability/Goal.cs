using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Goal : MonoBehaviour
{
    public List<SubGoal> subGoals = new List<SubGoal>();
    public bool isCompleted;

    private void Update()
    {
        for (int i = 0; i < subGoals.Count; i++)
        {
            if (IsAllSubGoalsComplete() && !isCompleted)
            {
                EventManager.TriggerEvent("GoalCompleted", new EventParam());
                isCompleted = true;
            }
        }
    }
    
    private bool IsAllSubGoalsComplete()
    {
        for (int i = 0; i < subGoals.Count; ++i) 
        {
            if (subGoals[i].isCompleted == false)
            {
                return false;
            }
        }
 
        return true;
    }
}
