using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    BoxCollider2D boxCollider;
    Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] float speed = 5f;
    float moveBy;

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
        float x = Input.GetAxisRaw("Horizontal");
        moveBy = x * speed;
    }
    #endregion

    private void CrouchInput()
    {
        if (Input.GetKey(crouchKey))
        {
            boxCollider.size = new Vector2(boxCollider.size.x, colliderSizeCrouched);
            boxCollider.offset = new Vector2(boxCollider.offset.x, colliderOffsetCrouched);
        }
        else if (!Bloqued.IsBloqued())
        {
            boxCollider.size = new Vector2(boxCollider.size.x, colliderSizeStanding);

            if (boxCollider.offset.y != colliderOffsetStanding)
            {
                boxCollider.offset = new Vector2(boxCollider.offset.x, colliderOffsetStanding);
            }
        }
    }

    #region Jump
    private void JumpInput()
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(feet.position, boxSizeGround);
        Gizmos.DrawWireCube(head.position, boxSizeCeiling);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + boxCollider.offset, boxCollider.size);
    }
}