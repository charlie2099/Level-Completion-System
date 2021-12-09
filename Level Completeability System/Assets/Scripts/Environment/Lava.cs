using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lava : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            Destroy(col.gameObject);
            SceneManager.LoadScene("Game");
            Key.collected = 0;
            Jewel.collected = 0;
        }
    }
}
