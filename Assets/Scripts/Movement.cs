using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        Vector2Int moveToDirection = Vector2Int.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("up" + transform.position);
            moveToDirection.y += 1;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("down");
            moveToDirection.y -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("left");
            moveToDirection.x -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("right");
            moveToDirection.x += 1;
        }
        if (moveToDirection != Vector2.zero)
        {
            Vector2 newPos = new Vector2(transform.position.x, transform.position.y);
            newPos += moveToDirection;
            transform.position = newPos;
        }
    }
}
