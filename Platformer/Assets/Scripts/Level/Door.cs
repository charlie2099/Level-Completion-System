using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Door : MonoBehaviour, IDestroyable
{
    private void Update()
    {
        if(Key.keysCollected == 2)
        {
            Destroy(gameObject);
        }
    }

    /*private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Agent"))
        {
            if(Key.keysCollected == keysRequired)
            {
                Destroy(gameObject);
            }
        }
    }*/
    
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
