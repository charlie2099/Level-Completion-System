using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletion : MonoBehaviour
{
    public static LevelCompletion Instance;
    
    [Serializable]
    private class CompletionConditions
    {
        public int keysCollected;
        public int doorsOpened;
    }

    [SerializeField] private CompletionConditions conditions;

    private void Awake()
    {
        Instance = this;
    }
}
