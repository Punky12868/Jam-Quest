using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "DowngradeCards/Minimal Light Downgrade")]
public class MinimalLight : DownGradeCard
{
    [Header("Custom Card Variables")]

    public float newSmallLightRadius;
    public float newMediumLightRadius;
    public float newBigLightRadius;

    public override void CardEffect()
    {
        FindObjectOfType<ReduceLight>().SetSmallLightRadius(newSmallLightRadius);
        FindObjectOfType<ReduceLight>().SetMediumLightRadius(newMediumLightRadius);
        FindObjectOfType<ReduceLight>().SetBigLightRadius(newBigLightRadius);
    }
}
