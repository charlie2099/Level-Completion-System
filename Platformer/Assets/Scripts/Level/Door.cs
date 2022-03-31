using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    private Goal _goal;
    private NavMeshObstacle _navMeshObstacle;
    private MeshRenderer _meshRenderer;
    private BoxCollider _collider;
 
    private void Awake()
    {
        _goal = GetComponent<Goal>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (_goal.isCompleted)
        {
            _navMeshObstacle.enabled = false;
            _meshRenderer.enabled = false;
            _collider.enabled = false;
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
}
