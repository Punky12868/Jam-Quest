using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloqued : MonoBehaviour
{
    private static bool isBloqued = false;

    private static Transform headPos;
    private static Vector2 boxSize;
    private static float boxAngle;
    private static LayerMask ceilingLayer;

    public static void SetBloquedVariables(Transform pos, Vector2 size, float angle, LayerMask layer)
    {
        headPos = pos;
        boxSize = size;
        boxAngle = angle;
        ceilingLayer = layer;
    }
    public static bool IsBloqued()
    {
        isBloqued = Physics2D.OverlapBox(headPos.position, boxSize, boxAngle, ceilingLayer);
        return isBloqued;
    }
}
