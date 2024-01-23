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
    private bool canDoubleJump;
    private bool canJumpLeft = true;
    private bool canJumpRight = true;
    private float timeWallJump = 0.5f;
    private float timerWall = 0;
    private float wallSlidingSpeed = 2f;
    

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timerWall = timerWall + Time.deltaTime;

        //Jump
        if (Input.GetKeyDown("space") && !GameManager.Instance.playerIsDead)
        {
            Jump();
        }

        //WallSlide
        if (CheckRightWall.isOnWall || CheckLeftWall.isOnWall)
        {
            WallSlide();
        }

        //Move
        inputH = Input.GetAxisRaw("Horizontal");
        if (timerWall > timeWallJump && !GameManager.Instance.playerIsDead)
        {
            Move();
        }
        
        //Jump animation on air
        if (!CheckGround.isGrounded && !GameManager.Instance.playerIsDead)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Walk", false);
        }

        if (GameManager.Instance.playerIsDead)
        {
            playerRb.velocity = Vector2.zero;
        }

        //Deactivate jump anim when grounded
        if (CheckGround.isGrounded)
        {
            animator.SetBool("Jump", false);
            canDoubleJump = true;
            canJumpRight = true;
            canJumpLeft = true;
            //Debug.Log("Ground");
        }
        //Shift to shoot
        if (Input.GetKeyDown(KeyCode.LeftShift) && GameManager.level > 1 && !GameManager.Instance.playerIsDead)
        {
            FindObjectOfType<PlayerController>().ShootBullet();
        }
    }

    private void Move()
    {
        if (inputH > 0 && !CheckRightWall.isOnWall)
        {
            playerRenderer.flipX = false;
            animator.SetBool("Walk", true);
            playerRb.velocity = new Vector2(inputH * runSpeed, playerRb.velocity.y);
        }
        else if (inputH < 0 && !CheckLeftWall.isOnWall)
        {
            playerRenderer.flipX = true;
            animator.SetBool("Walk", true);
            playerRb.velocity = new Vector2(inputH * runSpeed, playerRb.velocity.y);
        }
        else if (inputH == 0)
        {
            animator.SetBool("Walk", false);
            playerRb.velocity = new Vector2(inputH * runSpeed, playerRb.velocity.y);
        }
        if (CheckLeftWall.isObstacle)
        {
            animator.SetBool("Walk", false);
        }
        else if (CheckRightWall.isObstacle)
        {
            animator.SetBool("Walk", false);
        }

    }
    private void Jump()
    {
        //NormalJump
        if (CheckGround.isGrounded)
        {
            
            audioSourceJump.Play();
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("Walk", false);
            canJumpLeft = true;
            canJumpRight = true;
        
        }
        //JumpLeft
        else if (!CheckGround.isGrounded && CheckLeftWall.isOnWall && canJumpLeft)
        {
            audioSourceJump.Play();
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
            playerRb.AddForce(Vector2.one * jumpForce, ForceMode2D.Impulse);
            playerRenderer.flipX = false;
            timerWall = 0;
            canJumpLeft = false;
            canJumpRight = true;
        }
        else if (!CheckGround.isGrounded && CheckRightWall.isOnWall && canJumpRight)
        {
            audioSourceJump.Play();
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
            playerRb.AddForce(new Vector2(-1, 1) * jumpForce, ForceMode2D.Impulse);
            playerRenderer.flipX = true;
            timerWall = 0;
            canJumpRight = false;
            canJumpLeft = true;
        }
        //DoubleJump
        else if (!CheckGround.isGrounded && !CheckRightWall.isOnWall && !CheckLeftWall.isOnWall && canDoubleJump && GameManager.level > 1)
        {
            audioSourceJump.Play();
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canDoubleJump = false;
        }
    }

    private void WallSlide()
    {
        playerRb.velocity = new Vector2(playerRb.velocity.x, Mathf.Clamp(playerRb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        if (CheckRightWall.isOnWall)
        {
            playerRenderer.flipX = true;
            
        }
        else if (CheckLeftWall.isOnWall)
        {
            playerRenderer.flipX = false;
        }
        canDoubleJump = true;
    }
}
