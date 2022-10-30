using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private TrailRenderer trailRenderer;
    private Animator animator;

    [Header("Movement Variables")]
    [SerializeField] private float maxMoveSpeed = 10f;
    private float originalMoveSpeed = 10f;
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

    [Header("Dash Variables")]
    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeed = 20f;
    private Vector2 dashingDir;
    private bool isDashing;
    private bool canDash = true;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask Ground;

    [Header("Ground Collison Variables")]
    [SerializeField] private float groundRayLength;
    public bool isGrounded;
    public bool jumpReleased;
    private bool dashInput;
    private bool isJumping;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        //animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        moveDirectionY = GetInput().y;
        moveDirectionX = GetInput().x;
        jumpReleased = Input.GetButtonUp("Jump");
        dashInput = Input.GetButtonDown("Dash");
        Jump();
        Dash();
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
            canDash = true;
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

        if (moveDirectionX > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveDirectionX < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
            
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
            isJumping = true;
            //currentJumpTime = 0f;
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
            currentJumpTime += Time.deltaTime;
        }

        if (Input.GetButton("Jump") && isJumping == true)
        {
            if (currentJumpTime < maxJumpTime)
            {
                Vector2 newVelocity;
                newVelocity.x = rb.velocity.x;
                newVelocity.y = jumpForce;
                rb.velocity = newVelocity;
                currentJumpTime += Time.deltaTime;
            }

            else if (currentJumpTime >= maxJumpTime)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                isJumping = false;
            }
        }

        if (jumpReleased && rb.velocity.y > 0)
        {
            jumpReleased = true;
            isJumping = false;
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

    private void Dash()
    {
        float originalGravity = rb.gravityScale;

        if (dashInput && canDash)
        {
            rb.gravityScale = 0f;
            isDashing = true;
            canDash = false;
            trailRenderer.emitting = true;
            dashingDir = GetInput();
        
            if (dashingDir == Vector2.zero)
            {
                dashingDir = new Vector2(moveDirectionX, 0);
            }

            StartCoroutine(StopDashing());
        }

        if (isDashing)
        {
            rb.velocity = dashingDir * dashSpeed;
            return;
        }

        if (isGrounded)
        {
            rb.gravityScale = originalGravity;
            canDash = true;
        }
    }

    public bool GroundCheck()
    {
        LayerMask Ground = LayerMask.GetMask("Ground");
        RaycastHit2D hitRec = Physics2D.Raycast(transform.position * groundRayLength, Vector2.down, groundRayLength, Ground);

        return hitRec.collider != null;
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashTime);
        trailRenderer.emitting = false;
        isDashing = false;
        
        if (isDashing == false)
            maxMoveSpeed = originalMoveSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundRayLength);
    }
}
