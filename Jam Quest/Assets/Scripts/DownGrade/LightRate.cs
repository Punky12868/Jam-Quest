using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "DowngradeCards/Light Downgrade")]
public class LightRate : DownGradeCard
{
    [Header("Custom Card Variables")]
    public float newReduceSpeed;
    public override void CardEffect()
    {
        FindObjectOfType<ReduceLight>().SetReduceTime(newReduceSpeed);
    }
}
