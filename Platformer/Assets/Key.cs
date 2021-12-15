using UnityEngine;

public class Key : MonoBehaviour, IDestroyable
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
