using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private float speed = 3.0f;
    private bool moveRight = true;

    private void Update()
    {
        if (transform.position.x > 1.0f)
        {
            moveRight = false;
        }
        if (transform.position.x < -1.0f)
            moveRight = true;

        if (moveRight)
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }
}
