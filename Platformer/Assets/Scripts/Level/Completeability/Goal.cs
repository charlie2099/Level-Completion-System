using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isCompleted;
    [SerializeField] private List<SubGoal> subGoals = new List<SubGoal>();
}
