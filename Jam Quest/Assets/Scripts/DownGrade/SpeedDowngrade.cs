using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "DowngradeCards/Speed Downgrade")]
public class SpeedDowngrade : DownGradeCard
{
    [Header ("Custom Card Variables")]
    public float newSpeed;
    public override void CardEffect()
    {
        FindObjectOfType<PlayerController>().SetNewSpeed(newSpeed);
    }
}
