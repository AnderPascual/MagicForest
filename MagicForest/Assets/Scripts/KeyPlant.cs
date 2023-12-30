using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPlant : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceKeyPlant;
    private SpriteRenderer plantRenderer;
    private bool collected = false;

    private void Start()
    {
        plantRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected == false && collision.CompareTag("Player"))
        {
            audioSourceKeyPlant.Play();
            collected = true;
            plantRenderer.enabled = false;
            Destroy(gameObject, audioSourceKeyPlant.clip.length);
            FindObjectOfType<GameManager>().GetKey();
        }
    }
}
