using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCheckPoints : MonoBehaviour
{
    private CheckPoint[] checkPoints;
    public static CheckPoint currentCheckpoint;

    private void Awake()
    {
        checkPoints = FindObjectsOfType<CheckPoint>();
    }

    public void DeactivateAllCheckPoints()
    {
        foreach (CheckPoint checkPoint in checkPoints)
        {
            checkPoint.DeactivateCheckpoint();
        }

        FindObjectOfType<Death>().ResetSpawnPoint();
    }

    public void DeactivateLastCheckPoint()
    {
        for (int i = 0; i < checkPoints.Length; i++)
        {
            if (checkPoints[i] != currentCheckpoint)
            {
                checkPoints[i].DeactivateCheckpoint();
            }
            else
            {
                FindObjectOfType<Death>().SetRespawnPoint(currentCheckpoint.respawnPoint);
            }
        }
    }
}
