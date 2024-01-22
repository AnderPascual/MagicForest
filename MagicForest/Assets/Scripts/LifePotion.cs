using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePotion : MonoBehaviour
{
    [SerializeField] AudioSource audioSourceLife;
    private SpriteRenderer potionRenderer;
    private bool collected = false;
    // Start is called before the first frame update
    void Start()
    {
        potionRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        


    }
    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (!collected && elOtro.CompareTag("Player")){
            //Debug.Log("tocat");
            audioSourceLife.Play();
            collected = true;
            potionRenderer.enabled = false;
            Destroy(this.gameObject, audioSourceLife.clip.length);
            FindObjectOfType<GameManager>().IncrementLife();
           

        }
    }
}
