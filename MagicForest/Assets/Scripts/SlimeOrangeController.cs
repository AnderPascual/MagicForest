using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeOrangeController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceSlimeOrange;
    private SpriteRenderer slimeOrangeRenderer;

    private void Start()
    {
        slimeOrangeRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            slimeOrangeRenderer.enabled = false;
            transform.position = new Vector2(0, 20);
            audioSourceSlimeOrange.Play();
            Destroy(gameObject, audioSourceSlimeOrange.clip.length);
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
