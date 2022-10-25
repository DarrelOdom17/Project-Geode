using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;

    [Header("Movement Variables")]
    [SerializeField] private float maxMoveSpeed = 10f;
    private float moveDirectionX;
    private float moveDirectionY;

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float fallSpeed = 5f;
    [SerializeField] private float fallMultiplier = 8f;
    [SerializeField] private float lowJumpFallMultiplier = 5f;
    [SerializeField] private float maxJumpTime = 0.5f;
    [SerializeField] private int jumpsLeft = 1;
    private int jumpCounter = 0;
    [SerializeField] private float currentJumpTime;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask Ground;

    [Header("Ground Collison Variables")]
    [SerializeField] private float groundRayLength;
    public bool isGrounded;
    public bool jumpReleased;
    private bool isJumping;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        moveDirectionX = GetInput().x;
        jumpReleased = Input.GetButtonUp("Jump");
        Jump();
    }

    private void FixedUpdate()
    {
        isGrounded = GroundCheck();
        if (isGrounded == true)
        {
            jumpsLeft = 1;
            jumpCounter = 0;
            currentJumpTime = 0f;
            maxJumpTime = 0.35f;
        }

        MoveCharacter();
        PlayerFall();
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MoveCharacter()
    {
        float moveDirectionX = Input.GetAxisRaw("Horizontal") * maxMoveSpeed;

        // Velocity Settings
        Vector2 newVelocity;

        newVelocity.x = moveDirectionX;
        newVelocity.y = rb.velocity.y;
        rb.velocity = newVelocity;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            isJumping = false;
            Vector2 newVelocity;
            newVelocity.x = rb.velocity.x;
            newVelocity.y = jumpForce;
            rb.velocity = newVelocity;
        }

        if (Input.GetButtonDown("Jump") && !isGrounded && jumpsLeft > 0)
        {
            isJumping = true;
            Vector2 newVelocity;
            newVelocity.x = rb.velocity.x;
            newVelocity.y = jumpForce;
            rb.velocity = newVelocity;

            jumpCounter++;
            jumpsLeft--;
            //currentJumpTime += Time.deltaTime;
        }

        if (Input.GetButton("Jump"))
        {
            currentJumpTime += Time.deltaTime;

            if (currentJumpTime < maxJumpTime)
            {
                Vector2 newVelocity;
                newVelocity.x = rb.velocity.x;
                newVelocity.y = jumpForce;
                rb.velocity = newVelocity;
            }

            else if (currentJumpTime >= maxJumpTime && rb.velocity.y > 0)
            {
                //rb.gravityScale = fallMultiplier;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                //isJumping = true;
            }
        }

        if (jumpReleased && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        
    }

    private void PlayerFall()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpFallMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    public bool GroundCheck()
    {
        LayerMask Ground = LayerMask.GetMask("Ground");
        RaycastHit2D hitRec = Physics2D.Raycast(transform.position * groundRayLength, Vector2.down, groundRayLength, Ground);

        return hitRec.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundRayLength);
    }
}
