using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chracter_movement : MonoBehaviour

{
    public float speed;
    public float jumpForce;
    public float winPositionX;
    public KeyCode moveLeftKey;
    public KeyCode moveRightKey;
    public KeyCode jumpKey;

    // Start is called before the first frame update
    void Awake()
    {
        speed = 5f;
        jumpForce = 10f;
        winPositionX = 20f;
        moveLeftKey = KeyCode.A;
        moveRightKey = KeyCode.D;
        jumpKey = KeyCode.Space;

    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = 0;

        // Check for custom controls
        if (Input.GetKey(moveLeftKey))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(moveRightKey))
        {
            horizontalInput = 1f;
        }

        Vector2 moveDirection = new Vector2(horizontalInput, 0);
        rb.velocity = new Vector2(moveDirection.x * speed, rb.velocity.y);

        // Check for jump
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Check for win condition
        if (transform.position.x >= winPositionX)
        {
            SceneManager.LoadScene("WinScene"); // Load a scene for winning
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene("LoseScene"); // Load a scene for losing
        }

        // Check if player is grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Check if player leaves the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    }
