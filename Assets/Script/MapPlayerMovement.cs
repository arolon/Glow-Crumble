using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Transform groundDetect;
    [SerializeField] private bool isGrounded; // Just so we can see in Editor.
    [SerializeField] private float moveForce;
    [SerializeField] private float jumpForce;
    public LayerMask groundLayer;
    private float groundCheckWidth = 0.75f;
    private float groundCheckHeight = 0.1f;
    private Animator an;
    private Rigidbody2D rb;
    private Vector3 lastCheckpoint; // Stores the last checkpoint position

    void Start()
    {
        an = GetComponentInChildren<Animator>();
        isGrounded = false;
        rb = GetComponent<Rigidbody2D>();
        lastCheckpoint = transform.position; // Start position as the first checkpoint
    }

    void Update()
    {
        GroundedCheck();

        // Horizontal movement.
        float moveInput = Input.GetAxis("Horizontal");
        float moveInputRaw = Input.GetAxisRaw("Horizontal"); // Clamped to -1, 0, or 1.
        // bool isMoving = Mathf.Abs(moveInputRaw) > 0f;
        an.SetBool("IsMoving", Mathf.Abs(moveInputRaw) > 0f);
        // Set horizontal force in player. Use current vertical velocity.
        rb.velocity = new Vector2(moveInput * moveForce * Time.fixedDeltaTime, rb.velocity.y);

        // Flip the player. Could use down and up functions for not every frame.
        if (moveInputRaw != 0)
            transform.localScale = new Vector3(moveInputRaw, 1, 1);

        // Trigger jump. Use current horizontal velocity.
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime);
            //Game.Instance.SOMA.PlaySound("Jump");
        }
    }

    private void GroundedCheck()
    {
        isGrounded = Physics2D.OverlapBox(groundDetect.position,
            new Vector2(groundCheckWidth, groundCheckHeight), 0f, groundLayer);
        an.SetBool("IsJumping", !isGrounded);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            lastCheckpoint = collision.transform.position; // Update checkpoint
            //Game.Instance.SOMA.PlaySound("Checkpoint");
        }
        else if (collision.CompareTag("Finish"))
        {
            Time.timeScale = 0f; //Stop game
            //Game.Instance.SOMA.PlaySound("Win");
            //Game.Instance.SOMA.PlayMusic("StillDre");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pit")) // Make sure the player has the "Player" tag
        {
            Debug.Log("Player fell off! Respawning...");
            //Game.Instance.SOMA.PlaySound("Lose");
            transform.position = lastCheckpoint;
            rb.velocity = Vector2.zero;
        }
    }
}
