using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "DowngradeCards/Enemy Vision Downgrade")]
public class EnemyDetect : DownGradeCard
{
    [Header("Custom Card Variables")]
    public int enemyVisionIncrease;
    public int enemyVisionCone;
    public override void CardEffect()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyBehaviour>().SetMultiplyOuterLightDistance(enemyVisionCone);
            enemy.GetComponent<EnemyBehaviour>().SetDirLevel(enemyVisionIncrease);
        }
    }
}
