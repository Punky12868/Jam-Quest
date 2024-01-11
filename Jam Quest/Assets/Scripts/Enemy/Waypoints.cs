using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;

    public Transform[] GetWaypoints()
    {
        return waypoints;
    }

    private void OnDrawGizmos()
    {
        if (waypoints != null)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                if (i == 0)
                {
                    Gizmos.color = Color.yellow;

                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);

                    Gizmos.color = Color.red;

                    Gizmos.DrawWireSphere(waypoints[i].position, 0.5f);
                }
                else if (i == waypoints.Length - 1)
                {
                    Gizmos.color = Color.green;

                    Gizmos.DrawWireSphere(waypoints[i].position, 0.5f);
                }
                else
                {
                    Gizmos.color = Color.yellow;

                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);

                    Gizmos.color = Color.cyan;

                    Gizmos.DrawWireSphere(waypoints[i].position, 0.5f);
                }
            }
        }
    }
}
