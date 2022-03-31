using UnityEngine;
using Utility;

public class Key : MonoBehaviour, IObjective
{
    [HideInInspector] public static int keysCollected;

    /*public void Destroy()
    {
        Destroy(gameObject);
        keysCollected++;
    }*/
    
    public void Collect()
    {
        Destroy(gameObject);
    }
}
