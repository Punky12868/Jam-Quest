using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    Animator anim;
    EnemyBehaviour enemyBehaviour;

    const string ENEMY_RUN = "Enemy_Run";

    private void Awake()
    {
        enemyBehaviour = GetComponent<EnemyBehaviour>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (enemyBehaviour.playerInSight)
        {
            anim.Play(ENEMY_RUN);
        }
    }
}
