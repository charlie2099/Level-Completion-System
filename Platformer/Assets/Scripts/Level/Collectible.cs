using UnityEngine;
using Utility;

public class Collectible : MonoBehaviour, IObjective
{
    public void Collect()
    {
        Destroy(gameObject);
    }
}
