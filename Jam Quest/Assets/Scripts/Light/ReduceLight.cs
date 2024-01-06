using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ReduceLight : MonoBehaviour
{
    [SerializeField] Light2D smallLight;
    [SerializeField] Light2D mediumLight;
    [SerializeField] Light2D bigLight;

    private float smallLightRadius;
    private float mediumLightRadius;
    private float bigLightRadius;

    [SerializeField] private float reduceAmount;
    [SerializeField] private float reduceTime;

    [SerializeField] private float augmentAmount;
    [SerializeField] private float augmentTime;

    private static bool reduce;

    private static float currentSmallLightRadius;
    private static float currentMediumLightRadius;
    private static float currentBigLightRadius;

    private void Awake()
    {
        smallLightRadius = smallLight.pointLightOuterRadius;
        mediumLightRadius = mediumLight.pointLightOuterRadius;
        bigLightRadius = bigLight.pointLightOuterRadius;
    }

    private void Update()
    {
        currentSmallLightRadius = smallLight.pointLightOuterRadius;
        currentMediumLightRadius = mediumLight.pointLightOuterRadius;
        currentBigLightRadius = bigLight.pointLightOuterRadius;

        if (reduce)
        {
            if (smallLight.pointLightOuterRadius > 0)
            {
                smallLight.pointLightOuterRadius -= reduceAmount * reduceTime * Time.deltaTime;
                smallLight.pointLightInnerRadius -= reduceAmount * reduceTime * Time.deltaTime;
            }

            if (mediumLight.pointLightOuterRadius > 0)
            {
                mediumLight.pointLightOuterRadius -= reduceAmount * reduceTime * Time.deltaTime;
                mediumLight.pointLightInnerRadius -= reduceAmount * reduceTime * Time.deltaTime;
            }

            if (bigLight.pointLightOuterRadius > 0)
            {
                bigLight.pointLightOuterRadius -= reduceAmount * reduceTime * Time.deltaTime;
                bigLight.pointLightInnerRadius -= reduceAmount * reduceTime * Time.deltaTime;
            }
        }
        else
        {
            if (bigLight.pointLightOuterRadius < bigLightRadius)
            {
                bigLight.pointLightOuterRadius += augmentAmount * augmentTime * Time.deltaTime;
                bigLight.pointLightInnerRadius += augmentAmount * augmentTime * Time.deltaTime;
            }
            
            if (mediumLight.pointLightOuterRadius < mediumLightRadius && bigLight.pointLightOuterRadius > bigLightRadius / 2)
            {
                mediumLight.pointLightOuterRadius += augmentAmount * augmentTime * Time.deltaTime;
                mediumLight.pointLightInnerRadius += augmentAmount * augmentTime * Time.deltaTime;
            }
            
            if (smallLight.pointLightOuterRadius < smallLightRadius && mediumLight.pointLightOuterRadius > mediumLightRadius / 2)
            {
                smallLight.pointLightOuterRadius += augmentAmount * augmentTime * Time.deltaTime;
                smallLight.pointLightInnerRadius += augmentAmount * augmentTime * Time.deltaTime;
            }
        }
    }

    public static void SetReduce(bool value)
    {
        reduce = value;
    }
    public static float GetSmallLightRadius()
    {
        return currentSmallLightRadius;
    }
    public static float GetMediumLightRadius()
    {
        return currentMediumLightRadius;
    }
    public static float GetBigLightRadius()
    {
        return currentBigLightRadius;
    }
}
