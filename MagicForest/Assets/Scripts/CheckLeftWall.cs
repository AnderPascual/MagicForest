using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLeftWall : MonoBehaviour
{
    public static bool isOnWall;
    public static bool isObstacle;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            isOnWall = true;
        }
        isObstacle = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            isOnWall = false;
        }
        isObstacle = false;
    }
}
