using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "DowngradeCards/Flip Controller")]
public class ControllerFlip : DownGradeCard
{
    //[Header("Custom Card Variables")]
    //public bool flipControls;
    public override void CardEffect()
    {
        FindObjectOfType<PlayerController>().SetFlipController(true);
    }
}
