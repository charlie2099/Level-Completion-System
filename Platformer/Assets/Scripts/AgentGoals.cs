using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentGoals : MonoBehaviour
{
    enum Goal { ALL_KEYS, ALL_COLLECTIBLES, ALL_ITEMS, COMPLETE_LEVEL };
    [SerializeField] private Goal current_goal;

    [Header("Goal Sequence")]
    [SerializeField] private List<Transform> goal_item = new List<Transform>(); // queue? So does goals in order. First in, first out.
    private int current_goal_index = 0;

    private void Start()
    {
        switch (current_goal)
        {
            case Goal.ALL_KEYS:
                // Invoke event 
                break;
            case Goal.ALL_COLLECTIBLES:
                // Invoke event 
                break;
            case Goal.ALL_ITEMS:
                // Invoke event
                break;
            case Goal.COMPLETE_LEVEL:
                // Invoke event
                break;
        }
    }

    private void Update()
    {
        if(goal_item[current_goal_index] == null)
        {
            // update agent destination target
            // update agent goal
            current_goal++;
            print("Current goal index: " + current_goal_index);
        }
    }

    private void UpdateGoal(Goal goal)  // invoke an event whenever there is a change in goal? 
    {
        current_goal = goal;
    }
}
