using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles each goal defined by the player, checking if it's sub-goals or parent goals
/// have been completed. 
/// </summary>

public abstract class GoalManager : MonoBehaviour
{
    //private List<Goal> _goals = new List<Goal>();

    private void Start()
    {
        //_goals = GameObject.FindWithTag("Player").GetComponent<Lev>();
    }
}
