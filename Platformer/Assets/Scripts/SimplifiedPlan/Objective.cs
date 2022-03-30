using UnityEngine;

public class Objective : MonoBehaviour
{
    public Objective parentObjective;
    public Objective[] childObjectives;

    [SerializeField] private bool completed = false;

    public void CompleteObjective()
    {
        completed = true;
    }

    public bool IsCompleted()
    {
        return completed;
    }

    public bool HasParentObjective()
    {
        if (parentObjective != null)
        {
            return true;
        }
        return false;
    }

    public Objective GetParentObjective()
    {
        return parentObjective;
    }

    public bool HasChildObjectives()
    {
        if (childObjectives != null)
        {
            return true;
        }
        return false;
    }

    public Objective[] GetChildObjectives()
    {
        return childObjectives;
    }
}

