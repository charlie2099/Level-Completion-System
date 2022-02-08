using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void Update()
    {
        if(Key.keysCollected == 2)
        {
            Destroy(gameObject);
        }
    }
}
