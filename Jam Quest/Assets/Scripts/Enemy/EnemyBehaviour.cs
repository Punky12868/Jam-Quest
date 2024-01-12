using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyBehaviour : MonoBehaviour
{
    Transform player;
    public bool playerInSight = false;

    [SerializeField] Light2D light2D;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;

    [SerializeField] GameObject waypointsHolder;
    Transform[] waypoints;
    int waypointIndex;

    float localScaleX;
    bool isFlipped;

    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask IgnoreLayer;

    [SerializeField] Vector2 raycastOriginOffset;
    [SerializeField] Vector2[] raycastDirections;

    private int directionLevel = 0;

    private float xPositiveDirValue;
    private float xNegativeDirValue;

    private float xPositiveMultiply;
    private float xNegativeMultiply;

    private void Awake()
    {
        waypoints = waypointsHolder.GetComponent<Waypoints>().GetWaypoints();
        localScaleX = transform.localScale.x;
        player = GameObject.FindGameObjectWithTag("Player").transform;


        if (raycastDirections[raycastDirections.Length - 1].x > -0.001)
        {
            xPositiveDirValue = raycastDirections[raycastDirections.Length - 1].x;
            xNegativeDirValue = -raycastDirections[raycastDirections.Length - 1].x;
        }
        else if (raycastDirections[raycastDirections.Length - 1].x < 0.001)
        {
            xPositiveDirValue = -raycastDirections[raycastDirections.Length - 1].x;
            xNegativeDirValue = raycastDirections[raycastDirections.Length - 1].x;
        }

        xPositiveMultiply = xPositiveDirValue * 2;
        xNegativeMultiply = xNegativeDirValue * 2;

        EnemyLevelLogic();
    }

    private void Update()
    {
        Movement();
        Vision();
        //enemyVision
    }
    private void Movement()
    {
        if (playerInSight)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, runSpeed * Time.deltaTime);

            if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector2(localScaleX, transform.localScale.y);

                isFlipped = true;
            }
            else if (player.position.x > transform.position.x)
            {
                transform.localScale = new Vector2(-localScaleX, transform.localScale.y);

                isFlipped = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, walkSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, waypoints[waypointIndex].position) < 0.1f)
            {
                if (waypointIndex < waypoints.Length - 1)
                {
                    waypointIndex++;
                }
                else
                {
                    waypointIndex = 0;
                }
            }

            if (waypoints[waypointIndex].position.x < transform.position.x)
            {
                transform.localScale = new Vector2(localScaleX, transform.localScale.y);

                isFlipped = true;
            }
            else if (waypoints[waypointIndex].position.x > transform.position.x)
            {
                transform.localScale = new Vector2(-localScaleX, transform.localScale.y);

                isFlipped = false;
            }
        }
    }
    private void Vision()
    {
        for (int i = 0; i < raycastDirections.Length; i++) // Linecast from point a to point b, if the raycast hits the player, set playerInSight to true
        {
            RaycastHit2D hit = Physics2D.Linecast((Vector2)transform.position + raycastOriginOffset, (Vector2)transform.position + raycastOriginOffset + raycastDirections[i], ~IgnoreLayer);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    playerInSight = true;
                }
            }
        }

        if (isFlipped) // turn the raycast directions x value negative if the enemy is flipped, and vice versa... also do the same for the raycast origin offset
        {
            for (int i = 0; i < raycastDirections.Length; i++)
            {
                raycastDirections[i] = new Vector2(-Mathf.Abs(raycastDirections[i].x), raycastDirections[i].y);
            }

            raycastOriginOffset = new Vector2(-Mathf.Abs(raycastOriginOffset.x), raycastOriginOffset.y);
        }
        else
        {
            for (int i = 0; i < raycastDirections.Length; i++)
            {
                raycastDirections[i] = new Vector2(Mathf.Abs(raycastDirections[i].x), raycastDirections[i].y);
            }

            raycastOriginOffset = new Vector2(Mathf.Abs(raycastOriginOffset.x), raycastOriginOffset.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<Damage>().OnDeath();
            playerInSight = false;
        }
    }

    public void SetDirLevel(int level)
    {
        directionLevel = level;

        EnemyLevelLogic();
    }
    public int GetDirLevel()
    {
        return directionLevel;
    }

    public void SetWaypoint(int value)
    {
        waypointIndex = value;
    }

    public void SetMultiplyOuterLightDistance(int value)
    {
        light2D.pointLightOuterRadius *= value;
    }
    public void SetDivideOuterLightDistance(int value)
    {
        light2D.pointLightOuterRadius /= value;
    }
    public float GetLightValue()
    {
        return light2D.pointLightOuterRadius;
    }
    public void SetLightValue(float value)
    {
        light2D.pointLightOuterRadius = value;
    }

    public void EnemyLevelLogic()
    {
        if (directionLevel == 1)
        {
            if (raycastDirections[raycastDirections.Length - 1].x * 2 == xPositiveMultiply && raycastDirections[raycastDirections.Length - 1].x > -0.01 ||
                raycastDirections[raycastDirections.Length - 1].x * 2 == xNegativeMultiply && raycastDirections[raycastDirections.Length - 1].x < 0.01)
            {
                for (int i = 0; i < raycastDirections.Length; i++)
                {
                    raycastDirections[i] *= 2;
                }
            }
        }
        else if (directionLevel == 0)
        {
            if (xPositiveDirValue != raycastDirections[raycastDirections.Length - 1].x && raycastDirections[raycastDirections.Length - 1].x > -0.01 ||
                xNegativeDirValue != raycastDirections[raycastDirections.Length - 1].x && raycastDirections[raycastDirections.Length - 1].x < 0.01)
            {
                for (int i = 0; i < raycastDirections.Length; i++)
                {
                    raycastDirections[i] /= 2;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < raycastDirections.Length; i++)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawRay((Vector2)transform.position + raycastOriginOffset, raycastDirections[i]);

            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere((Vector2)transform.position + raycastOriginOffset + raycastDirections[i], 0.2f);
        }
    }
}