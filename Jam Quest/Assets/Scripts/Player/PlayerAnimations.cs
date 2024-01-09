using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    PlayerController playerController;

    [SerializeField] bool flipDirection;

    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_RUN = "Player_Run";
    const string PLAYER_CROUCH = "Player_Crouch";
    const string PLAYER_CROUCHMOVE = "Player_CrouchMove";

    const string PLAYER_JUMP = "Player_Jump";
    const string PLAYER_FALL = "Player_Fall";


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Grounded.IsGrounded() && !playerController.GetCrouchStatus())
        {
            if (playerController.GetMovementDirection() > 0)
            {
                spriteRenderer.flipX = flipDirection ? false : true;
                anim.Play(PLAYER_RUN);
            }
            else if (playerController.GetMovementDirection() < 0)
            {
                spriteRenderer.flipX = flipDirection ? true : false;
                anim.Play(PLAYER_RUN);
            }
            else
            {
                anim.Play(PLAYER_IDLE);
            }
        }
        else if (Grounded.IsGrounded() && playerController.GetCrouchStatus())
        {
            if (playerController.GetMovementDirection() > 0)
            {
                spriteRenderer.flipX = flipDirection ? false : true;
                anim.Play(PLAYER_CROUCHMOVE);
            }
            else if (playerController.GetMovementDirection() < 0)
            {
                spriteRenderer.flipX = flipDirection ? true : false;
                anim.Play(PLAYER_CROUCHMOVE);
            }
            else
            {
                anim.Play(PLAYER_CROUCH);
            }
        }

        if (!Grounded.IsGrounded() && rb.velocity.y > 0)
        {
            anim.Play(PLAYER_JUMP);
        }
        else if (!Grounded.IsGrounded() && rb.velocity.y < 0)
        {
            anim.Play(PLAYER_FALL);
        }
    }
}
