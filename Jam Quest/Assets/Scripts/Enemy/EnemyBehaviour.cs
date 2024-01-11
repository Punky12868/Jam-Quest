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
    [SerializeField] Vector2 raycastOriginOffset;
    [SerializeField] Vector2[] raycastDirections;
    private void Awake()
    {
        waypoints = waypointsHolder.GetComponent<Waypoints>().GetWaypoints();
        localScaleX = transform.localScale.x;
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
            RaycastHit2D hit = Physics2D.Linecast((Vector2)transform.position + raycastOriginOffset, (Vector2)transform.position + raycastOriginOffset + raycastDirections[i]);

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

    public Vector2[] GetDirections()
    {
        return raycastDirections;
    }

    public void SetDirections(int value)
    {
        for (int i = 0; i < raycastDirections.Length; i++)
        {
            raycastDirections[i] = new Vector2(raycastDirections[i].x * value, raycastDirections[i].y * value);
        }
    }

    public void SetOuterLightDistance(int value)
    {
        light2D.pointLightOuterRadius *= value;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < raycastDirections.Length; i++)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawRay((Vector2)transform.position + raycastOriginOffset, raycastDirections[i]);

            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere((Vector2)transform.position + raycastOriginOffset + raycastDirections[i], 0.3f);
        }

        Gizmos.color = Color.blue;
    }
}