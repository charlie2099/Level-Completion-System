using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SubGoal : MonoBehaviour
{
    public Goal goal;
    public int cost;
    public bool isCompleted;
    private bool _oneTimeCheck;

    private void Update()
    {
        if (isCompleted && !_oneTimeCheck)
        {
            EventManager.TriggerEvent("SubGoalCompleted", new EventParam());
            //print("<color=cyan> Sub-Goal Completed </color>");
            _oneTimeCheck = true;
        }
    }
}
