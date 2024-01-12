using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    BoxCollider2D boxCollider;
    Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] float speed = 5f;
    [SerializeField] float crouchSpeed = 2.5f;
    float moveBy;

    bool canMove = true;
    bool flipController = false;

    [Header("Jump")]
    [SerializeField] float jumpForce = 5f;
    bool alreadyJumped = false;

    [SerializeField] float jumpBufferLength = 0.1f;
    float jumpBufferCount;
    bool jumpBufferExpired;
    bool autoJump = false;

    [SerializeField] float coyoteTimeLength = 0.1f;
    float coyoteTimeCount;

    [Header("Crouch")]
    [SerializeField] float colliderSizeCrouched;
    [SerializeField] float colliderOffsetCrouched;
    float colliderOffsetStanding;
    float colliderSizeStanding;

    [Header("Ground Check")]
    [SerializeField] Transform feet;
    [SerializeField] LayerMask groundLayers;
    [SerializeField] Vector2 boxSizeGround;
    float boxAngle = 0f;
    bool isCrouching;

    [Header("Ceiling Check")]
    [SerializeField] Transform head;
    [SerializeField] LayerMask ceilingLayers;
    [SerializeField] Vector2 boxSizeCeiling;
    float boxAngleCrouched;

    [Header("Inputs")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode crouchKey = KeyCode.LeftControl;
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        colliderSizeStanding = boxCollider.size.y;
        colliderOffsetStanding = boxCollider.offset.y;
        Grounded.SetGroundedVariables(feet, boxSizeGround, boxAngle, groundLayers);
        Bloqued.SetBloquedVariables(head, boxSizeCeiling, boxAngleCrouched, ceilingLayers);
    }
    private void Update()
    {
        MoveInput();
        CrouchInput();
        JumpInput();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }

    #region Inputs

    #region Move
    private void MoveInput()
    {
        if (canMove)
        {
            float x = Input.GetAxisRaw("Horizontal");

            if (isCrouching && Grounded.IsGrounded())
            {
                moveBy = flipController ? -x * crouchSpeed : x * crouchSpeed;
            }
            else
            {
                moveBy = flipController ? -x * speed : x * speed;
            }

            //moveBy = x * speed;
        }
        else
        {
            moveBy = 0;
        }
    }
    #endregion

    private void CrouchInput()
    {
        if (Input.GetKey(crouchKey) && canMove)
        {
            boxCollider.size = new Vector2(boxCollider.size.x, colliderSizeCrouched);
            boxCollider.offset = new Vector2(boxCollider.offset.x, colliderOffsetCrouched);

            isCrouching = true;
        }
        else if (!Bloqued.IsBloqued())
        {
            boxCollider.size = new Vector2(boxCollider.size.x, colliderSizeStanding);

            if (boxCollider.offset.y != colliderOffsetStanding)
            {
                boxCollider.offset = new Vector2(boxCollider.offset.x, colliderOffsetStanding);
            }

            isCrouching = false;
        }
    }

    #region Jump
    private void JumpInput()
    {
        if (canMove)
        {
            if (Input.GetKeyDown(jumpKey))
            {
                if (Grounded.IsGrounded() || !jumpBufferExpired)
                {
                    Jump();
                }
                else if (!Grounded.IsGrounded())
                {
                    if (!alreadyJumped && coyoteTimeCount > 0)
                    {
                        Jump();
                    }
                    else
                    {
                        jumpBufferCount = jumpBufferLength;
                    }
                }
            }

            JumpLogic();
        }
    }
    private void JumpLogic()
    {
        // Coyote Time
        if (Grounded.IsGrounded() && coyoteTimeCount <= 0)
        {
            coyoteTimeCount = coyoteTimeLength;
            alreadyJumped = false; // Reset alreadyJumped when grounded
        }
        else if (!Grounded.IsGrounded() && coyoteTimeCount > 0)
        {
            coyoteTimeCount -= Time.deltaTime;
        }

        // Jump Buffer
        if (jumpBufferCount > 0)
        {
            jumpBufferCount -= Time.deltaTime;
            autoJump = true;
        }
        else
        {
            jumpBufferExpired = true;
            autoJump = false;
        }

        // Auto Jump
        if (autoJump && Grounded.IsGrounded())
        {
            Jump();
        }
    }
    private void Jump()
    {
        alreadyJumped = true;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    #endregion

    #endregion

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(feet.position, boxSizeGround);
        Gizmos.DrawWireCube(head.position, boxSizeCeiling);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + boxCollider.offset, boxCollider.size);
    }*/

    public float GetMovementDirection()
    {
        return moveBy;
    }
    public bool GetCrouchStatus()
    {
        return isCrouching;
    }
    public void SetNewSpeed(float newSpeed)
    {
        speed = newSpeed;
        crouchSpeed = speed / 2;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public void SetFlipController(bool flip)
    {
        flipController = flip;
    }
    public bool GetFlipController()
    {
        return flipController;
    }
    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}
