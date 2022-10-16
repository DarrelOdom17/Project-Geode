using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter2D : CharacterController2D
{
    [Header("Speed Settings")]
    [SerializeField]
    private float walkSpeed = 5.0f;
    [SerializeField]
    private float sprintSpeed = 7.0f;

    private float collisonTestOffset;
    public SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;
    private float jumpInputLastFrame = 0.0f;

    public bool isTouchingGround = false;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        bool sprint = Input.GetKey(KeyCode.LeftShift);
        isTouchingGround = IsTouchingGround();

        Vector2 motion = rigidbody2D.velocity;
        
        // Test before you move
        if (xInput != 0.0f)
            motion.x = xInput*walkSpeed;
        if (xInput != 0.0f && sprint == true)
            motion.x = xInput*sprintSpeed;

        if (xInput != 0.0f)

        if (Input.GetAxis("Jump") > 0.0f && isTouchingGround)
        {
            motion.y = walkSpeed + 2.5f;
        }

        rigidbody2D.velocity = motion;
    }
}
