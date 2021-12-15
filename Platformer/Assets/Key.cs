using UnityEngine;

public class Key : MonoBehaviour, IDestroyable
{
    [HideInInspector] public static int keysCollected;

    public void Destroy()
    {
        Destroy(gameObject);
        keysCollected++;
    }
}
