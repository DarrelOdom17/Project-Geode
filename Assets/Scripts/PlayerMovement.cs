using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;

    [Header("Movement Variables")]
    [SerializeField] private float maxMoveSpeed = 10f;
    [SerializeField] private int jumpsLeft = 2;
    private float moveDirectionX;
    private float moveDirectionY;

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float fallSpeed = 5f;
    [SerializeField] private float fallMultiplier = 8f;
    [SerializeField] private float lowJumpFallMultiplier = 5f;
    public bool isGrounded = false;
    public bool jumpReleased = false;



    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        moveDirectionX = GetInput().x;
        MoveCharacter(); 
        //PlayerFall();
        Jump();

        isGrounded = GroundCheck();
        jumpReleased = Input.GetButtonUp("Jump");
    }

    private void FixedUpdate()
    {
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
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 newVelocity;
            newVelocity.x = rb.velocity.x;
            newVelocity.y = jumpForce;

            rb.velocity = newVelocity;
            jumpsLeft -= 1;

            if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
            {
                newVelocity.x = rb.velocity.x;
                newVelocity.y = jumpForce;

                rb.velocity = newVelocity;
                jumpsLeft -= 1;
            }

            if (Input.GetButtonDown("Jump") && jumpsLeft == 0)
            {
                //PlayerFall();
                jumpsLeft = 2;
            }
        }

        if (jumpReleased && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

      jumpsLeft = 2;
    }

    public bool GroundCheck()
    {
        Vector2 origin = transform.position;
        
        float radius = 0.2f;

        // detect downwards
        Vector2 direction;
        direction.x = 0;
        direction.y = -1;

        float distance = 0.5f;
        LayerMask layerMask = LayerMask.GetMask("Ground");

        RaycastHit2D hitRec = Physics2D.CircleCast(origin, radius, direction, distance, layerMask);

        return hitRec.collider != null;
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




    // Old jump code

   /* private void Jump()
    {
        Vector2 newVelocity;
        newVelocity.x = rb.velocity.x;
        newVelocity.y = jumpSpeed;

        rb.velocity = newVelocity;

        jumpsLeft -= 1;

        if (!Input.GetButtonDown("Jump"))
            return;

        else if (Input.GetButtonDown("Jump") && jumpsLeft == 1)
        {
            //Jump();
            //animator.SetTrigger("IsJumpFirst");
            isGrounded = true;
        }

        else if (Input.GetButtonUp("Jump") && jumpsLeft <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
   */
}
