using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour, IDestroyable
{
    private Goal _goal;
    private NavMeshObstacle _navMeshObstacle;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _goal = GetComponent<Goal>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (_goal.isCompleted)
        {
            _navMeshObstacle.enabled = false;
            _meshRenderer.enabled = false;
            //Destroy();
        }
        
        /*if(Key.keysCollected == 2)
        {
            Destroy(gameObject);
        }*/
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