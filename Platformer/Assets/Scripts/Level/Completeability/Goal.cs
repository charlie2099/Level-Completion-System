using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;

namespace Level.Completeability
{
    public class Goal : MonoBehaviour, IGoal
    {
        public List<SubGoal> subGoalsList = new List<SubGoal>();
        public bool isCompleted;

        private void Update()
        {
            for (int i = 0; i < subGoalsList.Count; i++)
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
            return subGoalsList.All(subGoal => subGoal.isCompleted);
        }
    
        public void Complete()
        {
            Destroy(gameObject);
        }
    }
}
