using UnityEngine;

public class Collectible : MonoBehaviour, IDestroyable
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
