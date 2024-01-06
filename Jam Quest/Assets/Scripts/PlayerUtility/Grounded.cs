using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    private static bool isGrounded = false;

    private static Transform groundPos;
    private static Vector2 boxSize;
    private static float boxAngle;
    private static LayerMask groundLayer;

    public static void SetGroundedVariables(Transform pos, Vector2 size, float angle, LayerMask layer)
    {
        groundPos = pos;
        boxSize = size;
        boxAngle = angle;
        groundLayer = layer;
    }
    public static bool IsGrounded()
    {
        isGrounded = Physics2D.OverlapBox(groundPos.position, boxSize, boxAngle, groundLayer);
        return isGrounded;
    }
}
