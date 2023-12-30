using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private AudioSource audioSourceDead;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce = 600;
    private Rigidbody2D playerRb;
    private SpriteRenderer playerRenderer;
    
    private float bulletTimer = 0;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        bulletTimer += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Collision"))
        {
            Dead();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            FindObjectOfType<GameManager>().LevelCompleted();
        }
        else if (collision.CompareTag("EnemyBullet"))
        {
            Dead();
        }
    }

    public void Dead()
    {
        if (!GameManager.Instance.playerIsDead)
        {
            playerRenderer.color = Color.grey;
            audioSourceDead.Play();
            FindObjectOfType<GameManager>().DecreaseLife();
        }
    }

    public void ShootBullet()
    {
        if (bulletTimer > 0.3f)
        {
            int bulletDirection = 1;
            if (playerRenderer.flipX == true)
            {
                bulletDirection = -1;
            }
            GameObject bulletCopy = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bulletCopy.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletDirection, 1) * bulletForce);
            Destroy(bulletCopy, 2);
            bulletTimer = 0;
        }
        
    }

}
