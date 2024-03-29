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

    private void Awake()
    {
        smallLightRadius = smallLight.pointLightOuterRadius;
        mediumLightRadius = mediumLight.pointLightOuterRadius;
        bigLightRadius = bigLight.pointLightOuterRadius;
    }
    private void Update()
    {
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

    public float GetSmallLightRadius()
    {
        return smallLightRadius;
    }
    public float GetMediumLightRadius()
    {
        return mediumLightRadius;
    }
    public float GetBigLightRadius()
    {
        return bigLightRadius;
    }
    public void SetReduceTime(float value)
    {
        reduceTime = value;
    }
    public float GetReduceTime()
    {
        return reduceTime;
    }
    public void SetSmallLightRadius(float value)
    {
        smallLightRadius = value;
        smallLight.pointLightOuterRadius = value;
        smallLight.pointLightInnerRadius = value;
    }
    public void SetMediumLightRadius(float value)
    {
        mediumLightRadius = value;
        mediumLight.pointLightOuterRadius = value;
        mediumLight.pointLightInnerRadius = value;
    }
    public void SetBigLightRadius(float value)
    {
        bigLightRadius = value;
        bigLight.pointLightOuterRadius = value;
        bigLight.pointLightInnerRadius = value;
    }
}
