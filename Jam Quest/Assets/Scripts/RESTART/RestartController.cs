using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartController : MonoBehaviour
{
    RestartAllClass restartAllClass;

    [SerializeField] bool waitForFrame = true;
    [SerializeField] int waitFrames = 1;
    int frameCount = 0;

    bool waitFrame = false;

    GameObject player;
    Vector3 playerStartPos;

    [SerializeField] GameObject blurDowngrade;
    float playerSpeed;
    bool blurDowngradeActive;
    bool flipController;

    float smallLightRadius;
    float mediumLightRadius;
    float bigLightRadius;

    float reduceTime;

    GameObject[] enemies;
    Vector3[] enemiesStartPos;
    int enemyLevel;

    float enemyLightValue;

    private void Awake()
    {
        restartAllClass = new RestartAllClass();

        if (!waitForFrame)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            player = GameObject.FindGameObjectWithTag("Player");

            SaveProgress();

            restartAllClass.GetStartingVariables(player, blurDowngrade, playerSpeed, blurDowngradeActive, flipController, smallLightRadius, mediumLightRadius, bigLightRadius, reduceTime, enemies, playerStartPos, enemiesStartPos, enemyLightValue, enemyLevel);
        }
        else
        {
            waitFrame = true;
        }
    }
    private void Update()
    {
        if (waitFrame)
        {
            frameCount++;

            if (frameCount >= waitFrames)
            {
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
                player = GameObject.FindGameObjectWithTag("Player");


                SaveProgress();
                restartAllClass.GetStartingVariables(player, blurDowngrade, playerSpeed, blurDowngradeActive, flipController, smallLightRadius, mediumLightRadius, bigLightRadius, reduceTime, enemies, playerStartPos, enemiesStartPos, enemyLightValue, enemyLevel);

                waitFrame = false;
            }
        }
    }
    public void SaveProgress()
    {
        //PLAYER

        playerStartPos = FindObjectOfType<Death>().GetSpawnPoint();
        playerSpeed = player.GetComponent<PlayerController>().GetSpeed();
        flipController = player.GetComponent<PlayerController>().GetFlipController();
        blurDowngradeActive = blurDowngrade.activeInHierarchy ? true : false;

        reduceTime = FindObjectOfType<ReduceLight>().GetReduceTime();

        //ENEMY

        enemiesStartPos = new Vector3[enemies.Length];
        enemyLightValue = enemies[0].GetComponent<EnemyBehaviour>().GetLightValue();
        enemyLevel = enemies[0].GetComponent<EnemyBehaviour>().GetDirLevel();

        for (int i = 0; i < enemies.Length; i++)
        {
            enemiesStartPos[i] = enemies[i].transform.position;
        }

        //LIGHT

        smallLightRadius = FindObjectOfType<ReduceLight>().GetSmallLightRadius();
        mediumLightRadius = FindObjectOfType<ReduceLight>().GetMediumLightRadius();
        bigLightRadius = FindObjectOfType<ReduceLight>().GetBigLightRadius();
    }

    public void LoadProgress()
    {
        //PLAYER

        player.transform.position = playerStartPos;
        player.GetComponent<PlayerController>().SetNewSpeed(playerSpeed);
        player.GetComponent<PlayerController>().SetFlipController(flipController);
        blurDowngrade.SetActive(blurDowngradeActive);

        //LIGHT

        FindObjectOfType<ReduceLight>().SetSmallLightRadius(smallLightRadius);
        FindObjectOfType<ReduceLight>().SetMediumLightRadius(mediumLightRadius);
        FindObjectOfType<ReduceLight>().SetBigLightRadius(bigLightRadius);

        FindObjectOfType<ReduceLight>().SetReduceTime(reduceTime);

        //ENEMY

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].transform.position = enemiesStartPos[i];
            enemies[i].GetComponent<EnemyBehaviour>().SetWaypoint(0);
            enemies[i].GetComponent<EnemyBehaviour>().SetLightValue(enemyLightValue);

            enemies[i].GetComponent<EnemyBehaviour>().SetDirLevel(enemyLevel);
        }
    }

    public void Restart()
    {

        player = restartAllClass.ReturnPlayerGameObject();
        blurDowngrade = restartAllClass.ReturnBlurDowngradeGameObject();
        playerSpeed = restartAllClass.ReturnPlayerSpeed();
        blurDowngradeActive = restartAllClass.ReturnBlurDowngradeActive();
        flipController = restartAllClass.ReturnFlipController();
        smallLightRadius = restartAllClass.ReturnSmallLightRadius();
        mediumLightRadius = restartAllClass.ReturnMediumLightRadius();
        bigLightRadius = restartAllClass.ReturnBigLightRadius();
        reduceTime = restartAllClass.ReturnReduceTime();
        enemies = restartAllClass.ReturnEnemies();
        playerStartPos = restartAllClass.ReturnPlayerStartPos();
        enemiesStartPos = restartAllClass.ReturnEnemiesStartPos();

        enemyLightValue = restartAllClass.ReturnEnemyLightValue();

        enemyLevel = restartAllClass.ReturnEnemyLevel();

        FindObjectOfType<AllCheckPoints>().DeactivateAllCheckPoints();

        LoadProgress();
    }
}

public class RestartAllClass
{
    GameObject player;
    GameObject blurDowngrade;
    float playerSpeed;
    bool blurDowngradeActive;
    bool flipController;

    float smallLightRadius;
    float mediumLightRadius;
    float bigLightRadius;

    float reduceTime;

    GameObject[] enemies;

    Vector3 playerStartPos;
    Vector3[] enemiesStartPos;
    int enemyLevel;
    float enemyLightValue;

    public void GetStartingVariables(GameObject player, GameObject blurDowngrade, float playerSpeed, bool blurDowngradeActive, bool flipController, float smallLightRadius, float mediumLightRadius, float bigLightRadius, float reduceTime, GameObject[] enemies, Vector3 playerStartPos, Vector3[] enemiesStartPos, float enemyLightValue, int enemyLevel)
    {
        this.player = player;
        this.blurDowngrade = blurDowngrade;
        this.playerSpeed = playerSpeed;
        this.blurDowngradeActive = blurDowngradeActive;
        this.flipController = flipController;
        this.smallLightRadius = smallLightRadius;
        this.mediumLightRadius = mediumLightRadius;
        this.bigLightRadius = bigLightRadius;
        this.reduceTime = reduceTime;
        this.enemies = enemies;
        this.playerStartPos = playerStartPos;
        this.enemiesStartPos = enemiesStartPos;
        this.enemyLightValue = enemyLightValue;
        this.enemyLevel = enemyLevel;
    }

    #region Return Data

    public GameObject ReturnPlayerGameObject()
    {
        return player;
    }
    public GameObject ReturnBlurDowngradeGameObject()
    {
        return blurDowngrade;
    }
    public float ReturnPlayerSpeed()
    {
        return playerSpeed;
    }
    public bool ReturnBlurDowngradeActive()
    {
        return blurDowngradeActive;
    }
    public bool ReturnFlipController()
    {
        return flipController;
    }
    public float ReturnSmallLightRadius()
    {
        return smallLightRadius;
    }
    public float ReturnMediumLightRadius()
    {
        return mediumLightRadius;
    }
    public float ReturnBigLightRadius()
    {
        return bigLightRadius;
    }
    public float ReturnReduceTime()
    {
        return reduceTime;
    }
    public GameObject[] ReturnEnemies()
    {
        return enemies;
    }
    public Vector3 ReturnPlayerStartPos()
    {
        return playerStartPos;
    }
    public Vector3[] ReturnEnemiesStartPos()
    {
        return enemiesStartPos;
    }
    public int ReturnEnemyLevel()
    {
        return enemyLevel;
    }
    public float ReturnEnemyLightValue()
    {
        return enemyLightValue;
    }

    #endregion
}
