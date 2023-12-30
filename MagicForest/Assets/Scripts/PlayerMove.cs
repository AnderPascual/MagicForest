using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceJump;
    [SerializeField] private float runSpeed = 5;
    [SerializeField] private float jumpForce = 7.5f;
    [SerializeField] public Animator animator;

    private Rigidbody2D playerRb;
    private SpriteRenderer playerRenderer;
    private float inputH;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        if (inputH != 0 && !GameManager.Instance.playerIsDead)
        {
            playerRb.velocity = new Vector2(inputH * runSpeed, playerRb.velocity.y);
            animator.SetBool("Walk", true);
            if (inputH > 0)
            {
                playerRenderer.flipX = false;
            }
            else if (inputH < 0)
            {
                playerRenderer.flipX = true;
            }
        }
        else
        {
            playerRb.velocity = new Vector2(0, playerRb.velocity.y);
            animator.SetBool("Walk", false);
        }

        if (Input.GetKeyDown("space") && CheckGround.isGrounded && !GameManager.Instance.playerIsDead)
        {
            audioSourceJump.Play();
            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("Walk", false);
        }
        if (!CheckGround.isGrounded && !GameManager.Instance.playerIsDead)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Walk", false);
        }
        if (CheckGround.isGrounded)
        {
            animator.SetBool("Jump", false);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && GameManager.level > 1 && !GameManager.Instance.playerIsDead)
        {
            FindObjectOfType<PlayerController>().ShootBullet();
        }
    }
}
