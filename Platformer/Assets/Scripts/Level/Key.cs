using UnityEngine;

namespace Level
{
    public class Key : MonoBehaviour/*, IGoal*/
    {
        [HideInInspector] public static int keysCollected;

        /*public void Destroy()
        {
        Destroy(gameObject);
        keysCollected++;
        }*/
    }
}
