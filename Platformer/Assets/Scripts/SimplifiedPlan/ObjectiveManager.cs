using UnityEngine;
using Debug = UnityEngine.Debug;

public class ObjectiveManager : MonoBehaviour
{
    private Objective _currentObjective;

    private void Update()
    {
        Debug.Log("Current Objective: " + _currentObjective + ", Completed status = " + _currentObjective.IsCompleted());

        if (_currentObjective.IsCompleted() == false)
        {
            if (_currentObjective.HasChildObjectives())
            {
                Objective[] objectivesToSearch = _currentObjective.GetChildObjectives();
                int numberOfObjectives = objectivesToSearch.Length - 1;

                for (int i = 0; i < numberOfObjectives; i++)
                {
                    if (objectivesToSearch[i].IsCompleted() == false)
                    {
                        _currentObjective = objectivesToSearch[i];
                        break;
                    }
                }
            }
        }
        else
        {
            if (_currentObjective.HasParentObjective())
            {
                _currentObjective = _currentObjective.GetParentObjective();
            }
            else
            {
                Debug.Log("WIN");
            }
        }
    }
}

