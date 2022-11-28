using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    private TrailRenderer trailRenderer;
    public Animator animator;

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
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float verticalDashGravity;
    private Vector2 dashingDir;
    private bool isDashing;
    private bool canDash = true;

    [Header("KnockBack Variables")]
    [SerializeField] public float knockBackForce;
    [SerializeField] public float knockBackCounter;
    [SerializeField] public float knockBackTotalTime;
    [SerializeField] public bool knockFromRight;


    [Header("Layer Masks")]
    [SerializeField] private LayerMask Ground;

    [Header("Ground Collison Variables")]
    [SerializeField] private float groundRayLength;
    public bool isGrounded = false;
    public bool jumpReleased;
    private bool dashInput;
    private bool isJumping;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        animator = GetComponent<Animator>();
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
        MoveCharacter();
        PlayerFall();
    }

    private Vector2 GetInput()
    {
        return new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MoveCharacter()
    {
        if (knockBackCounter <= 0)
        {
            float moveDirectionX = Input.GetAxisRaw("Horizontal") * maxMoveSpeed;
            knockBackCounter = 0;
            animator.SetFloat("Speed", Mathf.Abs(moveDirectionX));

            if (moveDirectionX > 0)
            {
                transform.eulerAngles = new Vector2(0, 0);
            }
            else if (moveDirectionX < 0)
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
            
            // Velocity Settings
            Vector2 newVelocity;

            newVelocity.x = moveDirectionX;
            newVelocity.y = rb.velocity.y;
            rb.velocity = newVelocity;
        }
        else
        {
            if (knockFromRight == true)
            {
                rb.velocity = new Vector3(-knockBackForce, knockBackForce);
            }
            if (knockFromRight == false)
            {
                rb.velocity = new Vector3(knockBackForce, knockBackForce);
            }
            Debug.Log("Checked");

            knockBackCounter -= Time.deltaTime;
        }
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            isJumping = true;
            Vector2 newVelocity;
            newVelocity.x = rb.velocity.x;
            newVelocity.y = jumpForce;
            rb.velocity = newVelocity;
            animator.SetBool("isJumping", true);
        }

        if (Input.GetButtonDown("Jump") && !isGrounded && jumpsLeft > 0)
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
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
                animator.SetBool("isJumping", false);
                isJumping = false;
            }
        }

        if (jumpReleased && rb.velocity.y > 0)
        {
            jumpReleased = true;
            isJumping = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            animator.SetBool("isJumping", false);
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
            if (isDashing && rb.velocity.y > 0 && Input.GetButtonDown("Dash"))
            {
                rb.gravityScale = verticalDashGravity;
            }
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
            maxMoveSpeed = dashSpeed;
            isDashing = true;
            canDash = false;
            trailRenderer.emitting = true;
            dashingDir = GetInput();
            animator.SetBool("isDashing", true);
        
            if (dashingDir == Vector2.zero)
            {
                dashingDir = new Vector2(moveDirectionX, 0);
            }
            
            if (dashingDir == Vector2.up)
            {
                dashingDir = new Vector2(0, moveDirectionY);
                rb.gravityScale = verticalDashGravity;
            }

            StartCoroutine(StopDashing());
        }

        if (isDashing)
        {
            rb.velocity = dashingDir.normalized * dashSpeed;
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
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        LayerMask Ground = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(position, direction, groundRayLength, Ground);

        if (hit.collider != null)
        {
            jumpsLeft = 1;
            jumpCounter = 0;
            currentJumpTime = 0f;
            maxJumpTime = 0.4f;

            //Debug.Log("Raycast hit " + hit.collider.tag);
            animator.SetBool("isGrounded", true);
            return true;
        }
        
        animator.SetBool("isGrounded", false);
        return false;
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashTime);
        trailRenderer.emitting = false;
        isDashing = false;
        animator.SetBool("isDashing", false);
        maxMoveSpeed = originalMoveSpeed;
        yield return new WaitForSeconds(dashCooldown);
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundRayLength);
    }
}
