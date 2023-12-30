using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyLimit : MonoBehaviour
{
    public static int enemyDirection = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Foreground"))
        {
            enemyDirection = -enemyDirection;
        }
    }
}
