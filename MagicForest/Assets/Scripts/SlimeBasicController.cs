using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class SlimeBasicController : MonoBehaviour
{
    [SerializeField] private float runSpeed = 5;
    private Rigidbody2D slimeRb;
    private SpriteRenderer slimeRenderer;
    [SerializeField] private AudioSource audioSourceSlime;

    void Start()
    {
        slimeRb = GetComponent<Rigidbody2D>();
        slimeRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!GameManager.Instance.playerIsDead)
        {
            slimeRb.velocity = new Vector2(CheckEnemyLimit.enemyDirection * runSpeed, slimeRb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            slimeRenderer.enabled = false;
            transform.position = new Vector2(0, 20);
            audioSourceSlime.Play();
            Destroy(gameObject, audioSourceSlime.clip.length);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            FindObjectOfType<PlayerController>().Dead();
        }
    }
}
