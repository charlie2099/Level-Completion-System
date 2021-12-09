using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private int keysRequired;
    [SerializeField] private int jewelsRequired;

    private void Update()
    {
        Debug.Log("Jewels: " + Jewel.collected);
        Debug.Log("Keys: " + Key.collected);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player") && Key.collected == keysRequired/* && Jewel.collected == jewelsRequired*/)
        {
            Debug.Log("DOOR OPENS");
            SceneManager.LoadScene("Game");
            Key.collected = 0;
            Jewel.collected = 0;
        }
    }
}
