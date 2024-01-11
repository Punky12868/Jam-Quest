using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "DowngradeCards/Blur Downgrade")]
public class BlurDowngrade : DownGradeCard
{
    //[Header("Custom Card Variables")]
    //public float blurAmount;
    public override void CardEffect()
    {
        GameObject.FindGameObjectWithTag("InGame").transform.GetChild(1).gameObject.SetActive(true);
    }
}
