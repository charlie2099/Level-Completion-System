using UnityEngine;

namespace Level.Completeability
{
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
                _oneTimeCheck = true;
            }
        }
    }
}
